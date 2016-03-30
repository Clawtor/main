using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyDemandModel
{
	public class Program
	{
		//	Model demand.
		//	Agent has a commodity that is used up over time.
		//	Agent has the means to buy more, agent's desire is related to how much it currently has.
		//	Step 1, model desire.
		public static void Main(string[] args)
		{
			var r = new Random();

			var farmers = new List<Farmer>();
			var farmerNumber = 10;

			var farmersOverTime = new List<int>();
			var foodPriceOverTime = new List<double>();

			for(var i=0;i<farmerNumber;i++)
			{
				farmers.Add(new Farmer(r) { Name = $"Farmer{i}" });
			}

			var broker = new Broker();
			var rainFallModifier = 1.2;

			for (int i = 0; i < 100; i++)
			{
				if(i > 0 && i % 10 == 0)
				{
					rainFallModifier = r.Next(6, 15)/10.0;
				}
				Console.WriteLine($"Rainfall Modifier = {rainFallModifier}.");

				foreach(var agent in farmers)
				{
					agent.Produce(rainFallModifier);
					agent.Upkeep();
					agent.Desire();

					agent.Sell(broker);
					agent.Buy(broker);
					agent.Food.TickEnd();
				}

				broker.ProcessOffers();

				//	Need to redistribute gold from dead farmers.
				var deadGold = farmers.Where(x => x.Alive == false).Sum(x => x.Gold.Quantity);
				
				Console.WriteLine($"{farmers.Count(x => x.Alive == false)} farmers died.");

				farmers = farmers.Where(x => x.Alive).ToList();
				if (deadGold > 0)
				{
					foreach (var f in farmers)
					{
						f.Gold.Quantity += deadGold / farmers.Count;
					}
				}
				Console.WriteLine($"{farmers.Count(x => x.Reproduce == true)} farmers born.");

				var bornList = new List<Farmer>();

				foreach (var f in farmers.Where(x => x.Reproduce))
				{
					var newFarmer = new Farmer(r) { Name = $"so_{f.Name}" };

					var halfFood = f.Food.Quantity / 2;
					var halfGold = f.Gold.Quantity / 2;

					newFarmer.Food.Quantity = halfFood;
					newFarmer.Gold.Quantity = halfGold;

					bornList.Add(newFarmer);

					f.Reproduce = false;
					f.Food.Quantity = halfFood;
					f.Gold.Quantity = halfGold;
				}

				farmers.AddRange(bornList);
				Console.WriteLine($"Number of farmers: {farmers.Count}");
				Console.WriteLine($"The Price of food is {broker.Price}");

				Console.WriteLine($"Total gold = {farmers.Sum(x => x.Gold.Quantity)}");
				
				Console.ReadLine();
			}
			Console.ReadLine();
		}

		public class Broker
		{
			public List<Offer> SellOffers { get; set; } = new List<Offer>();
			public List<Offer> BuyOffers { get; set; } = new List<Offer>();
			public double Price { get; set; } = 1;
			public double PriceInflation = 0.1;
			public double GoldPool { get; set; }

			//	This is the first iteration and will of course contain errors.
			public void ProcessOffers()
			{
				if (BuyOffers.Count() == 0)
				{
					Price -= Price * PriceInflation;
					BuyOffers.Clear();
					SellOffers.Clear();
					return;
				}

				if (SellOffers.Count() == 0)
				{
					Price += Price * PriceInflation;
					BuyOffers.Clear();
					SellOffers.Clear();
					return;
				}

				var sellTotal = SellOffers.Sum(x => x.Quantity);
				var buyTotal = BuyOffers.Sum(x => x.Quantity);

				if (buyTotal > sellTotal)
				{
					var commoditySold = sellTotal;
					var fractionBought = sellTotal / buyTotal;

					foreach (var buy in BuyOffers)
					{
						var buyAmount = buy.Quantity * fractionBought;
						buy.Farmer.Food.Quantity += buyAmount;
						buy.Farmer.Gold.Quantity -= buyAmount * Price;
                    }

					foreach(var sell in SellOffers)
					{
						var sellAmount = sell.Quantity;
						sell.Farmer.Food.Quantity -= sellAmount;
						sell.Farmer.Gold.Quantity += sellAmount * Price; 
					}

					Price += Price * PriceInflation;
                }

				if (buyTotal < sellTotal)
				{
					var commoditySold = buyTotal;
					var fractionSold = buyTotal / sellTotal;
					foreach (var buy in BuyOffers)
					{
						var buyAmount = buy.Quantity;
						buy.Farmer.Food.Quantity += buyAmount;
						buy.Farmer.Gold.Quantity -= buyAmount * Price;
					}

					foreach (var sell in SellOffers)
					{
						var sellAmount = sell.Quantity * fractionSold;
						sell.Farmer.Food.Quantity -= sellAmount;
						sell.Farmer.Gold.Quantity += sellAmount * Price;
					}

					Price -= Price * PriceInflation;
				}

				BuyOffers.Clear();
				SellOffers.Clear();
			}
		}
		
		public class Farmer
		{
			private Random Random { get; }
			public string Name { get; set; }
			public bool Reproduce = false;
			public Dictionary<CommodityType, Commodity> Commodities = new Dictionary<CommodityType, Commodity>();

			public Commodity Food { get; set; } = new Commodity()
			{
				Type = CommodityType.Food,
				Max = 14,
				Min = 1, 
				ProduceQuantity = 4,
				Quantity = 4,
				UpkeepCost = 2,
				Desire = 0.4
			};

			public Commodity Gold { get; set; } = new Commodity()
			{
				Type = CommodityType.Gold,
				Max = 10,
				Min = 1,
				ProduceQuantity = 0,
				Quantity = 5,
				UpkeepCost = 0,
				Desire = 0.5
			};

			public Farmer(Random random)
			{
				Random = random;
				Commodities.Add(CommodityType.Food, Food);
				Commodities.Add(CommodityType.Gold, Gold);
			}

			public Demand Demand { get; set; }
			public bool Alive { get; set; } = true;

			public void Produce(double rainfallModifier)
			{
				Food.TickStart();
				if(Random.NextDouble() > 0.5)
					Food.Quantity += Food.ProduceQuantity * rainfallModifier;
			}

			public void Upkeep()
			{
				if (Food.Quantity <= 0)
					Alive = false;

				if (Food.Quantity > Food.Max * 0.75)
				{
					Reproduce = true;
				}

				Food.Quantity -= Food.UpkeepCost;
			}

			//	Desire for a commodity -> Based on what the actor requires and
			//	how much it has. Returns 0-1
			public void Desire()
			{
				//	Food desire.
				Food.Desire = Sigmoid(Food.Max - Food.Quantity, 1, Food.Max);
				//	Desire to sell food, opposite of desire for food?
				Gold.Desire = 1 - Food.Desire;
			}

			private double Sigmoid(double x, double steepness, double max)
			{
				return 1 / (1 + Math.Pow(Math.E, -(steepness * (x - max/2))));
			}
			
			public void Sell(Broker broker)
			{
				if(Random.NextDouble() < Gold.Desire && Food.Quantity > 0)
				{
					//	How much food do you sell..??
					//	Lets say total - 1.5 * usage.
					var quantity = Food.Quantity - 1.5 * Food.UpkeepCost;
					if(quantity > 0)
					{
						var offer = new Offer()
						{
							CommodityType = CommodityType.Food,
							Quantity = quantity,
							PriceEach = broker.Price, //	Base price on previous price.
							Desire = Food.Quantity / Food.Max,
							Farmer = this//	Should be base on how much food you have.
						};

						broker.SellOffers.Add(offer);
					}
				}
			}

			//	How much 
			public void Buy(Broker broker)
			{
				if(Random.NextDouble() < Food.Desire && Gold.Quantity > broker.Price)
				{
					//	TODO -> How much to buy.
					var offer = new Offer()
					{
						CommodityType = CommodityType.Food,
						PriceEach = broker.Price, //Lets just say people try to get the best price possible.
						Quantity = (Food.Max - Food.Quantity) * broker.Price <= Gold.Quantity 
									? Food.Max - Food.Quantity 
									: Gold.Quantity / broker.Price,
						Desire = Food.Desire,
						Farmer = this
					};
					broker.BuyOffers.Add(offer);
				}
			}
		}

		public enum CommodityType
		{
			Food,
			Wood,
			Gold
		}
		
		public class Commodity
		{
			public CommodityType Type { get; set; }
			public double Quantity { get; set; }
			public int Max { get; set; }
			public int Min { get; set; }    //	As in need at least this much.

			public int UpkeepCost { get; set; }
			private double PreviousQuantity { get; set; } = 0;
			public int ProduceQuantity { get; set; }

			public double Desire { get; set; }

			public double Change()
			{
				return Quantity - PreviousQuantity;
			}

			public void TickStart()
			{
				
			}
			public void TickEnd()
			{
				PreviousQuantity = Quantity;
			}
		}

		public class Offer
		{
			public CommodityType CommodityType { get; set; }
			public double Quantity { get; set; }
			public double PriceEach { get; set; }
			public double Desire { get; set; }	// How much the agent desires to buy/sell.
			public Farmer Farmer { get; set; }
		}

		//	Represents how much an actor wants a thing and how much they want it.
		public class Demand
		{
			public CommodityType CommodityType { get; set; }
			public int Quantity { get; set; }
			public double Price { get; set; }
		}
	}
}

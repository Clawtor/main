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
			var agent1 = new Farmer()
			{
				Name = "Farmer1"
			};
			var agent2 = new Farmer()
			{
				Name = "Farmer2"
			};

			var agent3 = new Farmer()
			{
				Name = "Farmer3"
			};

			var broker = new Broker();

			for (int i = 0;i<100;i++)
			{
				agent1.Produce(r);
				agent1.Upkeep();
				agent1.Desire();
				Console.WriteLine($"Agent 1 Food = {agent1.Food.Quantity}, Desire for Food = {agent1.Food.Desire}. Gold = {agent1.Gold.Quantity}");

				agent2.Produce(r);
				agent2.Upkeep();
				agent2.Desire();
				Console.WriteLine($"Agent 2 Food = {agent2.Food.Quantity}, Desire for Food = {agent2.Food.Desire}. Gold = {agent2.Gold.Quantity}");

				agent3.Produce(r);
				agent3.Upkeep();
				agent3.Desire();

				Console.WriteLine($"Agent 3 Food = {agent3.Food.Quantity}, Desire for Food = {agent3.Food.Desire}. Gold = {agent3.Gold.Quantity}");
				//	Note -> Buying and selling should be determined randomly..?

				agent1.Sell(broker);
				agent1.Buy(broker);
				agent1.Food.TickEnd();

				agent2.Sell(broker);
				agent2.Buy(broker);
				agent2.Food.TickEnd();

				agent3.Sell(broker);
				agent3.Buy(broker);
				agent3.Food.TickEnd();

				Console.WriteLine("Broker Buy Offers:");
				foreach (var b in broker.BuyOffers)
					Console.WriteLine($"\t{b.Quantity} units of type {b.CommodityType} at {b.PriceEach} each.");

				Console.WriteLine("Broker Sell Offers:");
				foreach (var b in broker.SellOffers)
					Console.WriteLine($"\t{b.Quantity} units of type {b.CommodityType} at {b.PriceEach} each.");

				broker.ProcessOffers();

				Console.WriteLine($"The Price of food is {broker.Price}");

				Console.WriteLine($"Total gold = {agent1.Gold.Quantity + agent2.Gold.Quantity + agent3.Gold.Quantity}");

				if (agent1.Gold.Quantity + agent2.Gold.Quantity + agent3.Gold.Quantity != 15)
				{
					Console.WriteLine();
				}

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
					Price -= Price * PriceInflation; Console.WriteLine("Price decreased.");
					SellOffers.Clear();
					return;
				}

				if (SellOffers.Count() == 0)
				{
					Price += Price * PriceInflation;
					BuyOffers.Clear();
					return;
				}

				var sale = 0;
				var goldLost = 0.0;
				var goldGained = 0.0;

				//	If there are more buy offers then sell offers
				if (BuyOffers.Select(x => x.Quantity).Sum() > SellOffers.Select(x => x.Quantity).Sum())
				{
					Price += Price * PriceInflation;
					BuyOffers = BuyOffers.Where(x => x.Desire > 0.5).ToList();
					Console.WriteLine($"Price = {Price}");

					//	Problem currently is that a wealthy farmer with low desire for food
					//	will get as much of a commodity as a farmer with more desire.
					//	Need a system of bidding.

					
					var orderedOffers = BuyOffers.OrderByDescending(x => x.Desire).ToList();
					var commodityAmount = SellOffers.Sum(x => x.Quantity);
                    while (commodityAmount > 0 && orderedOffers.Count() > 0)
					{
						
						for(var i=orderedOffers.Count-1;i>=0;i--)
						{
							var offer = orderedOffers[i];
							if(offer.Farmer.Gold.Quantity > Price)
							{
								//	Transaction
								GoldPool += Price;
								goldLost -= Price;
								offer.Farmer.Gold.Quantity -= Price;
								offer.Farmer.Food.Quantity += 1;
								commodityAmount -= 1;
								sale++;
							}else
							{
								orderedOffers.Remove(offer);
								if (orderedOffers.Count == 0) break;
							}
						}
						//	Update desires.
					}
					if(sale > 0)
					{
						var goldEach = GoldPool / sale;
						goldGained = GoldPool;
						Console.WriteLine($"Gold lost = {goldLost}");
						Console.WriteLine($"Gold gained = {goldGained}");
						//	Then distribute the money.
						foreach (var offer in SellOffers)
						{
							offer.Farmer.Gold.Quantity += offer.Quantity * goldEach;
						}
					}
				}
				else
				{
					//	Sellers market.
					//	The same as above but for selling.
					//	Dec
					Price -= Price * PriceInflation; Console.WriteLine("Price decreased.");
					var orderedOffers = BuyOffers.OrderByDescending(x => x.Desire).ToList();
					var commodityAmount = SellOffers.Sum(x => x.Quantity);
					while (commodityAmount > 0 && orderedOffers.Count() > 0)
					{
						
						for (var i = orderedOffers.Count - 1; i >= 0; i--)
						{
							var offer = orderedOffers[i];
							if (offer.Farmer.Gold.Quantity > Price)
							{
								//	Transaction
								GoldPool += Price;
								offer.Farmer.Gold.Quantity -= Price;
								offer.Farmer.Food.Quantity += 1;
								sale++;
								commodityAmount -= 1;
							}
							else
							{
								orderedOffers.Remove(offer);
								if (orderedOffers.Count == 0) break;
							}
						}
						//	Update desires.
					}
					if (sale > 0)
					{
						var goldEach = GoldPool / sale;
						Console.WriteLine($"Gold each = {goldEach}");
						//	Then distribute the money.
                        foreach (var offer in SellOffers)
						{
							offer.Farmer.Gold.Quantity += offer.Quantity * goldEach;
						}
					}
					if (commodityAmount > 0)
					{
						Price -= Price * PriceInflation; Console.WriteLine("Price decreased.");
					}
                }

				if(Math.Abs(goldGained) != Math.Abs(goldLost))
				{
					Console.WriteLine();
				}

				GoldPool = 0;
				BuyOffers.Clear();
				SellOffers.Clear();
			}
		}

		public class Farmer
		{
			public string Name { get; set; }
			public Commodity Food { get; set; } = new Commodity()
			{
				Type = CommodityType.Food,
				Max = 10,
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

			public Demand Demand { get; set; }
			public bool Alive { get; set; } = true;

			public void Produce(Random r)
			{
				Food.TickStart();
				if(r.NextDouble() > 0.5)
					Food.Quantity += Food.ProduceQuantity;
			}

			public void Upkeep()
			{
				if (Food.Quantity <= 0)
					Alive = false;

				Food.Quantity -= Food.UpkeepCost;
			}

			//	Desire for a commodity -> Based on what the actor requires and
			//	how much it has. Returns 0-1
			public void Desire()
			{
				if(Food.UpkeepCost > 0 && Food.Quantity/Food.Max < 0.5)
				{
					Food.Desire += -1.0 * (Food.Change()/(double)Food.Max);
				}else
				{
					Food.Desire = 0;
				}

				if (Food.Quantity == 0)
					Food.Desire = 1;

				if (Food.Desire < 0)
					Food.Desire = 0;
				if (Food.Desire > 1)
					Food.Desire = 1;

				if(Food.Desire == 0)
				{
					Gold.Desire = 1;
				}
				else
				{
					Gold.Desire = 0;
				}

				//return Food.Desire;
			}
			
			public void Sell(Broker broker)
			{
				if(Gold.Desire > 0 && Food.Desire == 0)
				{
					//	How much food do you sell..??
					//	Lets say total - 1.5 * usage.
					var quantity = Food.Quantity - 1.5 * Food.UpkeepCost;
					Console.WriteLine($"{Name} wants to sell {quantity} food.");

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

			//	How much 
			public void Buy(Broker broker)
			{
				if(Gold.Quantity > 0 && Food.Desire > 0)
				{
					Console.WriteLine($"{Name} wants to buy {Food.Max - Food.Quantity} food.");

					//	TODO -> How much to buy.
					var offer = new Offer()
					{
						CommodityType = CommodityType.Food,
						PriceEach = broker.Price, //Lets just say people try to get the best price possible.
						Quantity = Food.Max-Food.Quantity,
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

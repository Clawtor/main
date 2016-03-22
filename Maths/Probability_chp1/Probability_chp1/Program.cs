using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probability_chp1
{
	class Program
	{
		Random r = new Random((int)DateTime.Now.Ticks);
		static void Main(string[] args)
		{
			Program p = new Program();
		}

		Program()
		{
			//int length = 1000;
			//List<bool> sequence = CoinFlips(length);
			////  Number of trues vs false.
			//int nTrues = sequence.Where(x => x == true).Count();
			//int nFalses = length - nTrues;
			////  Number of runs of trues and falses
			//var results = WinningsExperiment(1000, 40);
			//Console.WriteLine("Chance of winning 6 or more coins = " + results.Where(x => x.Key >= 6).Sum(x => x.Value) / (float)length);

			Ex4();
			Console.ReadLine();
		}

		public List<bool> CoinFlips(int length)
		{

			List<bool> sequence = Enumerable.Range(0, length).Select(x =>
			{
				return r.NextDouble() > 0.5;
			}).ToList();
			return sequence;
		}

		public void Ex2()
		{
			var experimentNum = 100;
			var n = 80;

			var withinPoint1 = 0;
			for(int e = 0;e< experimentNum;e++)
			{
				double heads = 0;
				for (int i = 0; i < n; i++)
				{
					if (r.NextDouble() > 0.5)
					{
						heads++;
					}
				}
				if(Math.Abs(heads / n - 0.5) <= 0.1)
				{
					withinPoint1++;
                }
			}
			Console.WriteLine(withinPoint1);
		}

		//	Roll 3 dice and list the sum
		public void Ex3()
		{
			var experiments = 1000;
			var outcomes =
				Enumerable.Range(0, experiments)
				.Select(x => r.Next(1, 6) + r.Next(1, 6) + r.Next(1, 6))
				.GroupBy(x => x)
				.Select(g => new { Key = g.Key, Count = g.Count() })
				.OrderBy(y => y.Key) ;

			foreach(var outcome in outcomes)
			{
				Console.WriteLine(String.Format("Key: {0} = {1}%", outcome.Key, (float)outcome.Count / experiments * 100));
			}
		}

		//	Raquet ball
		public void Ex4()
		{
			var experiments = 1000;
			var player1Wins = 0;
			var player2Wins = 0
			foreach(var e in Enumerable.Range(0, experiments))
			{
				var winningPoints = 21;
				var player1Score = 0;
				var player2Score = 0;
				bool isPlayer1Serving = true;
				while(player1Score < 21 && player2Score < 21)
				{
					var random = r.NextDouble();
					if (isPlayer1Serving)
					{
						if(random >= 0.4)
						{
							player1Score++;
						}
						else
						{
							isPlayer1Serving = false;
						}
					}
					else
					{
						if (random >= 0.5)
						{
							player2Score++;
						}
						else
						{
							isPlayer1Serving = true;
						}
					}
				}
				if(player1Score == 21)
				{
					player1Wins++;
				}
				else
				{
					player2Wins++;
				}
			}

			Console.WriteLine
				(String.Format(
					"Chance player 1 will win: {0} \nChance player 2 will win: {1}",
					(float)player1Wins / experiments  * 100,
					(float)player2Wins / experiments * 100
				)
			);
		}

        public Dictionary<bool, List<Tuple<int, int>>> CountRuns(List<bool> sequence)
        {
            List<int> Runs_true = new List<int>();
            List<int> Runs_false = new List<int>();

            bool countingTrues = sequence[0];
            int count = 1;
            foreach (var b in sequence.Skip(1))
            {
                if (countingTrues)
                {
                    if (b == true)
                    {
                        count++;
                    }
                    else
                    {
                        Runs_true.Add(count);
                        count = 1;
                        countingTrues = false;
                    }
                }
                else
                {
                    if (b == true)
                    {
                        Runs_false.Add(count);
                        count = 1;
                        countingTrues = true;
                    }
                    else
                    {
                        count++;
                    }
                }
            }
            if(countingTrues)
            {
                Runs_true.Add(count);
            }
            else
            {
                Runs_false.Add(count);
            }

            var t = Runs_true.GroupBy(x => x).Select(x => {
                return new Tuple<int, int>(x.Key, x.Count());
            }).ToList();
            var f = Runs_false.GroupBy(x => x).Select(x => {
                return new Tuple<int, int>(x.Key, x.Count());
            }).ToList();

            var d = new Dictionary<bool, List<Tuple<int, int>>>();
            d.Add(true, t);
            d.Add(false, f);
            return d;
        }

        //  Count t-h
        public List<int> Difference(List<bool> sequence)
        {
            List<int> diffList = new List<int>();
            int difference = 0;
            foreach (var i in sequence)
            {
                if (i) difference++;
                else difference--;
                diffList.Add(difference);
            }
            return diffList;
        } 

        public Dictionary<int, int> WinningsExperiment(int trials, int trialLength)
        {
            var results = new Dictionary<int, int>();
            for(int i=0;i<trials;i++)
            {
                var sequence = CoinFlips(trialLength);
                var diff = Difference(sequence).Sum();
                if(results.Keys.Contains(diff))
                {
                    results[diff]++;
                }
                else
                {
                    results.Add(diff, 1);
                }
            }
            return results.OrderBy(x => x.Key).ToDictionary(t => t.Key, t => t.Value);
        }
    }
}

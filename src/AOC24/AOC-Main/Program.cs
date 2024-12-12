using AOC_1;
using AOC_10;
using AOC_11;
using AOC_12;
using AOC_2;
using AOC_3;
using AOC_4;
using AOC_5;
using AOC_6;
using AOC_7;
using AOC_8;
using AOC_9;

namespace AOC_Main
{
	public class Aoc24
	{
		private static readonly string ASSETS_DIR = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.Parent.FullName, "assets");
		static void Main(string[] args)
		{
			DateTime start;
			double timer=0;

			Console.WriteLine("/////////// 01 /////////// \r\n");

			start = DateTime.Now;
			var locations1 = new LocationsList(Path.Combine(ASSETS_DIR, "01-input.txt"), 0);
			var locations2 = new LocationsList(Path.Combine(ASSETS_DIR, "01-input.txt"), 1);

			Console.WriteLine(locations1.Locations.Count + "\r\n");
			Console.WriteLine(locations2.Locations.Count + "\r\n");

			Console.WriteLine($"DISTANCE : {locations1.DistanceToList(locations2)}" + "\r\n");

			Console.WriteLine("/////////// 01 - 2 ///////////");

			Console.WriteLine($"SIMILARITY = {locations1.SimilarityToList(locations2)}");

			timer = (DateTime.Now - start).TotalMilliseconds;
			Console.WriteLine($"Time : {timer} ms" + "\r\n");

			Console.WriteLine("/////////// 02 ///////////");

			start = DateTime.Now;
			var reports = new RedNoseReports(Path.Combine(ASSETS_DIR, "02-input.txt"));

			Console.WriteLine($"SAFE REPORTS = {reports.NumberSafeReports()}");

			timer = (DateTime.Now - start).TotalMilliseconds;
			Console.WriteLine($"Time : {timer} ms" + "\r\n");

			Console.WriteLine("/////////// 03 ///////////");

			start = DateTime.Now;
			var mulResult = MemoryDecipher.DecipherFromFile(Path.Combine(ASSETS_DIR, "03-input.txt"));

			Console.WriteLine($"MUL MEMORY RESULT = {mulResult}");

			Console.WriteLine("/////////// 03-2 ///////////");

			var mulResult2 = MemoryDecipher.DecipherFromFileConditional(Path.Combine(ASSETS_DIR, "03-input.txt"));

			timer = (DateTime.Now - start).TotalMilliseconds;

			Console.WriteLine($"MUL MEMORY RESULT 2 = {mulResult2}");
			Console.WriteLine($"Time : {timer} ms" + "\r\n");

			Console.WriteLine("/////////// 04 ///////////");

			var gridSearch = new WordSearchGrid(Path.Combine(ASSETS_DIR, "04-input.txt"));

			start = DateTime.Now;
			int xmasCount = gridSearch.FindOccurences("XMAS", "SAMX");
			timer = (DateTime.Now - start).TotalMilliseconds;

			Console.WriteLine($"GRID XMAS COUNT = {xmasCount}");
			Console.WriteLine($"Time : {timer} ms" + "\r\n");

			Console.WriteLine("/////////// 04 - 2 ///////////");

			start = DateTime.Now;
			int x_masCount = gridSearch.FindXOccurences("MAS", "SAM");
			timer = (DateTime.Now - start).TotalMilliseconds;

			Console.WriteLine($"GRID X-MAS COUNT = {x_masCount}");
			Console.WriteLine($"Time : {timer} ms" + "\r\n");

			Console.WriteLine("/////////// 05 ///////////");

			var udpates = new Updates(Path.Combine(ASSETS_DIR, "05-input.txt"));

			start = DateTime.Now;
			var graph = new DirectedAcyclicGraph(Path.Combine(ASSETS_DIR, "05-input.txt"));
			var goodUpdatesSum = graph.SumOfCorrectOrders(udpates);
			timer = (DateTime.Now - start).TotalMilliseconds;

			Console.WriteLine($"MIDDLE SUM FOR GOOD UPDATES : {goodUpdatesSum}");
			Console.WriteLine($"Time : {timer} ms" + "\r\n");

			Console.WriteLine("/////////// 05 - 2 ///////////");

			start = DateTime.Now;
			var badUpdatesSum = graph.SumOfIncorrectOrders(udpates);
			timer = (DateTime.Now - start).TotalMilliseconds;

			Console.WriteLine($"MIDDLE SUM FOR BAD UPDATES : {badUpdatesSum}");
			Console.WriteLine($"Time : {timer} ms" + "\r\n");

			Console.WriteLine("/////////// 06 ///////////");

			start = DateTime.Now;
			var predictor = new GuardPathPredictor(Path.Combine(ASSETS_DIR, "06-input.txt"));
			var numberSteps = predictor.StepsBeforeOut();
			timer = (DateTime.Now - start).TotalMilliseconds;

			Console.WriteLine($"NUMBER OF STEPS BEFORE OUT : {numberSteps}");
			Console.WriteLine($"Time : {timer} ms" + "\r\n");

			Console.WriteLine("/////////// 06 - 2 ///////////");

			start = DateTime.Now;
			var numberLoops = predictor.ObstaclesOptions();
			timer = (DateTime.Now - start).TotalMilliseconds;

			Console.WriteLine($"NUMBER OF POSSIBLE OBSTACLES : {numberLoops}");
			Console.WriteLine($"Time : {timer} ms" + "\r\n");

			Console.WriteLine("/////////// 07 ///////////");

			var solver = new EquationSolver(Path.Combine(ASSETS_DIR, "07-input.txt"));

			start = DateTime.Now;
			var solvableSum = solver.FindOperatorsAndSum();
			timer = (DateTime.Now - start).TotalMilliseconds;

			Console.WriteLine($"SUM OF TEST VALUES THAT CAN BE FOUND : {solvableSum}");
			Console.WriteLine($"Time : {timer} ms" + "\r\n");

			Console.WriteLine("/////////// 07 - 2 ///////////");

			start = DateTime.Now;
			var solvableSum2 = solver.FindOperatorsAndSum(true);
			timer = (DateTime.Now - start).TotalMilliseconds;

			Console.WriteLine($"SUM OF TEST VALUES THAT CAN BE FOUND : {solvableSum2}");
			Console.WriteLine($"Time : {timer} ms" + "\r\n");

			Console.WriteLine("/////////// 08 ///////////");

			start = DateTime.Now;
			var antinodesFinder = new AntinodesMap(Path.Combine(ASSETS_DIR, "08-input.txt"));
			antinodesFinder.FindAntinodes(out var antinodes,out var harmonics);
			timer = (DateTime.Now - start).TotalMilliseconds;

			Console.WriteLine($"ANTINODES : {antinodes}\r\n");

			Console.WriteLine("/////////// 08 - 2 ///////////");

			Console.WriteLine($"HARMONICS : {harmonics}\r\n");
			Console.WriteLine($"Time : {timer} ms" + "\r\n");

			Console.WriteLine("/////////// 09 ///////////");

			start = DateTime.Now;
			var diskFragment = new DiskFragment(Path.Combine(ASSETS_DIR, "09-input.txt"));
			diskFragment.Checksum(out var frag1, out var frag2);
			timer = (DateTime.Now - start).TotalMilliseconds;

			Console.WriteLine($"PART 1 : {frag1}\r\n");

			Console.WriteLine("/////////// 09 - 2 ///////////");

			Console.WriteLine($"PART 2 : {frag2}\r\n");
			Console.WriteLine($"Time : {timer} ms" + "\r\n");

			Console.WriteLine("/////////// 10 ///////////");

			start = DateTime.Now;
			var topo = new TopographicMap(Path.Combine(ASSETS_DIR, "10-input.txt"));
			topo.CountScores(out var topo1, out var topo2);
			timer = (DateTime.Now - start).TotalMilliseconds;

			Console.WriteLine($"PART 1 : {topo1}\r\n");

			Console.WriteLine("/////////// 10 - 2 ///////////");

			Console.WriteLine($"PART 2 : {topo2}\r\n");
			Console.WriteLine($"Time : {timer} ms" + "\r\n");

			Console.WriteLine("/////////// 11 ///////////");

			start = DateTime.Now;
			var cailloux = new BlinkStoneStatus(Path.Combine(ASSETS_DIR, "11-input.txt"));
			cailloux.CountStones(out var cailloux1, out var cailloux2);
			timer = (DateTime.Now - start).TotalMilliseconds;

			Console.WriteLine($"PART 1 : {cailloux1}\r\n");

			Console.WriteLine("/////////// 11 - 2 ///////////");

			Console.WriteLine($"PART 2 : {cailloux2}\r\n");
			Console.WriteLine($"Time : {timer} ms" + "\r\n");

			Console.WriteLine("/////////// 12 ///////////");

			start = DateTime.Now;
			var garden = new PlantsMap(Path.Combine(ASSETS_DIR, "12-input.txt"));
			garden.CalculateFences(out var price1, out var price2);
			timer = (DateTime.Now - start).TotalMilliseconds;

			Console.WriteLine($"PART 1 : {price1}\r\n");

			Console.WriteLine("/////////// 12 - 2 ///////////");

			Console.WriteLine($"PART 2 : {price2}\r\n");
			Console.WriteLine($"Time : {timer} ms" + "\r\n");

			Console.Read();
		}
	}
}
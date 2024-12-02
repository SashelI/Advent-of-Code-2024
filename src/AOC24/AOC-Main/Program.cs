﻿using AOC_1;
using AOC_2;
using AOC_3;

namespace AOC_Main
{
	public class Aoc24
	{
		private static readonly string ASSETS_DIR = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.Parent.FullName, "assets");
		static void Main(string[] args)
		{
			DateTime start;
			double timer1=0; double timer2=0; double timer3=0;

			Console.WriteLine("/////////// 01 /////////// \r\n");

			start = DateTime.Now;
			var locations1 = new LocationsList(Path.Combine(ASSETS_DIR, "01-input.txt"), 0);
			var locations2 = new LocationsList(Path.Combine(ASSETS_DIR, "01-input.txt"), 1);

			Console.WriteLine(locations1.Locations.Count + "\r\n");
			Console.WriteLine(locations2.Locations.Count + "\r\n");

			Console.WriteLine($"DISTANCE : {locations1.DistanceToList(locations2)}" + "\r\n");

			Console.WriteLine("/////////// 01 - 2 ///////////");

			Console.WriteLine($"SIMILARITY = {locations1.SimilarityToList(locations2)}");

			timer1 = (DateTime.Now - start).TotalMilliseconds;
			Console.WriteLine($"Time : {timer1}" + "\r\n");

			Console.WriteLine("/////////// 02 ///////////");

			start = DateTime.Now;
			var reports = new RedNoseReports(Path.Combine(ASSETS_DIR, "02-input.txt"));

			Console.WriteLine($"SAFE REPORTS = {reports.NumberSafeReports()}");

			timer2 = (DateTime.Now - start).TotalMilliseconds;
			Console.WriteLine($"Time : {timer2}" + "\r\n");

			Console.WriteLine("/////////// 03 ///////////");

			start = DateTime.Now;
			var mulResult = MemoryDecipher.DecipherFromFile(Path.Combine(ASSETS_DIR, "03-input.txt"));

			Console.WriteLine($"MUL MEMORY RESULT = {mulResult}");

			Console.WriteLine("/////////// 03-2 ///////////");

			var mulResult2 = MemoryDecipher.DecipherFromFileConditional(Path.Combine(ASSETS_DIR, "03-input.txt"));

			timer3 = (DateTime.Now - start).TotalMilliseconds;

			Console.WriteLine($"MUL MEMORY RESULT 2 = {mulResult2}");
			Console.WriteLine($"Time : {timer3}" + "\r\n");

			Console.Read();
		}
	}
}
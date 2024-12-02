﻿using AOC_1;
using AOC_2;

namespace AOC_Main
{
	public class Aoc24
	{
		private static readonly string ASSETS_DIR = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.Parent.FullName, "assets");
		static void Main(string[] args)
		{
			Console.WriteLine("/////////// 01 /////////// \r\n");

			var locations1 = new LocationsList(Path.Combine(ASSETS_DIR, "01-input.txt"), 0);
			var locations2 = new LocationsList(Path.Combine(ASSETS_DIR, "01-input.txt"), 1);

			Console.WriteLine(locations1.Locations.Count + "\r\n");
			Console.WriteLine(locations2.Locations.Count + "\r\n");

			Console.WriteLine($"DISTANCE : {locations1.DistanceToList(locations2)}" + "\r\n");

			Console.WriteLine("/////////// 01 - 2 ///////////");

			Console.WriteLine($"SIMILARITY = {locations1.SimilarityToList(locations2)}");

			Console.WriteLine("/////////// 02 ///////////");

			var reports = new RedNoseReports(Path.Combine(ASSETS_DIR, "02-input.txt"));

			Console.WriteLine($"SAFE REPORTS = {reports.NumberSafeReports()}");

			Console.Read();
		}
	}
}
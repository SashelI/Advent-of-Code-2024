﻿using AOC_1;

namespace AOC_Main
{
	public class Aoc24
	{
		private static readonly string ASSETS_DIR = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.Parent.FullName;
		static void Main(string[] args)
		{
			Console.WriteLine("/////////// 01 ///////////" + ASSETS_DIR);

			var locations1 = new LocationsList(Path.Combine(ASSETS_DIR, "assets\\01-input.txt"), 0);
			var locations2 = new LocationsList(Path.Combine(ASSETS_DIR, "assets\\01-input.txt"), 1);

			Console.WriteLine(locations1.Locations.Count + "\r\n");
			Console.WriteLine(locations2.Locations.Count + "\r\n");

			Console.WriteLine($"DISTANCE : {locations1.DistanceToList(locations2)}");

			Console.Read();
		}
	}
}
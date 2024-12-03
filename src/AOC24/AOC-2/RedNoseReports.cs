using System.Text;

namespace AOC_2
{
	public class RedNoseReports
	{
		public List<List<int>> Reports;

		public RedNoseReports(string filePath)
		{
			var lines = File.ReadAllLines(filePath);
			Reports = new List<List<int>>();

			foreach (var line in lines)
			{
				var report = new List<int>();
				var levels = line.Split(" ");

				foreach (var level in levels)
				{
					report.Add(int.Parse(level));
				}

				Reports.Add(report);
			}
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();

			builder.Append($"Reports ({Reports.Count}) : \r\n");

			foreach (var report in Reports)
			{
				foreach (var level in report)
				{
					builder.Append($"{level} , ");
				}
				builder.Append("\r\n");
			}

			return builder.ToString();
		}

		public int NumberSafeReports()
		{
			int safeReports = 0;

			foreach (var report in Reports)
			{
				safeReports += Convert.ToInt32(IsReportSafe(report, report));
			}

			return safeReports;
		}

		private bool IsReportSafe(List<int> report, List<int> originalReport, int nbPass=0)
		{
			int variation = 0;

			for (int i = 1; i < report.Count; i++)
			{
				var diff = report[i] - report[i-1];

				if (diff is 0 or > 3 or < -3) 
				{
					if (nbPass >= originalReport.Count)
					{
						return false;
					}

					var newReport = new List<int>(originalReport);
					newReport.RemoveAt(nbPass);
					return IsReportSafe(newReport, originalReport, nbPass+1);
				}
				else if (diff < 0 && variation > 0)
				{
					if (nbPass >= originalReport.Count)
					{
						return false;
					}

					var newReport = new List<int>(originalReport);
					newReport.RemoveAt(nbPass);
					return IsReportSafe(newReport, originalReport, nbPass + 1);
				}
				else if (diff > 0 && variation < 0)
				{
					if (nbPass >= originalReport.Count)
					{
						return false;
					}

					var newReport = new List<int>(originalReport);
					newReport.RemoveAt(nbPass);
					return IsReportSafe(newReport, originalReport, nbPass + 1);
				}

				variation = diff;
			}

			return true;
		}
	}
}

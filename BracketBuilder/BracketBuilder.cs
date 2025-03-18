public class BracketBuilder
{
	private int matches = 0;
	private readonly object matchLock = new object();

	private const string USE_DEFAULT_STATS = "DEFAULT_STATS";
	private const string USE_STATS_MAP = "STATS_MAP";
	private const string USE_SEED_STATS = "SEED_STATS";
	private const string STATS_CHOICE = USE_STATS_MAP;
	private const bool RUN_PERFECT_BRACKET = false;

	private Dictionary<int, List<Team>> winnersByRound = new Dictionary<int, List<Team>>();

	#region Teams
	private List<Team> teams = new List<Team>
	{
		//South
		new Team("Auburn", 1,  Region.South),
        new Team("ALST/SFU", 16, Region.South),
        new Team("Louisville", 8,  Region.South),
        new Team("Creighton", 9,  Region.South),
        new Team("Michigan", 5,  Region.South),
        new Team("UC San Diego", 12, Region.South),
        new Team("Texas A&M", 4,  Region.South),
        new Team("Yale", 13, Region.South),
        new Team("Ole Miss", 6,  Region.South),
        new Team("SDSU/UNC", 11, Region.South),
        new Team("Iowa St.", 3,  Region.South),
        new Team("Lipscromb", 14, Region.South),
        new Team("Marquette", 7,  Region.South),
        new Team("New Mexico", 10, Region.South),
        new Team("Michigan St.", 2,  Region.South),
        new Team("Bryant", 15, Region.South),

		//West
		new Team("Florida", 1,  Region.West),
        new Team("Norfolk St.", 16, Region.West),
        new Team("UConn", 8,  Region.West),
        new Team("Oklahoma", 9,  Region.West),
        new Team("Memphis", 5,  Region.West),
        new Team("Colorado St.", 12, Region.West),
        new Team("Maryland", 4,  Region.West),
        new Team("Grand Canyon", 13, Region.West),
        new Team("Missouri", 6,  Region.West),
        new Team("Drake", 11, Region.West),
        new Team("Texas Tech", 3,  Region.West),
        new Team("UNCW", 14, Region.West),
        new Team("Kansas", 7,  Region.West),
        new Team("Arkansas", 10, Region.West),
        new Team("St. John's", 2,  Region.West),
        new Team("Omaha", 15, Region.West),

		//East
		new Team("Duke", 1,  Region.East),
        new Team("AMER/MTSM", 16, Region.East),
        new Team("Mississippi St.", 8,  Region.East),
        new Team("Baylor", 9,  Region.East),
        new Team("Oregon", 5,  Region.East),
        new Team("Liberty", 12, Region.East),
        new Team("Arizona", 4,  Region.East),
        new Team("Akron", 13, Region.East),
        new Team("BYU", 6,  Region.East),
        new Team("VCU", 11, Region.East),
        new Team("Wisconsin", 3,  Region.East),
        new Team("Montana", 14, Region.East),
        new Team("Saint Mary's", 7,  Region.East),
        new Team("Vanderbilt", 10, Region.East),
        new Team("Alabama", 2,  Region.East),
        new Team("Robert Morris", 15, Region.East),

		//Midwest
		new Team("Houston", 1,  Region.Midwest),
        new Team("SIUE", 16, Region.Midwest),
        new Team("Gonzaga", 8,  Region.Midwest),
        new Team("Georgia", 9,  Region.Midwest),
        new Team("Clemson", 5,  Region.Midwest),
        new Team("McNeese", 12, Region.Midwest),
        new Team("Purdue", 4,  Region.Midwest),
        new Team("High Point", 13, Region.Midwest),
        new Team("Illinois", 6,  Region.Midwest),
        new Team("TEX/XAV", 11, Region.Midwest),
        new Team("Kentucky", 3,  Region.Midwest),
        new Team("Troy", 14, Region.Midwest),
        new Team("UCLA", 7,  Region.Midwest),
        new Team("Utah St.", 10, Region.Midwest),
        new Team("Tennessee", 2,  Region.Midwest),
        new Team("Wofford", 15, Region.Midwest),
	};
    #endregion

    #region Perfect Bracket Teams

    private readonly Dictionary<int, List<Team>> perfectMap = new Dictionary<int, List<Team>>()
    {
        {1, new List<Team>() 
		{
            new Team("UConn", 1,  Region.East),
            new Team("Northwestern", 9,  Region.East),
			new Team("San Diego St.", 5,  Region.East),
			new Team("Yale", 13, Region.East),
			new Team("Duquesne", 11, Region.East),
			new Team("Illinois", 3,  Region.East),
			new Team("Washington St.", 7,  Region.East),
			new Team("Iowa St.", 2,  Region.East),
			new Team("N. Carolina", 1,  Region.West),
			new Team("Michigan St.", 9,  Region.West),
			new Team("Grand Canyon", 12, Region.West),
			new Team("Alabama", 4,  Region.West),
			new Team("Clemson", 6,  Region.West),
			new Team("Baylor", 3,  Region.West),
			new Team("Dayton", 7,  Region.West),
			new Team("Arizona", 2,  Region.West),
			new Team("Houston", 1,  Region.South),
			new Team("Texas A&M", 9,  Region.South),
			new Team("James Madison", 12, Region.South),
			new Team("Duke", 4,  Region.South),
			new Team("NC State", 11, Region.South),
			new Team("Oakland", 14, Region.South),
			new Team("BSU/COLO", 10, Region.South),
			new Team("Marquette", 2,  Region.South),
			new Team("Purdue", 1,  Region.Midwest),
			new Team("Utah St.", 8,  Region.Midwest),
			new Team("Gonzaga", 5,  Region.Midwest),
			new Team("Kansas", 4,  Region.Midwest),
			new Team("Oregon", 11, Region.Midwest),
			new Team("Creighton", 3,  Region.Midwest),
			new Team("Texas", 7,  Region.Midwest),
			new Team("Tennessee", 2,  Region.Midwest),
        } },
		{2, new List<Team>
        {
            new Team("UConn", 1,  Region.East),
            new Team("San Diego St.", 5,  Region.East),
            new Team("Illinois", 3,  Region.East),
            new Team("Iowa St.", 2,  Region.East),
            new Team("N. Carolina", 1,  Region.West),
            new Team("Alabama", 4,  Region.West),
            new Team("Clemson", 6,  Region.West),
            new Team("Arizona", 2,  Region.West),
            new Team("Houston", 1,  Region.South),
            new Team("Duke", 4,  Region.South),
            new Team("NC State", 11, Region.South),
            new Team("Marquette", 2,  Region.South),
            new Team("Purdue", 1,  Region.Midwest),
            new Team("Gonzaga", 5,  Region.Midwest),
            new Team("Creighton", 3,  Region.Midwest),
            new Team("Tennessee", 2,  Region.Midwest),
        } },
		{ 3, new List<Team>
		{
            new Team("UConn", 1,  Region.East),
            new Team("Illinois", 3,  Region.East),
            new Team("Alabama", 4,  Region.West),
            new Team("Clemson", 6,  Region.West),
            new Team("Duke", 4,  Region.South),
            new Team("NC State", 11, Region.South),
            new Team("Purdue", 1,  Region.Midwest),
            new Team("Tennessee", 2,  Region.Midwest),
        } },
		{ 4, new List<Team>
        {
            new Team("UConn", 1,  Region.East),
            new Team("Alabama", 4,  Region.West),
            new Team("NC State", 11, Region.South),
            new Team("Purdue", 1,  Region.Midwest),
        } },
		{ 5, new List<Team>
        {
            new Team("UConn", 1,  Region.East),
            new Team("Purdue", 1,  Region.Midwest),
        } },
		{ 6, new List<Team>
		{
            new Team("UConn", 1,  Region.East)
        } }
    };

    #endregion

    #region StatMaps
    private static Dictionary<double, double> statsMap = new Dictionary<double, double>
	{
		{0, .5 },
		{1, .46},
		{2, .40},
		{3, .35},
		{4, .28},
		{5, .20},
		{6, .15},
		{7, .10},
		{8, .05},
		{9, .03},
		{10, .007},
		{11, .005},
		{12, .003},
		{13, .001},
		{14, .0008 },
		{15, .0001 }
	};

	private static Dictionary<double, double> oneSeedMap = new Dictionary<double, double>
	{
		{1, .5},
		{2, .533},
		{3, .625},
		{4, .707},
		{5, .833},
		{6, .688},
		{7, .857},
		{8, .802},
		{9, .9},
		{10, .857},
		{11, .571},
		{12, 1.0},
		{13, 1.0},
		{14, 0.5},
		{15, 0.5},
		{16, .993},
	};

	private static Dictionary<double, double> twoSeedMap = new Dictionary<double, double>
	{
		{1, .467},
		{2, .5},
		{3, .603},
		{4, .444},
		{5, .167},
		{6, .722},
		{7, .701},
		{8, .444},
		{9, .5},
		{10, .633},
		{11, .875},
		{12, 1.0},
		{13, 0.5},
		{14, 0.5},
		{15, .943},
		{16, 0.5},
	};

	private static Dictionary<double, double> threeSeedMap = new Dictionary<double, double>
	{
		{1, .375},
		{2, .397},
		{3, .5},
		{4, .625},
		{5, .5},
		{6, .578},
		{7, .611},
		{8, 1.0},
		{9, 1.0},
		{10, .692},
		{11, .691},
		{12, 0.5},
		{13, 0.5},
		{14, .85},
		{15, 1.0},
		{16, 0.5},
	};

	private static Dictionary<double, double> fourSeedMap = new Dictionary<double, double>
	{
		{1, .293},
		{2, .556},
		{3, .375},
		{4, .5},
		{5, .558},
		{6, .333},
		{7, .333},
		{8, .364},
		{9, .5},
		{10, 1.0},
		{11, 0.5},
		{12, .707},
		{13, .799},
		{14, 0.5},
		{15, 0.5},
		{16, 0.5},
	};

	private static Dictionary<double, double> fiveSeedMap = new Dictionary<double, double>
	{
		{1, .167},
		{2, .833},
		{3, .5},
		{4, .442},
		{5, .5},
		{6, 1.0},
		{7, 0.5},
		{8, .25},
		{9, .25},
		{10, 1.0},
		{11, 0.5},
		{12, .669},
		{13, .824},
		{14, 0.5},
		{15, 0.5},
		{16, 0.5},
	};

	private static Dictionary<double, double> sixSeedMap = new Dictionary<double, double>
	{
		{1, .313},
		{2, .278},
		{3, .422},
		{4, .667},
		{5, 0.0},
		{6, 0.5},
		{7, .625},
		{8, .25},
		{9, 0.5},
		{10, .6},
		{11, .638},
		{12, 0.5},
		{13, 0.5},
		{14, .875},
		{15, 0.5},
		{16, 0.5},
	};

	private static Dictionary<double, double> sevenSeedMap = new Dictionary<double, double>
	{
		{1, .143},
		{2, .299},
		{3, .389},
		{4, .667},
		{5, 0.5},
		{6, .375},
		{7, 0.5},
		{8, .5},
		{9, 0.5},
		{10, .604},
		{11, 0.0},
		{12, 0.5},
		{13, 0.5},
		{14, 1.0},
		{15, .667},
		{16, 0.5},
	};

	private static Dictionary<double, double> eightSeedMap = new Dictionary<double, double>
	{
		{1, .198},
		{2, .556},
		{3, 0.0},
		{4, .636},
		{5, .75},
		{6, .75},
		{7, .5},
		{8, 0.5},
		{9, .512},
		{10, 0.5},
		{11, 1.0},
		{12, 0.0},
		{13, 1.0},
		{14, 0.5},
		{15, 0.5},
		{16, 0.5},
	};

	private static Dictionary<double, double> nineSeedMap = new Dictionary<double, double>
	{
		{1, .1},
		{2, .5},
		{3, 0.0},
		{4, .5},
		{5, .75},
		{6, 0.5},
		{7, 0.5},
		{8, .488},
		{9, 0.5},
		{10, 1.0},
		{11, 0.0},
		{12, 0.5},
		{13, 1.0},
		{14, 0.5},
		{15, 0.5},
		{16, 1.0},
	};

	private static Dictionary<double, double> tenSeedMap = new Dictionary<double, double>
	{
		{1, .143},
		{2, .367},
		{3, .308},
		{4, 0.0},
		{5, 0.0},
		{6, .4},
		{7, .396},
		{8, 0.5},
		{9, 0.0},
		{10, 0.5},
		{11, .333},
		{12, 0.5},
		{13, 0.5},
		{14, 1.0},
		{15, 1.0},
		{16, 0.5},
	};

	private static Dictionary<double, double> elevenSeedMap = new Dictionary<double, double>
	{
		{1, .429},
		{2, .125},
		{3, .309},
		{4, 0.5},
		{5, 0.5},
		{6, .365},
		{7, 1.0},
		{8, 0.0},
		{9, 1.0},
		{10, .667},
		{11, 0.5},
		{12, 0.5},
		{13, 0.5},
		{14, 1.0},
		{15, 0.5},
		{16, 0.5},
	};

	private static Dictionary<double, double> twelveSeedMap = new Dictionary<double, double>
	{
		{1, 0.0},
		{2, 0.0},
		{3, 0.5},
		{4, .293},
		{5, .331},
		{6, 0.5},
		{7, 0.5},
		{8, 1.0},
		{9, 0.5},
		{10, 0.5},
		{11, 0.5},
		{12, 0.5},
		{13, .75},
		{14, 0.5},
		{15, 0.5},
		{16, 0.5},
	};

	private static Dictionary<double, double> thirteenSeedMap = new Dictionary<double, double>
	{
		{1, 0.0},
		{2, 0.5},
		{3, 0.5},
		{4, .201},
		{5, .176},
		{6, 0.5},
		{7, 0.5},
		{8, 0.0},
		{9, 0.0},
		{10, 0.5},
		{11, 0.5},
		{12, .25},
		{13, 0.5},
		{14, 0.5},
		{15, 0.5},
		{16, 0.5},
	};

	private static Dictionary<double, double> fourteenSeedMap = new Dictionary<double, double>
	{
		{1, 0.5},
		{2, 0.5},
		{3, .15},
		{4, 0.5},
		{5, 0.5},
		{6, .125},
		{7, 0.0},
		{8, 0.5},
		{9, 0.5},
		{10, 0.0},
		{11, 0.0},
		{12, 0.5},
		{13, 0.5},
		{14, 0.5},
		{15, 0.5},
		{16, 0.5},
	};

	private static Dictionary<double, double> fifteenSeedMap = new Dictionary<double, double>
	{
		{1, 0.5},
		{2, .057},
		{3, 0.0},
		{4, 0.5},
		{5, 0.5},
		{6, 0.5},
		{7, .333},
		{8, 0.5},
		{9, 0.5},
		{10, 0.0},
		{11, 0.5},
		{12, 0.5},
		{13, 0.5},
		{14, 0.5},
		{15, 0.5},
		{16, 0.5},
	};

	private static Dictionary<double, double> sixteenSeedMap = new Dictionary<double, double>
	{
		{1, 0.007},
		{2, 0.5},
		{3, 0.5},
		{4, 0.5},
		{5, 0.5},
		{6, 0.5},
		{7, 0.5},
		{8, 0.5},
		{9, 0.0},
		{10, 0.5},
		{11, 0.5},
		{12, 0.5},
		{13, 0.5},
		{14, 0.5},
		{15, 0.5},
		{16, 0.5},
	};
	private static Dictionary<double, Dictionary<double, double>> seedStatsMap = new Dictionary<double, Dictionary<double, double>>
	{
		{1, oneSeedMap},
		{2, twoSeedMap},
		{3, threeSeedMap},
		{4, fourSeedMap},
		{5, fiveSeedMap},
		{6, sixSeedMap},
		{7, sevenSeedMap},
		{8, eightSeedMap},
		{9, nineSeedMap},
		{10, tenSeedMap},
		{11, elevenSeedMap},
		{12, twelveSeedMap},
		{13, thirteenSeedMap},
		{14, fourteenSeedMap},
		{15, fifteenSeedMap},
		{16, sixteenSeedMap},
	};
	#endregion

	#region RoundDisplayNames
	private readonly IReadOnlyDictionary<int, string> roundDisplayNames = new Dictionary<int, string>
	{
		{ 1, "Round 1" },
		{ 2, "Round 2" },
		{ 3, "Sweet 16" },
		{ 4, "Elite Eight" },
		{ 5, "Final Four" },
		{ 6, "Championship" }
	};
	#endregion

    public static void Main(string[] args)
	{
		BracketBuilder drafts = new BracketBuilder();
		drafts.Run();
		Console.WriteLine("Press any key to finish");
		Console.ReadKey();
	}

	private void Run()
	{
		if (RUN_PERFECT_BRACKET)
		{
			PerfectBracket();
		}
		else
		{
			StartBracket();
		}
	}

	

	private void StartBracket()
	{
		RunBracket(1, teams);
	}

	private bool RunBracket(int round, List<Team> list)
	{
		if (list.Count < 2)
		{
			return true;
		}
		WriteLineWithColor(roundDisplayNames[round], ConsoleColor.Cyan);
		List<Team> nextRound = new List<Team>();
		for (int i = 0; i < list.Count; i += 2)
		{
			Team t1 = list.ElementAt(i);
			Team t2 = list.ElementAt(i + 1);
			Team t = FindWinner(t1, t2);
			var isUpset = (t.Name == t1.Name && t.Seed > t2.Seed) || (t.Name == t2.Name && t.Seed > t1.Seed);
			var message = $"({t.Seed}) {t.Name}";
			if (isUpset)
            {
				WriteLineWithColor(message, ConsoleColor.Red);
			}
            else
            {
				Console.WriteLine(message);
            }
			
			nextRound.Add(t);
			if (list.Count > 8 && nextRound.Count % (list.Count / 8) == 0)
			{
				Console.WriteLine();
			}
		}
		Console.WriteLine();
		winnersByRound.Add(round, nextRound);
		return RunBracket(++round, nextRound);
	}

	private void PerfectBracket()
	{
		bool successful = false;
		var options = new ParallelOptions
		{
			MaxDegreeOfParallelism = 16
		};
		var iteration = 0;
		Parallel.For(1, int.MaxValue, options, (index, loopState) =>
		{
			Interlocked.Increment(ref iteration);
			var success = RunPerfectBracket(1, teams, iteration);
            if (success)
            {
                Console.WriteLine("Perfect Bracket on try: " + iteration);
				loopState.Break();
            }
        });
	}

	private bool RunPerfectBracket(int round, List<Team> list, int currentIteration)
	{
		if (list.Count < 2)
		{
			return true;
		}
		List<Team> nextRound = new List<Team>();
		for (int i = 0; i < list.Count; i += 2)
		{
			Team t1 = list.ElementAt(i);
			Team t2 = list.ElementAt(i + 1);
			nextRound.Add(FindWinner(t1, t2));
		}
		var wrong = nextRound.Except(perfectMap[round]).ToList();
        if (!wrong.Any())
		{
			Console.WriteLine($"Iteration {currentIteration} - Perfect Bracket through round: {round}");
			return RunPerfectBracket(round + 1, nextRound, currentIteration);
		}
		else
		{
			var currentMatches = perfectMap[round].Count - wrong.Count;
			lock(matchLock)
			{
                if (currentMatches > matches)
                {
                    Console.WriteLine($"Iteration {currentIteration} - New high for matches: {currentMatches}");
                    matches = currentMatches;
                }
                else if (currentMatches == matches)
                {
                    Console.WriteLine($"Iteration {currentIteration} - Matched existing high for matches: {currentMatches}");
                }
            }
			
		}
		return false;
	}

	private Team FindWinner(Team t1, Team t2)
	{
		double r = double.Parse(string.Format("{0:0.0000}", Random.Shared.NextDouble()));
		double odd = GetWinningOdd(t1.Seed, t2.Seed);
		if (r < odd)
		{
			return t2;
		}
		return t1;
	}

	private double GetWinningOdd(double s1, double s2)
	{
		switch (STATS_CHOICE)
		{
			case USE_STATS_MAP:
				return GetStatsMapWinningOdd(s1, s2);
			case USE_SEED_STATS:
				return GetSeedStatsWinningOdd(s1, s2);
			case USE_DEFAULT_STATS:
			default:
				return GetDefaultStatsWinningOdd(s1, s2);
		}

	}

	private double GetSeedStatsWinningOdd(double s1, double s2)
	{
		return seedStatsMap[s2][s1];
	}

	private double GetDefaultStatsWinningOdd(double s1, double s2)
	{
		double odd = s1 / (s2 + s1);
		if (s1 - s2 < 0)
		{
			odd -= ((s1 - s2) / ((s2 - s1) * 10));
		}
		else if (s1 - s2 > 0)
		{
			odd += ((s1 - s2) / ((s2 - s1) * 10));
		}
		return double.Parse(odd.ToString("#.##"));
	}

	private double GetStatsMapWinningOdd(double s1, double s2)
	{
		if (s1 == s2)
		{
			return statsMap[0];
		}
		else if (s1 > s2)
		{
			return statsMap[s1 - s2];
		}
		return statsMap[s2 - s1];
	}

	private void WriteLineWithColor(string message, ConsoleColor color)
    {
		Console.ForegroundColor = color;
		Console.WriteLine(message);
		Console.ResetColor();
    }

	sealed record Team(string Name, double Seed, Region Region);
		
	private enum Region
	{
		South,
		East,
		West,
		Midwest
	}
}
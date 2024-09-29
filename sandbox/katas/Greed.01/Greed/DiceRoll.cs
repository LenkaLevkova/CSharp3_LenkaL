public class DiceRoll
{
    private Random random = new Random();

    public List<int> RollDice(int numberOfDice)
    {
        var dice = new List<int>();

        for (int i = 0; i < numberOfDice; i++)
        {
            dice.Add(random.Next(1, 7));
        }

        return dice;
    }

    public List<int> GetScoringDice(List<int> dice)
    {
        var scoringDice = new List<int>();
        var groups = dice.GroupBy(d => d).ToDictionary(g => g.Key, g => g.Count());

        // Check for special hands like straight or three pairs
        if (dice.Count == 6 && IsStraight(dice))
        {
            Console.WriteLine("You rolled a straight!");
            return dice; // All dice are part of the straight
        }

        if (dice.Count == 6 && IsThreePairs(dice))
        {
            Console.WriteLine("You rolled three pairs!");
            return dice; // All dice are part of the three pairs
        }

        // Check for triples, four, five, and six of a kind
        foreach (var group in groups)
        {
            if (group.Value >= 3)
            {
                for (int i = 0; i < group.Value; i++)
                {
                    scoringDice.Add(group.Key);
                }
            }
        }

        // Check for single ones and fives
        scoringDice.AddRange(dice.Where(d => d == 1 || d == 5));
        return scoringDice;
    }

    public int CalculateScore(List<int> scoringDice)
    {
        int score = 0;
        var groups = scoringDice.GroupBy(d => d).ToDictionary(g => g.Key, g => g.Count());

        foreach (var group in groups)
        {
            if (group.Value >= 3)
            {
                score += CalculateTripleScore(group.Key, group.Value);
            }
        }

        score += groups.ContainsKey(1) ? groups[1] % 3 * 100 : 0;
        score += groups.ContainsKey(5) ? groups[5] % 3 * 50 : 0;
        return score;
    }

    private int CalculateTripleScore(int value, int count)
    {
        int baseScore = value switch
        {
            1 => 1000,
            2 => 200,
            3 => 300,
            4 => 400,
            5 => 500,
            6 => 600,
            _ => 0
        };

        if (count == 4) return baseScore * 2;
        if (count == 5) return baseScore * 4;
        if (count == 6) return baseScore * 8;

        return baseScore;
    }

    private bool IsStraight(List<int> dice)
    {
        return dice.OrderBy(d => d).SequenceEqual(new List<int> { 1, 2, 3, 4, 5, 6 });
    }

    private bool IsThreePairs(List<int> dice)
    {
        var groups = dice.GroupBy(d => d).ToDictionary(g => g.Key, g => g.Count());
        return groups.Count == 3 && groups.All(g => g.Value == 2);
    }
}

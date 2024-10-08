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
        var rolledNumbersGroups = dice.GroupBy(d => d).ToDictionary(g => g.Key, g => g.Count());

        //Check for straight
        if (dice.Count == 6 && IsStraight(dice))//ahaaa toto platí len pre 6 stranné kocky..
        {
            Console.WriteLine("You rolled a straight!");
            return dice; //ale v tom 
        }

        //Check for 3 pairs
        if (dice.Count == 6 && IsThreePairs(dice))
        {
            Console.WriteLine("You rolled three pairs!");
            return dice;
        }

        //Check if there are 3, 4, 5 or 6 dice of the same kind
        foreach (var group in rolledNumbersGroups)
        {
            if (group.Value >= 3)
            {
                for (int i = 0; i < group.Value; i++)
                {
                    scoringDice.Add(group.Key);
                }
            }
        }

        //Check for signle 1 and 5
        scoringDice.AddRange(dice.Where(d => d == 1 || d == 5));

        return scoringDice;
    }

    public int CalculateScore(List<int> dice)
    {
        var score = 0;

        if (dice.Count == 6 && IsStraight(dice))
        {
            return 1200;
        }

        if (dice.Count == 6 && IsThreePairs(dice))
        {
            return 800;
        }

        var scoringGroups = dice.GroupBy(d => d).ToDictionary(g => g.Key, g => g.Count());

        foreach (var group in scoringGroups)
        {
            if (group.Value >= 3)
            {
                score += CalculateTripleScore(group.Key, group.Value);
            }
        }

        score += scoringGroups.ContainsKey(1) ? scoringGroups[1] % 3 * 100 : 0; //toto mi úplne nedáva zmysel. Ak sa tam nachádzajú jedničky, tak ich počeet vymodulujem troma, to prenásobím 100 a prirátam k výsledku? Takže keď budem mať 5 jedničiek, tak nie len že dostanem 4000 bodov, ešte budem mať aj 200 navyše? :-) 
        score += scoringGroups.ContainsKey(5) ? scoringGroups[5] % 3 * 50 : 0;
        return score;
    }

    private int CalculateTripleScore(int value, int count)
    {
        var baseScore = value switch
        {
            1 => 1000,
            2 => 200,
            3 => 300,
            4 => 400,
            5 => 500,
            6 => 600,
            _ => 0
        };

        var multiplier = (count > 4) ? Math.Pow(2, count-3) : 1;
        return baseScore * multiplier; // 2, 4, 8, to vyzera, ako nejaka matematicka postutpnost :-) 
    }

    private bool IsStraight(List<int> dice)
    {
        return dice.OrderBy(d => d).SequenceEqual(new List<int> { 1, 2, 3, 4, 5, 6 }); //toto nebude uplne fungovat pre kocky s viacerymi stranami. Mozes na to ale pouzit uplne rovnaku metodu ako na tie dvojice, akurat pre kocku s n stranami budees hladat n grup, ktore maju presne jedneho clena
    }

    private bool IsThreePairs(List<int> dice)
    {
        var groups = dice.GroupBy(d => d).ToDictionary(g => g.Key, g => g.Count());
        return groups.Count == 3 && groups.All(g => g.Value == 2); //dooost dobre :-) 
    }
}

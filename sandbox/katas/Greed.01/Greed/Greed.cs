public class Greed
{
    public int TotalScore { get; private set; }
    private DiceRoll diceRoll;

    public Greed()
    {
        diceRoll = new DiceRoll();
    }

    public void Play()
    {
        var keepPlaying = true;

        while (keepPlaying)
        {
            var roundScore = PlayOneRound();
            Console.WriteLine($"Round over! You scored {roundScore} points this round.");
            TotalScore += roundScore;
            Console.WriteLine($"Total score: {TotalScore}");
            Console.WriteLine("Do you want to play another round? (yes/no)");
            keepPlaying = Console.ReadLine() == "yes";
        }
    }

    private int PlayOneRound()
    {
        var roundScore = 0;
        var dice = diceRoll.RollDice(6);

        while (true)
        {
            Console.WriteLine("You rolled: " + string.Join(", ", dice));
            var scoringDice = diceRoll.GetScoringDice(dice);

            if (scoringDice.Count == 0)
            {
                Console.WriteLine("You rolled no scoring dice, which means you lost, loser!");
                return 0;
            }

            var currentScore = diceRoll.CalculateScore(scoringDice);
            roundScore += currentScore;
            Console.WriteLine($"You scored {currentScore} points this roll. Total round score: {roundScore}");

            Console.WriteLine("Do you want to keep rolling or end the game with your current score? (roll/end)");
            var decision = Console.ReadLine().ToLower();

            while (!(decision.Equals("end") || decision.Equals("roll")))
            {
                Console.WriteLine("Instructions unclear. Please select from options: roll/end.");
                decision = Console.ReadLine().ToLower();
            }

            if (decision.Equals("end"))
            {
                return roundScore;
            }

            if (decision.Equals("roll"))
            {
                dice = ReRollDice(dice);
            }
        }
    }

    private List<int> ReRollDice(List<int> currentRoll)
    {
        var rerollDice = new List<int>();
        Console.WriteLine("Which dice do you want to reroll? Enter the numbers, comma-separated: ");
        var diceToReroll = GetValidatedDiceInput(currentRoll);
        rerollDice = diceRoll.RollDice(diceToReroll.Count);
        Console.WriteLine("Rerolled dice: " + string.Join(", ", rerollDice));
        return rerollDice;
    }

    private List<int> GetValidatedDiceInput(List<int> currentRoll)
    {
        var diceToReroll = new List<int>();
        var validInput = false;

        while (!validInput)
        {
            var input = Console.ReadLine();
            if (input != null)
            {
                var diceInput = input.Split(',');

                validInput = true;
                diceToReroll = new List<int>();

                foreach (var diceStr in diceInput)
                {
                    if (int.TryParse(diceStr.Trim(), out int diceNumber) && currentRoll.Contains(diceNumber))
                    {
                        diceToReroll.Add(diceNumber);
                    }
                    else
                    {
                        validInput = false;
                        break;
                    }
                }

                if (!validInput)
                {
                    Console.WriteLine("Invalid input. Please enter only valid dice numbers (comma-separated) from the current roll.");
                    Console.WriteLine("Available dice: " + string.Join(", ", currentRoll.Distinct()));
                }
            }
        }

        return diceToReroll;
    }
}

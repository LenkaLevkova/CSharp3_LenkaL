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

            if (decision.Equals("end"))
            {
                return roundScore;
            }

            if (decision.Equals("roll"))
            {
                Console.WriteLine("How many dice would you like to re-roll?");
                var numberOfDiceToReroll = int.Parse(Console.ReadLine());
                dice = diceRoll.RollDice(numberOfDiceToReroll);
            }
        }
    }
}

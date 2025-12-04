namespace Days;

public class Day3
{

    public void PartOne()
    {

        var inputs = File.ReadAllLines("./Inputs/Day3.txt");

        var result = new List<long>();

        foreach (var bank in inputs)
        {
            var parsedBank = bank.Select(_ => Int32.Parse(_.ToString()));
            var maxJoltage = MaxJoltage(parsedBank, 2);
            result.Add(maxJoltage);

        }

        Console.WriteLine($"Day3 PartOne: {result.Sum()}");
    }

    public void PartTwo()
    {
        var inputs = File.ReadAllLines("./Inputs/Day3.txt");
        var result = new List<long>();

        foreach (var bank in inputs)
        {
            var parsedBank = bank.Select(_ => Int32.Parse(_.ToString()));
            var maxJoltage = MaxJoltage(parsedBank, 12);
            result.Add(maxJoltage);

        }

        Console.WriteLine($"Day3 PartTwo: {result.Sum()}");

    }

    private long MaxJoltage(IEnumerable<int> bank, int max)
    {

        var resultlist = new Stack<int>();

        foreach (var (battery, index) in bank.Select((value, index) => (value, index)))
        {
            var maxSpaceLeft = Math.Min(12, bank.Count() - index);
            var batteryCombo = BatteryCombo(resultlist, battery, maxSpaceLeft, max);
            resultlist = batteryCombo;


        }

        return long.Parse(string.Join("", resultlist.Reverse()));
    }

    private Stack<int> BatteryCombo(Stack<int> currentBatteryBank, int battery, int maxSpaceLeft, int MaxLenght)
    {

        if (!currentBatteryBank.Any())
        {
            currentBatteryBank.Push(battery);
            return currentBatteryBank;
        }


        while (currentBatteryBank.Any() && currentBatteryBank.Peek() < battery && maxSpaceLeft + currentBatteryBank.Count() > MaxLenght)
        {
            currentBatteryBank.Pop();
        }

        if (currentBatteryBank.Count() == MaxLenght)
        {
            return currentBatteryBank;

        }

        if (!currentBatteryBank.Any() || currentBatteryBank.Peek() >= battery || maxSpaceLeft + currentBatteryBank.Count() >= MaxLenght)
        {
            // Console.WriteLine($"pushing {battery}");
            currentBatteryBank.Push(battery);
        }

        return currentBatteryBank;

    }
}

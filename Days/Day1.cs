namespace Days;

public class Day1
{
    public void PartOne()
    {
        var inputs = File.ReadAllLines("./Inputs/Day1.txt");

        var pointer = 50; // start at 50

        var zeroCounter = 0;

        foreach (var input in inputs)
        {
            var originalPointer = pointer;
            var direction = input[0];
            var absuluteAmount = Int32.Parse(input.Substring(1));
            var amount = absuluteAmount % 100;

            if (direction == 'L')
            {
                var remaining = pointer - amount;

                if (remaining < 0)
                {
                    pointer = 100 + remaining;
                }
                else
                {
                    pointer = remaining;
                }
            }
            else
            {
                var remaining = pointer + amount;
                if (remaining >= 100)
                {
                    pointer = remaining - 100;
                }
                else
                {
                    pointer = remaining;
                }
            }


            if (pointer == 0)
            {
                zeroCounter++;
            }
            if (pointer > 99 || pointer < 0)
            {
                Console.WriteLine(direction);
                Console.WriteLine(amount);
                Console.WriteLine(originalPointer);
                Console.WriteLine(pointer);
            }

        }

        Console.WriteLine($"Day1 PartOne:{zeroCounter}");
    }

    public void PartTwo()
    {
        var inputs = File.ReadAllLines("./Inputs/Day1.txt");

        var pointer = 50; // start at 50

        var zeroCounter = 0;

        foreach (var input in inputs)
        {
            var originalPointer = pointer;
            var direction = input[0];
            var absuluteAmount = Int32.Parse(input.Substring(1));
            var amount = absuluteAmount % 100;

            if (direction == 'L')
            {
                var remaining = pointer - amount;

                if (remaining < 0)
                {
                    pointer = 100 + remaining;
                    if (originalPointer != 0 && pointer != 0)
                    {
                        zeroCounter++;
                    }
                }
                else
                {
                    pointer = remaining;
                }
            }
            else
            {
                var remaining = pointer + amount;
                if (remaining >= 100)
                {
                    pointer = remaining - 100;
                    if (originalPointer != 0 && pointer != 0)
                    {
                        zeroCounter++;
                    }
                }
                else
                {
                    pointer = remaining;
                }
            }

            if (pointer == 0)
            {
                zeroCounter++;
            }

            double amountOfRotations = (absuluteAmount / 100);
            int x = Convert.ToInt32(Math.Floor(amountOfRotations));

            zeroCounter += x;

        }

        Console.WriteLine($"Day1 PartTwo:{zeroCounter}");
    }
}

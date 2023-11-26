using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        char[][] matrix = new char[n][];
        int fishCatch = 0;
        int[] position = new int[2];
        Stack<int[]> previousPositions = new Stack<int[]>();
        Queue<string> commands = new Queue<string>();

        for (int i = 0; i < n; i++)
        {
            matrix[i] = Console.ReadLine().ToCharArray();
            for (int j = 0; j < n; j++)
            {
                if (matrix[i][j] == 'S')
                {
                    position[0] = i;
                    position[1] = j;
                }
            }
        }

        while (true)
        {
            string command = Console.ReadLine();

            if (command == "collect the nets")
                break;

            commands.Enqueue(command);
        }

        while (commands.Count > 0)
        {
            string command = commands.Dequeue();
            previousPositions.Push(new int[] { position[0], position[1] });
            position = Move(position, command, n);
            int x = position[0];
            int y = position[1];

            if (matrix[x][y] == '-')
            {
                continue;
            }
            else if (char.IsDigit(matrix[x][y]))
            {
                fishCatch += int.Parse(matrix[x][y].ToString());
                matrix[x][y] = '-';
            }
            else if (matrix[x][y] == 'W')
            {
                Console.WriteLine($"You fell into a whirlpool! The ship sank and you lost the fish you caught. Last coordinates of the ship: [{x},{y}]");
                return;
            }
        }

        if (fishCatch >= 20)
        {
            Console.WriteLine("Success! You managed to reach the quota!");
        }
        else
        {
            int lackOfFish = 20 - fishCatch;
            Console.WriteLine($"You didn't catch enough fish and didn't reach the quota! You need {lackOfFish} tons of fish more.");
        }

        Console.WriteLine($"Amount of fish caught: {fishCatch} tons.");

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine(new string(matrix[i]));
        }
    }

    static int[] Move(int[] position, string command, int n)
    {
        int x = position[0];
        int y = position[1];

        switch (command)
        {
            case "up":
                x = (x - 1 + n) % n;
                break;
            case "down":
                x = (x + 1) % n;
                break;
            case "left":
                y = (y - 1 + n) % n;
                break;
            case "right":
                y = (y + 1) % n;
                break;
        }

        return new int[] { x, y };
    }
}


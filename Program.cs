using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static int screenWidth = 40;
    static int screenHeight = 20;
    static char snakeChar = '*';
    static char foodChar = '@';

    static List<int> snakeX = new List<int>();
    static List<int> snakeY = new List<int>();
    static int foodX;
    static int foodY;
    static int score = 0;
    static bool gameOver = false;

    static void Main()
    {
        Console.Title = "Console Snake Game";
        Console.CursorVisible = false;

        InitializeGame();
        Draw();
        ConsoleKeyInfo keyInfo;

        while (!gameOver)
        {
            if (Console.KeyAvailable)
            {
                keyInfo = Console.ReadKey(true);
                UpdateDirection(keyInfo.Key);
            }

            Move();
            CheckCollision();
            CheckFood();
            Draw();
            Thread.Sleep(100);
        }

        Console.Clear();
        Console.WriteLine("Game Over! Your score: " + score);
    }

    static void InitializeGame()
    {
        // Initialize snake
        snakeX.Add(10);
        snakeY.Add(10);

        // Initialize food
        GenerateFood();
    }

    static void Draw()
    {
        Console.Clear();

        // Draw snake
        for (int i = 0; i < snakeX.Count; i++)
        {
            Console.SetCursorPosition(snakeX[i], snakeY[i]);
            Console.Write(snakeChar);
        }

        // Draw food
        Console.SetCursorPosition(foodX, foodY);
        Console.Write(foodChar);

        // Draw score
        Console.SetCursorPosition(0, screenHeight + 1);
        Console.Write("Score: " + score);
    }

    static void Move()
    {
        int newHeadX = snakeX[0];
        int newHeadY = snakeY[0];

        switch (direction)
        {
            case Direction.Up:
                newHeadY--;
                break;
            case Direction.Down:
                newHeadY++;
                break;
            case Direction.Left:
                newHeadX--;
                break;
            case Direction.Right:
                newHeadX++;
                break;
        }

        snakeX.Insert(0, newHeadX);
        snakeY.Insert(0, newHeadY);

        // Remove last part of the tail
        if (snakeX.Count > score + 1)
        {
            snakeX.RemoveAt(snakeX.Count - 1);
            snakeY.RemoveAt(snakeY.Count - 1);
        }
    }

    static void CheckCollision()
    {
        // Check collision with walls
        if (snakeX[0] < 0 || snakeX[0] >= screenWidth || snakeY[0] < 0 || snakeY[0] >= screenHeight)
        {
            gameOver = true;
        }

        // Check collision with self
        for (int i = 1; i < snakeX.Count; i++)
        {
            if (snakeX[i] == snakeX[0] && snakeY[i] == snakeY[0])
            {
                gameOver = true;
            }
        }
    }

    static void CheckFood()
    {
        // Check if snake eats food
        if (snakeX[0] == foodX && snakeY[0] == foodY)
        {
            score++;
            GenerateFood();
        }
    }

    static void GenerateFood()
    {
        Random random = new Random();
        foodX = random.Next(0, screenWidth);
        foodY = random.Next(0, screenHeight);

        // Make sure food does not spawn on the snake
        while (snakeX.Contains(foodX) && snakeY.Contains(foodY))
        {
            foodX = random.Next(0, screenWidth);
            foodY = random.Next(0, screenHeight);
        }
    }

    static Direction direction = Direction.Right;

    static void UpdateDirection(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow:
                if (direction != Direction.Down)
                    direction = Direction.Up;
                break;
            case ConsoleKey.DownArrow:
                if (direction != Direction.Up)
                    direction = Direction.Down;
                break;
            case ConsoleKey.LeftArrow:
                if (direction != Direction.Right)
                    direction = Direction.Left;
                break;
            case ConsoleKey.RightArrow:
                if (direction != Direction.Left)
                    direction = Direction.Right;
                break;
        }
    }

    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}

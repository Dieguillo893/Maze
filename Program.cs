using System;

public class Maze
{
    private char[,] maze;
    private int playerRow, playerCol;
    private int goalRow, goalCol;
    private bool gameOver;

    public static void Main(string[] args)
    {
        Maze game = new Maze();
        game.Start();
    }

    public Maze()
    {
        Init();
    }

    public void Start()
    {
        ShowGameStartScreen();

        while (!IsGameOver())
        {
            ShowBoard();
            string input;
            do
            {
                ShowInputOptions();
                input = GetInput();
            }
            while (!IsValidInput(input));

            ProcessInput(input);
        }

        ShowGameOverScreen();
    }

    public void Init()
    {
        maze = new char[,]
        {
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
            { '|', 'R', '|', ' ', '|', ' ', '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
            { '|', ' ', '|', ' ', '|', ' ', '|', ' ', '|', '#', '#', '#', '#', '#', '#', '#', '#', '#', '|' },
            { '|', ' ', '|', ' ', '|', ' ', '|', ' ', '|', ' ', '|', ' ', '|', ' ', '|', ' ', '|', ' ', '|' },
            { '|', '_', '|', '_', '|', ' ', '|', ' ', '|', ' ', '|', ' ', '|', ' ', '|', ' ', '|', ' ', '|' },
            { '|', ' ', '|', ' ', '|', ' ', '|', 'G', '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
        };

        playerRow = 1;
        playerCol = 1;
        goalRow = 5;
        goalCol = 7;
        gameOver = false;
    }

    public void ShowGameStartScreen()
    {
        Console.WriteLine("Welcome to the Maze!");
    }

    public void ShowBoard()
    {
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                if (i == playerRow && j == playerCol)
                    Console.Write('R');
                else
                    Console.Write(maze[i, j]);
            }
            Console.WriteLine();
        }
    }

    public void ShowInputOptions()
    {
        Console.Write("Enter [w] UP | [s] DOWN | [a] LEFT | [d] RIGHT: ");
    }

    public string GetInput()
    {
        return Console.ReadLine().Trim().ToLower();
    }

    public bool IsValidInput(string input)
    {
        if (input == "w" || input == "s" || input == "a" || input == "d")
            return true;

        Console.WriteLine("Invalid input.");
        return false;
    }

    public void ProcessInput(string input)
    {
        int newRow = playerRow;
        int newCol = playerCol;

        switch (input)
        {
            case "w":
                newRow--;
                break;
            case "s":
                newRow++;
                break;
            case "a":
                newCol--;
                break;
            case "d":
                newCol++;
                break;
        }

        if (CanMoveTo(newRow, newCol))
        {
            playerRow = newRow;
            playerCol = newCol;
        }
        else
        {
            Console.WriteLine($"Cannot go {DirectionName(input)}.");
        }

        if (playerRow == goalRow && playerCol == goalCol)
        {
            gameOver = true;
        }
    }

    public bool CanMoveTo(int row, int col)
    {
        if (row < 0 || col < 0 || row >= maze.GetLength(0) || col >= maze.GetLength(1))
            return false;

        char cell = maze[row, col];
        return cell != '#' && cell != '|';
    }

    public string DirectionName(string input)
    {
        return input switch
        {
            "w" => "up",
            "s" => "down",
            "a" => "left",
            "d" => "right",
            _ => "that way"
        };
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void ShowGameOverScreen()
    {
        ShowBoard();
        Console.WriteLine("You won! Congratulations!");
    }
}

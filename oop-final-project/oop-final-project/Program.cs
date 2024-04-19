using System;

//class to show coordinate on the board
class Coordinate
{
    public int Row { get; set; }
    public int Column { get; set; }

    public Coordinate(int row, int column)
    {
        Row = row;
        Column = column;
    }
}

//class to show player 
class Player
{
    public char Symbol { get; }

    public Player(char symbol)
    {
        Symbol = symbol;
    }

    public Coordinate MakeMove()
    {
        Console.Write($"Enter column number (1-7) to place '{Symbol}': ");
        int column = int.Parse(Console.ReadLine()) - 1;
        return new Coordinate(0, column);
    }
}

//class to show board game
class Board
{
    private cahr[,] grid;

    public Board()
    {
        grid = new char[6, 7];
        Initialize();
    }

    private void Initialize()
    {
        for (int row = 0; row < 6; row++)
        {
            for(int col = 0; col < 7; col++)
            {
                grid[row, col] = '-';
            }
        }
    }

    public void Print()
    {
        for (int row =0; row < 6; row++)
        {
            for(int col = 0; col < 7; col++)
            {
                Console.Write(grid[row, col] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public bool IsValidMove(int column)
    {
        return grid[0, column] == '-';
    }

    public void PlacePiece (int column, char symbol)
    {
        for (int row = 5; row>=0; row--)
        {
            if (grid[row, column] == '-')
            {
                grid[row, column] = symbol;
                break;
            }
        }
    }

    public bool CheckForWin(char symbol)
    {
        //horizntal win
        for (int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                if (grid[row, col] == symbol && grid[row, col + 1] == symbol && grid[row, col + 2] == symbol && grid[row, col + 3] == symbol)
                {
                    return true;
                }
            }
        }

        //vertical win
        for (int row = 0; row<3; row++)
        {
            for (int col = 0; col<7; col++)
            {
                if (grid[row, col] == symbol && grid[row + 1, col] == symbol && grid[row + 2, col] == symbol && grid[row + 3, col] == symbol)
                {
                    return true;
                }
            }
        }

        //diagonal win L to R
        for (int row = 0; row < 3; row++)
        {
            for(int col = 0; col<4; col++)
            {
                if (grid[row, col] == symbol && grid[row + 1, col + 1] == symbol && grid[row + 2, col + 2] == symbol && grid[row + 3, col + 3] == symbol)
                {
                    return true;
                }   
            }
        }

        //diagonal win R to L
        for (int row = 0; row< 3; row++)
        {
            for (int col = 3; col<7; col++)
            {
                if (grid[row, col] == symbol && grid[row + 1, col - 1] == symbol && grid[row + 2, col - 2] == symbol && grid[row + 3, col - 3] == symbol)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool IsBoardFull()
    {
        //check full
        for(int row = 0; row < 6; row++)
        {
            for (int col = 0; col<7; col++)
            {
                if (grid[row, col] == '-')
                {
                    return false;
                }
            }
        }
        return true;
    }
}

//class for managing the game

class Game
{
    private Board board;
    private Player[] players;
    private int currentPlayerIndex;

    public Game(Player player1, Player player2)
    {
        board = new Board();
        players = new Player[] { player1, player2 };
        currentPlayerIndex = 0;
    }

    public void Start()
    {
        bool gameEnded = false;
        while (!gameEnded)
        {
            // Print the board
            board.Print();

            Player currentPlayer = players[currentPlayerIndex];

            Coordinate move = currentPlayer.MakeMove();

            if (board.IsValidMove(move.Column))
            {
                // Place on board
                board.PlacePiece(move.Column, currentPlayer.Symbol);

                //win or draw
                if (board.CheckForWin(currentPlayer.Symbol))
                {
                    Console.WriteLine($"Player {currentPlayer.Symbol} wins!");
                    gameEnded = true;
                }
                else if (board.IsBoardFull())
                {
                    Console.WriteLine("It's a draw!");
                    gameEnded = true;
                }
                else
                {
                    //next player turn
                    currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
                }
            }
            else
            {
                Console.WriteLine("Invalid move. Please choose another column.");
            }
        }
    }
}

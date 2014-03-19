using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MyFirstSnakeGame
{

    class JustSnake
    {
        //use struct to create X and Y coords of the snake elements
        struct Position
        {
            public int X;
            public int Y;

            //create constructor to build object of type Position
            public Position(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }
        //snake directions represented by array 
        static Position[] directions = new Position[]
        {
            new Position(0,1), // right direction
            new Position(0,-1), //left direction
            new Position(1,0), //down direction
            new Position(-1,0), //up direction
        };
        //current direction
        static int right = 0;
        static int left = 1;
        static int down = 2;
        static int up = 3;
        static int direction = right;


        static char foodChar = '$';
        static int points = 0;
        static int lives = 3;

        //create food for snake on random position using class Random
        static Random randomGenerator = new Random();



        static Position CreateFood()
        {

            Position food = new Position();
            food = new Position(randomGenerator.Next(0, Console.WindowWidth - 1),
            randomGenerator.Next(0, Console.WindowHeight - 1));
            Console.SetCursorPosition(food.X, food.Y);
            Console.Write(foodChar);

            return food;
        }

        //use DateTime class to make food disapeard
        static DateTime lastFoodTime = new DateTime();
        static DateTime now = new DateTime();
        static int desapeardTime = 8;

        static Queue<Position> FillQueueWithSnakeElements(Queue<Position> snakeElements)
        {
            for (int i = 0; i < 6; i++)
            {
                snakeElements.Enqueue(new Position(0, i));
            }
            return snakeElements;
        }
        static void PrintSnake(Queue<Position> snakeElements)
        {

            foreach (Position position in snakeElements)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(position.Y, position.X);
                Console.Write("*");
                Console.ResetColor();
            }
        }
        static Position[] SetObstacle()
        {
            Position[] obstacles = new Position[20];
            for (int i = 0; i < 20; i++)
            {
                obstacles[i] = new Position(randomGenerator.Next(0, Console.WindowHeight - 1),
                    randomGenerator.Next(0, Console.WindowWidth - 1));

            }
            return obstacles;

        }
        static void PrintObstacle(Position[] obstacles)
        {
            for (int i = 0; i < obstacles.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(obstacles[i].Y, obstacles[i].X);
                Console.Write("|");
                Console.ResetColor();
            }
        }
        static Score score = new Score();

        static Menu menu = new Menu();
        static void Main(string[] args)
        {
            menu.PrintMenu();
        }

        public void Engine()
        {
            //clear the scroll bars
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;

            //create Queue with x,y Position elements
            Queue<Position> snakeElements = new Queue<Position>();

            //use methods to create and print the snake elements
            PrintSnake(FillQueueWithSnakeElements(snakeElements));

            Position food = new Position();
            Console.ForegroundColor = ConsoleColor.Yellow;

            food = CreateFood();
            lastFoodTime = DateTime.Now;
            Console.ResetColor();
            Position[] obstacle = SetObstacle();
            PrintObstacle(obstacle);
            while (true)
            {
                now = DateTime.Now;
                //make sneke faster by his length
                Thread.Sleep(100 - snakeElements.Count);
                //check user input
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.RightArrow)
                    {
                        if (direction != left)
                        {
                            direction = right;
                        }
                    }
                    if (key.Key == ConsoleKey.LeftArrow)
                    {
                        if (direction != right)
                        {
                            direction = left;
                        }

                    }
                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        if (direction != down)
                        {
                            direction = up;
                        }

                    }
                    if (key.Key == ConsoleKey.DownArrow)
                    {
                        if (direction != up)
                        {
                            direction = down;
                        }

                    }
                }

                Position snakeHead = snakeElements.Last();
                Position nextDirection = directions[direction];
                Position snakeNewHead = new Position(snakeHead.X + nextDirection.X, snakeHead.Y + nextDirection.Y);

                //check if snake bite himself of bite some obstacle

                if (snakeElements.Contains(snakeNewHead) || obstacle.Contains(snakeNewHead))
                {
                    snakeElements.Dequeue();
                    lives--;
                    if (lives == 0)
                    {
                        Console.Clear();
                        Console.SetCursorPosition(Console.WindowWidth / 2 - 10, 3);
                        Console.Write("Enter your nick name:");
                        score.SaveScore(points);
                        break;
                    }
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.SetCursorPosition(Console.WindowWidth/2, 7);
                    Console.Write("Lives: {0}", lives);
                    Console.SetCursorPosition(0, 2);
                    for (int i = 0; i < 6; i++)
                    {
                        Thread.Sleep(1000);
                        Console.SetCursorPosition(0, 4);
                        Console.Write(i);
                    }
                    Console.ResetColor();
                    Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    // Console.Write("*");
                    snakeNewHead.X = 0;
                    snakeNewHead.Y = 1;
                    snakeElements.Enqueue(snakeNewHead);
                    Console.SetCursorPosition(snakeNewHead.X, snakeNewHead.Y);
                    direction = right;
                    PrintObstacle(obstacle);

                }

                if (snakeNewHead.X < 0)
                {
                    snakeNewHead.X = Console.WindowHeight - 1;
                }
                if (snakeNewHead.X >= Console.WindowHeight)
                {
                    snakeNewHead.X = 0;
                }
                if (snakeNewHead.Y < 0)
                {
                    snakeNewHead.Y = Console.WindowWidth - 1;
                }
                if (snakeNewHead.Y >= Console.WindowWidth)
                {
                    snakeNewHead.Y = 0;
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(snakeHead.Y, snakeHead.X);
                Console.Write("*");
                Console.ResetColor();
                snakeElements.Enqueue(snakeNewHead);

                Console.SetCursorPosition(snakeNewHead.Y, snakeNewHead.X);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("@");
                Console.ResetColor();


                int seconds = lastFoodTime.Second - now.Second;
                if (seconds < 0) seconds = -(seconds);
                if (seconds >= desapeardTime)
                {

                    Console.SetCursorPosition(food.X, food.Y);
                    Console.Write(' ');
                    food = CreateFood();
                    lastFoodTime = DateTime.Now;
                    seconds = 0;
                    Console.SetCursorPosition(food.X, food.Y);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(foodChar);
                    Console.ResetColor();

                    //punish point for non eated food
                    points -= 30;

                }


                //check if the snake head eat food
                if (snakeNewHead.X == food.Y && snakeNewHead.Y == food.X)
                {
                    Console.Beep(440, 50);

                    food = CreateFood();
                    lastFoodTime = DateTime.Now;
                    seconds = 0;
                    if (snakeElements.Contains(food) || obstacle.Contains(food))
                    {
                        Console.SetCursorPosition(food.X, food.Y);
                        Console.Write(' ');
                        PrintObstacle(obstacle);
                        while (snakeElements.Contains(food))
                        {
                            food = CreateFood();
                            lastFoodTime = DateTime.Now;
                            seconds = 0;
                        }
                    }
                    Console.SetCursorPosition(food.X, food.Y);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(foodChar);
                    Console.ResetColor();
                    points += 100;


                }
                else
                {
                    Position last = snakeElements.Dequeue();
                    Console.SetCursorPosition(last.Y, last.X);
                    Console.Write(' ');
                }

            }
        }
    }

}

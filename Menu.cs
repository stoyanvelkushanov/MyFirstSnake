using System;

namespace MyFirstSnakeGame
{
    class Menu
    {
        JustSnake snake = new JustSnake();
        Score score = new Score();
        private int ArrowXCoord = Console.WindowWidth / 2 - 8;
        private int ArrowYCoord = 5;
        private int maxYCoord = 8;
        private int minYCoord = 5;
        private string arrow = "==>";
        private int currentArrow = 1;

        public void PrintMenu()
        {

            PrintArrow();
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, 3);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Snake Game");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, 5);
            Console.Write("New Game");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, 6);
            Console.Write("High Score");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, 7);
            Console.Write("Help");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, 8);
            Console.Write("Exit");
            Console.ResetColor();
            MoveArrow();

        }
        private void MoveArrow()
        {

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();

                if (key.Key == ConsoleKey.UpArrow && ArrowYCoord > minYCoord)
                {
                    currentArrow--;
                    MoveArrowUp();
                    Console.SetCursorPosition(0, 0);
                }
                if (key.Key == ConsoleKey.DownArrow && ArrowYCoord < maxYCoord)
                {

                    MoveArrowDown();
                    currentArrow++;
                    Console.SetCursorPosition(0, 0);
                }
                if (key.Key == ConsoleKey.Enter)
                {

                    Console.Beep(440, 150);
                    int commands = ArrowYCoord;
                    switch (commands)
                    {
                        case 5: Console.Clear(); snake.Engine(); break;
                        case 6: Console.Clear(); score.PrintHighScore(); break;
                        case 7: Console.Clear(); Help(); break;
                        case 8: Environment.Exit(0); break;
                        default:
                            break;
                    }
                }

            }
        }
        private void Help()
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 10, 1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("HELLO SNAKE PLAYER");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 23, 3);
            Console.Write("Follow the simple instructions to play the game!");
            Console.SetCursorPosition(3, 6);
            Console.Write("1 - Use left,right,up and down arrow for snake direction");
            Console.SetCursorPosition(3, 7);
            Console.Write("2 - Your game will start with 3 lives");
            Console.SetCursorPosition(3, 8);
            Console.Write("3 - This is food symbol $ try to eat it!");
            Console.SetCursorPosition(3, 9);
            Console.Write("4 - Food will disapeard after 8 secs");
            Console.SetCursorPosition(3, 10);
            Console.Write("5 - Eat food to grow up and win points");
            Console.SetCursorPosition(3, 11);
            Console.Write("6 - If you miss to eat the food you get -30 punish poins");
            Console.SetCursorPosition(3, 12);
            Console.Write("7 - On the field have obstacles caferul");
            Console.SetCursorPosition(3, 13);
            Console.WriteLine("8 - You will start with 3 lives");
            Console.SetCursorPosition(3, 14);
            Console.Write("9 - if you run out of lives your Game Is Over!!!");
            Console.SetCursorPosition(15, 18);
            Console.Write("WE WISH YOU A NICE GAME :)");
            Console.SetCursorPosition(15, 22);

            Console.Write("Press enter to back in the menu");
            ConsoleKeyInfo key = Console.ReadKey();
            Menu menu = new Menu();
            Console.ResetColor();
            if (key.Key == ConsoleKey.Enter)
            {

                Console.Clear();
                menu.PrintMenu();
            }


        }


        public void PrintArrow()
        {
            for (int i = ArrowXCoord; i < arrow.Length + ArrowXCoord; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(i, ArrowYCoord);
                Console.Write(arrow[i - ArrowXCoord]);
                Console.ResetColor();
            }
        }
        public void MoveArrowDown()
        {
            Console.Beep(320, 150);
            ArrowYCoord++;
            Console.SetCursorPosition(ArrowXCoord, ArrowYCoord);
            PrintArrow();
            Console.SetCursorPosition(ArrowXCoord, ArrowYCoord - 1);
            Console.Write(' ');
            Console.SetCursorPosition(ArrowXCoord + 1, ArrowYCoord - 1);
            Console.Write(' ');
            Console.SetCursorPosition(ArrowXCoord + 2, ArrowYCoord - 1);
            Console.Write(' ');
        }
        public void MoveArrowUp()
        {
            Console.Beep(320, 150);
            ArrowYCoord--;
            Console.SetCursorPosition(ArrowXCoord, ArrowYCoord);
            PrintArrow();
            Console.SetCursorPosition(ArrowXCoord, ArrowYCoord + 1);
            Console.Write(' ');
            Console.SetCursorPosition(ArrowXCoord + 1, ArrowYCoord + 1);
            Console.Write(' ');
            Console.SetCursorPosition(ArrowXCoord + 2, ArrowYCoord + 1);
            Console.Write(' ');
        }

    }
}

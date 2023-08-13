using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Catch_the_Square.Squares;

namespace Catch_the_Square
{
    class Program
    {
        private const string windowTitle = "Catch the Square";
        private const uint windowWidth = 800;
        private const uint windowHeight = 600;
        private const uint framerateLimit = 60;
        private const string fontFilepath = "comic.ttf";
        private const uint allySquareNumber = 10;
        private const uint enemySquareNumber = 10;
        private const float squareSize = 100;
        private const uint squareStartSpeed = 5;
        
        private static int score = 0;
        private static int highScore = 0;

        private static void Main()
        {
            RenderWindow window = new RenderWindow(new VideoMode(windowWidth, windowHeight), windowTitle);
            window.SetFramerateLimit(framerateLimit);
            window.Closed += Window_Closed;
            Font font = new Font(fontFilepath);
            List<Square> squares = SquareController.Initialize(allySquareNumber, enemySquareNumber, squareSize, squareStartSpeed, window);
            HeadUpDisplay HUD = new HeadUpDisplay(window, font);
            while (window.IsOpen)
            {
                window.Clear(new Color(230, 230, 230));
                window.DispatchEvents();
                SquareController.Pressed pressed = SquareController.Update(Mouse.GetPosition(window), squares);
                if (Keyboard.IsKeyPressed(Keyboard.Key.R) || pressed == SquareController.Pressed.Red)
                {
                    squares.Clear();
                    squares = SquareController.Initialize(allySquareNumber, enemySquareNumber, squareSize, squareStartSpeed, window);
                    if (score > highScore) highScore = score;
                    score = 0;
                }
                else if (pressed == SquareController.Pressed.Black) score++;
                HUD.Draw(window, score, highScore);
                SquareController.Draw(window, squares);
                window.Display();
            }
        }

        private static void Window_Closed(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Close();
        }
    }
}
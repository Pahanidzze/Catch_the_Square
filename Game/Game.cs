using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using Catch_the_Square.Squares;

namespace Catch_the_Square.Game
{
    class Game
    {
        private List<Square> squares;
        private HeadUpDisplay HUD;

        private static int score = 0;
        private static int highScore = 0;

        public Game(Window window, Font font)
        {
            squares = SquareController.Initialize(window);
            HUD = new HeadUpDisplay(window, font);
        }

        public void Update(RenderWindow window)
        {
            SquareController.Pressed pressed = SquareController.Update(Mouse.GetPosition(window), squares, window);
            if ((int)pressed > 0) Audio.PlaySound(Config.actionSoundFilepath, 30);
            if (Keyboard.IsKeyPressed(Keyboard.Key.R) || pressed == SquareController.Pressed.Red)
            {
                Restart(window);
            }
            else if (pressed == SquareController.Pressed.Black) score++;
            HUD.Draw(window, score, highScore);
            SquareController.Draw(window, squares);
        }

        private void Restart(Window window)
        {
            squares.Clear();
            squares = SquareController.Initialize(window);
            if (score > highScore) highScore = score;
            score = 0;
        }
    }
}

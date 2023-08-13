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
    static class SquareController
    {
        private static Pressed clickStatus = Pressed.Not;

        public static List<Square> Initialize(uint allySquareNumber, uint enemySquareNumber, float squareSize, uint squareStartSpeed, Window window)
        {
            List<Square> squares = new List<Square>();
            for (int i = 0; i < allySquareNumber; i++)
            {
                squares.Add(new AllySquare(new RectangleShape(new Vector2f(squareSize, squareSize)), squareStartSpeed, new IntRect(0, 0, (int)window.Size.X, (int)window.Size.Y)));
            }
            for (int i = 0; i < enemySquareNumber; i++)
            {
                squares.Add(new EnemySquare(new RectangleShape(new Vector2f(squareSize, squareSize)), squareStartSpeed, new IntRect(0, 0, (int)window.Size.X, (int)window.Size.Y)));
            }
            return squares;
        }

        public static Pressed Update(Vector2i mousePosition, List<Square> squares)
        {
            Pressed result = Pressed.Field;
            for (int i = squares.Count - 1; i >= 0; i--)
            {
                if (Mouse.IsButtonPressed(Mouse.Button.Left) && clickStatus == Pressed.Not)
                {
                    clickStatus = (Pressed)squares[i].ClickCheck(mousePosition);
                    result = clickStatus;
                    if (!squares[i].active) RespawnAlly(squares, i);
                }
                else if (!Mouse.IsButtonPressed(Mouse.Button.Left)) clickStatus = Pressed.Not;
                squares[i].Move();
            }
            if (Mouse.IsButtonPressed(Mouse.Button.Left)) clickStatus = Pressed.Field;
            return result;
        }

        public static void Draw(RenderWindow window, List<Square> squares)
        {
            for (int i = 0; i < squares.Count; i++)
            {
                squares[i].Draw(window);
            }
        }

        private static void RespawnAlly(List<Square> squares, int index)
        {
            Vector2f size = new Vector2f(squares[index].initSize.X, squares[index].initSize.Y);
            uint speed = squares[index].speed;
            IntRect bounds = squares[index].bounds;
            squares.RemoveAt(index);
            squares.Insert(index, new AllySquare(new RectangleShape(size), speed, bounds));
        }

        public enum Pressed
        {
            Field = -1,
            Not = 0,
            Black = 1,
            Red = 2
        }
    }
}

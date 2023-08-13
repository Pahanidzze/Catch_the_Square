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
        private const float BonusSquareSpawnChance = 1 / 600f;
        private const uint allySquareNumber = 10;
        private const uint enemySquareNumber = 10;
        private const float squareSize = 100;
        private const uint squareStartSpeed = 5;

        private static List<Square> squareList;

        private static Pressed clickStatus = Pressed.Not;

        public static List<Square> Initialize(Window window)
        {
            squareList = new List<Square>();
            for (int i = 0; i < allySquareNumber; i++)
            {
                squareList.Add(new AllySquare(new RectangleShape(new Vector2f(squareSize, squareSize)), squareStartSpeed, new IntRect(0, 0, (int)window.Size.X, (int)window.Size.Y)));
            }
            for (int i = 0; i < enemySquareNumber; i++)
            {
                squareList.Add(new EnemySquare(new RectangleShape(new Vector2f(squareSize, squareSize)), squareStartSpeed, new IntRect(0, 0, (int)window.Size.X, (int)window.Size.Y)));
            }
            return squareList;
        }

        public static Pressed Update(Vector2i mousePosition, List<Square> squares, Window window)
        {
            squareList = squares;
            Pressed result = Pressed.Field;
            for (int i = squareList.Count - 1; i >= 0; i--)
            {
                if (Mouse.IsButtonPressed(Mouse.Button.Left) && clickStatus == Pressed.Not && squareList[i].active)
                {
                    clickStatus = (Pressed)squareList[i].ClickCheck(mousePosition);
                    result = clickStatus;
                    if (clickStatus == Pressed.Blue) EnemyDecrease();
                    else if (clickStatus == Pressed.Yellow) EnemySlowdown();
                }
                else if (!Mouse.IsButtonPressed(Mouse.Button.Left)) clickStatus = Pressed.Not;
                squareList[i].Move();
                if (!squareList[i].active) i += UpdateIncative(i);
            }
            if (Mouse.IsButtonPressed(Mouse.Button.Left)) clickStatus = Pressed.Field;
            SpawnBonus(window);
            Console.WriteLine(squareList.Count());
            return result;
        }

        public static void Draw(RenderWindow window, List<Square> squares)
        {
            squareList = squares;
            for (int i = 0; i < squareList.Count; i++)
            {
                squareList[i].Draw(window);
            }
        }

        private static int UpdateIncative(int index)
        {
            int result = 0;
            if (squareList[index].tag == Square.Tag.Black) RespawnAlly(index);
            else if (squareList[index].tag == Square.Tag.Blue)
            {
                if (squareList[index].timer > 0) squareList[index].Countdown();
                else
                {
                    if (squareList[index].timer == 0) EnemyIncrease();
                    squareList.RemoveAt(index);
                    result = 1;
                }
            }
            else if (squareList[index].tag == Square.Tag.Yellow)
            {
                if (squareList[index].timer > 0) squareList[index].Countdown();
                else
                {
                    EnemyAccelerate();
                    squareList.RemoveAt(index);
                    result = 1;
                }
            }
            return result;
        }

        private static void SpawnBonus(Window window)
        {
            if (Mathf.rand.Next((int)(1 / BonusSquareSpawnChance)) == 0)
            {
                if (Mathf.rand.Next(2) == 0)
                {
                    squareList.Insert(0, new BonusSquare(new RectangleShape(new Vector2f(squareSize, squareSize)), squareStartSpeed,
                        new IntRect(0, 0, (int)window.Size.X, (int)window.Size.Y), Square.BonusTag.Blue));
                }
                else
                {
                    squareList.Insert(0, new BonusSquare(new RectangleShape(new Vector2f(squareSize, squareSize)), squareStartSpeed,
                        new IntRect(0, 0, (int)window.Size.X, (int)window.Size.Y), Square.BonusTag.Yellow));
                }
            }
        }

        private static void RespawnAlly(int index)
        {
            Vector2f size = new Vector2f(squareList[index].initSize.X, squareList[index].initSize.Y);
            uint speed = squareList[index].speed;
            IntRect bounds = squareList[index].bounds;
            squareList.RemoveAt(index);
            squareList.Insert(index, new AllySquare(new RectangleShape(size), speed, bounds));
        }

        private static void EnemyDecrease()
        {
            for (int i = 0; i < squareList.Count; i++)
            {
                if (squareList[i].tag == Square.Tag.Red) squareList[i].Decrease();
            }
        }

        private static void EnemyIncrease()
        {
            for (int i = 0; i < squareList.Count; i++)
            {
                if (squareList[i].tag == Square.Tag.Red) squareList[i].Increase();
            }
        }

        private static void EnemySlowdown()
        {
            for (int i = 0; i < squareList.Count; i++)
            {
                if (squareList[i].tag == Square.Tag.Red) squareList[i].Slowdown();
            }
        }

        private static void EnemyAccelerate()
        {
            for (int i = 0; i < squareList.Count; i++)
            {
                if (squareList[i].tag == Square.Tag.Red) squareList[i].Accelerate();
            }
        }

        public enum Pressed
        {
            Field = -1,
            Not = 0,
            Black = 1,
            Red = 2,
            Blue = 3,
            Yellow = 4
        }
    }
}

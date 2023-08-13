using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Catch_the_Square.Squares
{
    class Square
    {
        public bool active { get; protected set; }
        public uint speed { get; protected set; }
        public IntRect bounds { get; protected set; }
        public Vector2f initSize { get; protected set; }
        protected RectangleShape shape;
        protected Vector2f destination;
        protected Tag tag;

        protected static readonly Vector2f minSize = new Vector2f(40, 40);

        public void Move()
        {
            Vector2f vector = destination - shape.Position;
            float vectorLength = (float)Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2));
            if (vectorLength <= speed)
            {
                shape.Position = destination;
                UpdateDestination();
            }
            else
            {
                float speedMultiplier = 1 / vectorLength;
                shape.Position = new Vector2f(shape.Position.X + vector.X * speedMultiplier * speed, shape.Position.Y + vector.Y * speedMultiplier * speed);
            }
        }

        public void Draw(RenderWindow win)
        {
            if (!active) return;
            win.Draw(shape);
        }

        protected void UpdateDestination()
        {
            destination.X = Mathf.rand.Next(bounds.Left, bounds.Width - (int)shape.Size.X);
            destination.Y = Mathf.rand.Next(bounds.Top, bounds.Height - (int)shape.Size.Y);
        }

        protected void UpdateBonusDestination()
        {
            destination.X = Mathf.rand.Next(bounds.Left, bounds.Width - (int)shape.Size.X);
            destination.Y = Mathf.rand.Next(bounds.Top, bounds.Height - (int)shape.Size.Y);
        }

        public Tag ClickCheck(Vector2i mousePosition)
        {
            Tag result = Tag.Nothing;
            if (active)
            {
                if (mousePosition.X >= shape.Position.X && mousePosition.X <= shape.Position.X + shape.Size.X &&
                    mousePosition.Y >= shape.Position.Y && mousePosition.Y <= shape.Position.Y + shape.Size.Y)
                {
                    result = Click();
                }
            }
            return result;
        }

        protected virtual Tag Click() { return Tag.Black; }

        public enum Tag
        {
            Nothing = 0,
            Black = 1,
            Red = 2,
            Blue = 3,
            Yellow = 4
        }
    }
}

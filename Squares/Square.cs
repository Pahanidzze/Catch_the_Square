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
        public Tag tag { get; protected set; }
        public int timer { get; protected set; }
        protected RectangleShape shape;
        protected Vector2f destination;

        protected static readonly Vector2f minSize = new Vector2f(40, 40);

        public void Move()
        {
            Vector2f vector = destination - shape.Position;
            float vectorLength = (float)Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2));
            if (vectorLength <= speed)
            {
                if (tag == Tag.Black || tag == Tag.Red)
                {
                    shape.Position = destination;
                    UpdateDestination();
                }
                else if (active)
                {
                    active = false;
                    timer = -1;
                }
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

        protected Vector2f InitializePosition()
        {
            Vector2f shapeCenter = new Vector2f(shape.Size.X / 2, shape.Size.Y / 2);
            return new Vector2f(bounds.Left + bounds.Width / 2 - shapeCenter.X, bounds.Top + bounds.Height / 2 - shapeCenter.Y);
        }

        protected Vector2f InitializeBonusPosition(Border borderPosition)
        {
            Vector2f position;
            if (borderPosition == Border.Top)
            {
                position = new Vector2f(Mathf.rand.Next(bounds.Left, (int)(bounds.Left + bounds.Width - shape.Size.X)), bounds.Top - shape.Size.Y);
            }
            else if (borderPosition == Border.Bottom)
            {
                position = new Vector2f(Mathf.rand.Next(bounds.Left, (int)(bounds.Left + bounds.Width - shape.Size.X)), bounds.Top + bounds.Height);
            }
            else if (borderPosition == Border.Left)
            {
                position = new Vector2f(bounds.Left - shape.Size.X, Mathf.rand.Next(bounds.Top, (int)(bounds.Top + bounds.Height - shape.Size.Y)));
            }
            else
            {
                position = new Vector2f(bounds.Left + bounds.Width, Mathf.rand.Next(bounds.Top, (int)(bounds.Top + bounds.Height - shape.Size.Y)));
            }
            return position;
        }

        protected void UpdateDestination()
        {
            destination.X = Mathf.rand.Next(bounds.Left, bounds.Left + bounds.Width - (int)shape.Size.X);
            destination.Y = Mathf.rand.Next(bounds.Top, bounds.Top + bounds.Height - (int)shape.Size.Y);
        }

        protected void UpdateBonusDestination(Border borderPosition)
        {
            Border borderDestination = borderPosition;
            while (borderDestination == borderPosition)
            {
                borderDestination = (Border)Mathf.rand.Next(4);
            }
            destination = InitializeBonusPosition(borderDestination);
        }

        public void Slowdown()
        {
            speed /= 2;
        }

        public void Accelerate()
        {
            speed *= 2;
        }

        public void Decrease()
        {
            shape.Size /= 2;
        }

        public void Increase()
        {
            shape.Size *= 2;
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

        protected virtual Tag Click() { return tag; }
        public virtual void Countdown() { }

        public enum Tag
        {
            Nothing = 0,
            Black = 1,
            Red = 2,
            Blue = 3,
            Yellow = 4
        }

        public enum BonusTag
        {
            Blue = 3,
            Yellow = 4
        }

        public enum Border
        {
            Top = 0,
            Bottom = 1,
            Left = 2,
            Right = 3
        }
    }
}

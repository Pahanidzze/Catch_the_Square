using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace Catch_the_Square.Squares
{
    class AllySquare : Square
    {
        public AllySquare(RectangleShape shape, uint speed, IntRect bounds)
        {
            active = true;
            initSize = new Vector2f(shape.Size.X, shape.Size.Y);
            this.shape = shape;
            if (shape.Size.X < minSize.X || shape.Size.Y < minSize.Y) shape.Size = minSize;
            this.speed = speed;
            this.bounds = bounds;
            this.shape.FillColor = Color.Black;
            tag = Tag.Black;
            this.shape.Position = InitializePosition();
            UpdateDestination();
        }

        protected override Tag Click()
        {
            shape.Size -= new Vector2f(20, 20);
            shape.Position = new Vector2f(Mathf.rand.Next(bounds.Left, bounds.Left + bounds.Width - (int)shape.Size.Y), 
                Mathf.rand.Next(bounds.Top, bounds.Top + bounds.Height - (int)shape.Size.X));
            if (shape.Size.X < minSize.X || shape.Size.Y < minSize.Y) active = false;
            return tag;
        }
    }
}

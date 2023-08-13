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
    class EnemySquare : Square
    {
        public EnemySquare(RectangleShape shape, uint speed, IntRect bounds)
        {
            active = true;
            initSize = new Vector2f(shape.Size.X, shape.Size.Y);
            this.shape = shape;
            if (shape.Size.X < minSize.X || shape.Size.Y < minSize.Y) shape.Size = minSize;
            this.speed = speed;
            this.bounds = bounds;
            this.shape.Position = new Vector2f((bounds.Width - bounds.Left - shape.Size.X / 2) / 2, (bounds.Height - bounds.Top - shape.Size.Y / 2) / 2);
            this.shape.FillColor = Color.Red;
            tag = Tag.Red;
            UpdateDestination();
        }
        
        protected override Tag Click()
        {
            return Tag.Red;
        }
    }
}

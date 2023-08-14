using SFML.Graphics;
using SFML.System;

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
            this.shape.FillColor = Color.Red;
            tag = Tag.Red;
            this.shape.Position = InitializePosition();
            UpdateDestination();
        }
        
        protected override Tag Click()
        {
            return tag;
        }
    }
}

using SFML.Graphics;
using SFML.System;

namespace Catch_the_Square.Squares
{
    class BonusSquare : Square
    {
        public BonusSquare(RectangleShape shape, uint speed, IntRect bounds, Square.BonusTag tag)
        {
            timer = 300;
            active = true;
            initSize = new Vector2f(shape.Size.X, shape.Size.Y);
            this.shape = shape;
            if (shape.Size.X < minSize.X || shape.Size.Y < minSize.Y) shape.Size = minSize;
            this.speed = speed;
            this.bounds = bounds;
            if (tag == BonusTag.Blue) this.shape.FillColor = Color.Blue;
            else this.shape.FillColor = Color.Yellow;
            this.tag = (Tag)tag;
            Border borderPosition = (Border)Mathf.rand.Next(4);
            this.shape.Position = InitializeBonusPosition(borderPosition);
            UpdateBonusDestination(borderPosition);
        }

        protected override Tag Click()
        {
            active = false;
            return tag;
        }

        public override void Countdown()
        {
            if (timer > 0) timer--;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Catch_the_Square
{
    class HeadUpDisplay
    {
        private Text instructionText;
        private Text retryInstructionText;
        private Text scoreText;
        private Text highScoreText;

        private const string scoreTextPreparation = "Очки: ";
        private const string highScoreTextPreparation = "Рекорд: ";

        public HeadUpDisplay(Window window, Font font)
        {
            instructionText = new Text("Нажимайте мышкой на черные квадраты", font, 24)
            {
                Position = new Vector2f(10, 10),
                FillColor = Color.Black
            };
            retryInstructionText = new Text("Для перезапуска нажмите \"R\"", font, 24)
            {
                Position = new Vector2f(10, 40),
                FillColor = Color.Black
            };
            scoreText = new Text("", font, 24)
            {
                Position = new Vector2f(10, window.Size.Y - 64),
                FillColor = Color.Black
            };
            highScoreText = new Text("", font, 24)
            {
                Position = new Vector2f(10, window.Size.Y - 34),
                FillColor = Color.Black
            };
        }

        public void Draw(RenderWindow win, int score, int highScore)
        {
            win.Draw(instructionText);
            win.Draw(retryInstructionText);
            scoreText.DisplayedString = scoreTextPreparation + score;
            win.Draw(scoreText);
            highScoreText.DisplayedString = highScoreTextPreparation + highScore;
            win.Draw(highScoreText);
        }
    }
}

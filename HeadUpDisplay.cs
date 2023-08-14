using System;
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

        public HeadUpDisplay(Window window, Font font)
        {
            instructionText = new Text(Config.instructionTextPreparation, font, Config.HUDCharacterSize)
            {
                Position = new Vector2f(10, 10),
                FillColor = Color.Black
            };
            retryInstructionText = new Text(Config.retryInstructionTextPreparation, font, Config.HUDCharacterSize)
            {
                Position = new Vector2f(10, 40),
                FillColor = Color.Black
            };
            scoreText = new Text("", font, Config.HUDCharacterSize)
            {
                Position = new Vector2f(10, window.Size.Y - (40 + Config.HUDCharacterSize)),
                FillColor = Color.Black
            };
            highScoreText = new Text("", font, Config.HUDCharacterSize)
            {
                Position = new Vector2f(10, window.Size.Y - (10 + Config.HUDCharacterSize)),
                FillColor = Color.Black
            };
        }

        public void Draw(RenderWindow win, int score, int highScore)
        {
            win.Draw(instructionText);
            win.Draw(retryInstructionText);
            scoreText.DisplayedString = Config.scoreTextPreparation + score;
            win.Draw(scoreText);
            highScoreText.DisplayedString = Config.highScoreTextPreparation + highScore;
            win.Draw(highScoreText);
        }
    }
}

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
    static class Program
    {
        private static void Main()
        {
            RenderWindow window = new RenderWindow(new VideoMode(Config.windowWidth, Config.windowHeight), Config.windowTitle);
            window.SetFramerateLimit(Config.framerateLimit);
            window.Closed += Window_Closed;
            Font font = new Font(Config.fontFilepath);
            Audio.PlayMusic(Config.musicFilepath, 30);
            Game game = new Game(window, font);
            while (window.IsOpen)
            {
                window.Clear(new Color(230, 230, 230));
                window.DispatchEvents();
                game.Update(window);
                window.Display();
            }
        }

        private static void Window_Closed(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Close();
        }
    }
}
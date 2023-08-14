using System;
using SFML.Graphics;
using SFML.Window;

namespace Catch_the_Square
{
    static class Program
    {
        private static void Main()
        {
            RenderWindow window = new RenderWindow(new VideoMode(Config.windowWidth, Config.windowHeight), Config.windowTitle);
            window.SetFramerateLimit(Config.framerateLimit);
            window.Closed += Window_Closed;
            Font comic = new Font(Config.comicFontFilepath);
            Font arial = new Font(Config.arialFontFilepath);Audio.PlayMusic(Config.musicFilepath, 30);
            Game.Game game = new Game.Game(window, comic);
            while (window.IsOpen)
            {
                window.Clear(new Color(Config.backgroundR, Config.backgroundG, Config.backgroundB));
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
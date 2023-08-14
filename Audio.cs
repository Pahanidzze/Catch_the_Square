using SFML.Audio;

namespace Catch_the_Square
{
    class Audio
    {
        public static void PlaySound(string path, float volume)
        {
            Sound sound = new Sound(new SoundBuffer(path))
            {
                Volume = volume
            };
            sound.Play();
        }

        public static void PlayMusic(string path, float volume)
        {
            Music music = new Music(path)
            {
                Volume = volume
            };
            music.Play();
        }
    }
}

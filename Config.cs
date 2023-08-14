namespace Catch_the_Square
{
    public struct Config
    {
        public const byte backgroundR = 230;
        public const byte backgroundG = 230;
        public const byte backgroundB = 230;

        public const string windowTitle = "Catch the Square";
        public const uint windowWidth = 800;
        public const uint windowHeight = 600;
        public const uint framerateLimit = 60;
        public const string comicFontFilepath = "comic.ttf";
        public const string arialFontFilepath = "arial.ttf";
        public const string musicFilepath = "bg_music.wav";
        public const string actionSoundFilepath = "ActionSound.wav";

        public const float BonusSquareSpawnChance = 1 / 600f;
        public const uint allySquareNumber = 10;
        public const uint enemySquareNumber = 10;
        public const float squareSize = 100;
        public const uint squareStartSpeed = 5;

        public const string instructionTextPreparation = "Нажимайте мышкой на черные квадраты";
        public const string retryInstructionTextPreparation = "Для перезапуска нажмите \"R\"";
        public const string scoreTextPreparation = "Очки: ";
        public const string highScoreTextPreparation = "Рекорд: ";
        public const uint HUDCharacterSize = 24;
    }
}

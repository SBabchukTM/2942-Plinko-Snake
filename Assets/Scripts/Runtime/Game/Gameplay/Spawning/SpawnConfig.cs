namespace Runtime.Game.Gameplay.Spawning
{
    public static class SpawnConfig
    {
        public const float Width = 30;
        public const float Height = 100;
    
        public const float MaxAdditionalGroupHeight = 40;
    
        public const int MinNodes = 1;
        public const int MaxNodes = 5;

        public const int MinCoins = 1;
        public const int MaxCoins = 4;
    
        public const int MinObstacles = 1;
        public const int MaxObstacles = 3;

        public const float BombChance = 0.2f;
        public const float GhostChance = 0.3f;
        public const float MagnetChance = 0.25f;
        public const float RocketChance = 0.15f;
    }
}
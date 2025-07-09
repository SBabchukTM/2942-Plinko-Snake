using System;

namespace Runtime.Game.Achievements
{
    public static class AccomplishmentsEventInvoker
    {
        public static event Action OnGameFinished;
        public static event Action OnCoinPicked;
        public static event Action<int> OnLevelReached;
        public static event Action OnBallPurchased;
        public static event Action OnBackgroundPurchased;
        public static event Action OnDailyBonus;
        public static event Action OnMagnetUsed;
        public static event Action OnRocketUsed;
        public static event Action OnBombUsed;
        public static event Action OnGhostUsed;
        public static event Action<int> OnSnakeLengthReached;
    
        public static void InvokeOnGameFinished() => OnGameFinished?.Invoke();
        public static void InvokeOnCoinPicked() => OnCoinPicked?.Invoke();
        public static void InvokeOnLevelReached(int level) => OnLevelReached?.Invoke(level);
        public static void InvokeOnBallPurchased() => OnBallPurchased?.Invoke();
        public static void InvokeOnBackgroundPurchased() => OnBackgroundPurchased?.Invoke();
        public static void InvokeOnDailyBonus() => OnDailyBonus?.Invoke();
        public static void InvokeOnMagnetUsed() => OnMagnetUsed?.Invoke();
        public static void InvokeOnRocketUsed() => OnRocketUsed?.Invoke();
        public static void InvokeOnBombUsed() => OnBombUsed?.Invoke();
        public static void InvokeOnGhostUsed() => OnGhostUsed?.Invoke();
        public static void InvokeOnSnakeLengthReached(int length) => OnSnakeLengthReached?.Invoke(length);
    }
}

using Runtime.Game.Services.UI;
using UnityEngine;

namespace Runtime.Game.Services.Pause
{
    public static class PauseHelper
    {
        private static bool _pausedFromPauseTaker = false;
        
        public static void TakePause(GameState gameState)
        {
            if (Time.timeScale == 1 && gameState == GameState.Paused)
                _pausedFromPauseTaker = true;

            if (_pausedFromPauseTaker)
                Time.timeScale = gameState == GameState.Running ? 1 : 0;
            
            if(gameState == GameState.Running)
                _pausedFromPauseTaker = false;
        }
    }
}
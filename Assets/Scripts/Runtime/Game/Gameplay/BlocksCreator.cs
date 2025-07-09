using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Runtime.Game.Gameplay
{
    public class BlocksCreator
    {
        private const int BlocksCount = 5;
        private readonly GameData _gameData;
        private Random _random;

        public BlocksCreator(GameData gameData)
        {
            _gameData = gameData;
            _random = new Random();
        }

        public List<int> CreatePenaltiesList()
        {
            List<int> penalties = new(BlocksCount);
            int snakeLength = _gameData.SnakeLength;

            if(snakeLength == 0)
                return penalties;
            
            int guaranteedPenalty = _random.Next(1, Math.Max(2, snakeLength - 1));
            penalties.Add(guaranteedPenalty);
            
            Init1(snakeLength, penalties);

            Init2(penalties);

            return penalties;
        }

        private void Init2(List<int> penalties)
        {
            for (int i = penalties.Count - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                (penalties[i], penalties[j]) = (penalties[j], penalties[i]);
            }
        }

        private void Init1(int snakeLength, List<int> penalties)
        {
            for (int i = 1; i < BlocksCount; i++)
            {
                int min = 1;      
                int max = (int)(snakeLength * 1.5f);
                int penalty = _random.Next(min, max);
                penalties.Add(penalty);
            }
        }
    }
}
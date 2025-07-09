using System.Collections.Generic;
using Runtime.Game.Gameplay.Misc;
using Runtime.Game.Gameplay.Spawning.Pools;
using Runtime.Game.Gameplay.Systems;
using UnityEngine;
using Zenject;

namespace Runtime.Game.Gameplay.Spawning
{
    public class Spawner : GameSystem, ITickable
    {
        private const float YSpawnPos = 40;
    
        private readonly GameData _gameData;
        private readonly BlockRowPool _blockRowPool;
        private readonly BombPool _bombPool;
        private readonly CoinPool _coinPool;
        private readonly CollectibleNodesPool _collectibleNodesPool;
        private readonly GhostPool _ghostPool;
        private readonly MagnetsPool _magnetsPool;
        private readonly RocketPool _rocketPool;
        private readonly ObstaclePool _obstaclePool;
        private readonly GameBgScroller _gameBgScroller;

        private float _nextGroupY;

        public Spawner(ManagerOfSystems manager, GameData gameData,
            BlockRowPool blockRowPool, BombPool bombPool,
            CoinPool coinPool, CollectibleNodesPool collectibleNodesPool, GhostPool ghostPool, MagnetsPool magnetsPool,
            RocketPool rocketPool, ObstaclePool obstaclePool, GameBgScroller gameBgScroller) : base(manager)
        {
            _gameData = gameData;
            _blockRowPool = blockRowPool;
            _bombPool = bombPool;
            _coinPool = coinPool;
            _collectibleNodesPool = collectibleNodesPool;
            _ghostPool = ghostPool;
            _magnetsPool = magnetsPool;
            _rocketPool = rocketPool;
            _obstaclePool = obstaclePool;
            _gameBgScroller = gameBgScroller;
        }

        public override void Reset()
        {
            _nextGroupY = SpawnConfig.Height;
        }

        public void Tick()
        {
            if (!Enabled)
                return;

            if (_gameData.TotalDistanceTravelled >= _nextGroupY)
            {
                UpdateNext();
            }
        }

        private void UpdateNext()
        {
            float spawnHeight = (SpawnConfig.Height + Random.Range(0, SpawnConfig.MaxAdditionalGroupHeight));
            SpawnGroup(YSpawnPos, spawnHeight);
            _nextGroupY += spawnHeight;
        }

        private void SpawnGroup(float yBase, float spawnHeight)
        {
            List<Bounds> placedItemBounds = new();

            SpawnBlockRow(yBase, placedItemBounds);

            int nodes = Random.Range(SpawnConfig.MinNodes, SpawnConfig.MaxNodes);
            for(int i = 0; i < nodes; i++)
                TryPlaceItem(_collectibleNodesPool, yBase, spawnHeight, placedItemBounds);

            PlaceRandom(yBase, spawnHeight, placedItemBounds);
        }

        private void PlaceRandom(float yBase, float spawnHeight, List<Bounds> placedItemBounds)
        {
            int coins = Random.Range(SpawnConfig.MinCoins, SpawnConfig.MaxCoins);
            for(int i = 0; i < coins; i++)
                TryPlaceItem(_coinPool, yBase, spawnHeight, placedItemBounds);
        
            int obstacles = Random.Range(SpawnConfig.MinObstacles, SpawnConfig.MaxObstacles);
            for(int i = 0; i < obstacles; i++)
                TryPlaceItem(_obstaclePool, yBase, spawnHeight, placedItemBounds);
        
            if(Random.value < SpawnConfig.BombChance)
                TryPlaceItem(_bombPool, yBase, spawnHeight, placedItemBounds);
        
            if(Random.value < SpawnConfig.GhostChance)
                TryPlaceItem(_ghostPool, yBase, spawnHeight, placedItemBounds);
        
            if(Random.value < SpawnConfig.MagnetChance)
                TryPlaceItem(_magnetsPool, yBase, spawnHeight, placedItemBounds);
        
            if(Random.value < SpawnConfig.RocketChance)
                TryPlaceItem(_rocketPool, yBase, spawnHeight, placedItemBounds);
        }

        private void SpawnBlockRow(float y, List<Bounds> placedBounds)
        {
            var row = _blockRowPool.GetItem();
            row.transform.position = new Vector3(0, y, 0);
            _gameBgScroller.AddContent(row.transform);
        
            var itemSize = _blockRowPool.GetItemSize();
            var newBounds = new Bounds(new Vector3(0, y, 0), itemSize);
            placedBounds.Add(newBounds);
        }

        private void TryPlaceItem<T>(BaseItemPool<T> pool, float yBase, float spawnHeight, List<Bounds> placedBounds) where T : Component
        {
            var itemSize = pool.GetItemSize();

            float areaWidth = SpawnConfig.Width;
            float areaHeight = spawnHeight;

            int attemptsMax = 100;
        
            for (var attempt = 0; attempt < attemptsMax; attempt++)
            {
                var x = Random.Range(-areaWidth / 2f + itemSize.x / 2f, areaWidth / 2f - itemSize.x / 2f);
                var y = yBase + Random.Range(0f, areaHeight - itemSize.y);

                var newBounds = new Bounds(new Vector3(x, y, 0), itemSize);
            
                var overlaps = false;
                foreach (var bounds in placedBounds)
                    if (Intersects2D(bounds, newBounds))
                    {
                        overlaps = true;
                        break;
                    }

                if (!overlaps)
                {
                    var item = pool.GetItem();
                    item.transform.position = newBounds.center;
                    placedBounds.Add(newBounds);
                    _gameBgScroller.AddContent(item.transform);
                    return;
                }
            }
        }

        private bool Intersects2D(Bounds a, Bounds b)
        {
            return a.min.x < b.max.x && a.max.x > b.min.x &&
                   a.min.y < b.max.y && a.max.y > b.min.y;
        }
    }
}
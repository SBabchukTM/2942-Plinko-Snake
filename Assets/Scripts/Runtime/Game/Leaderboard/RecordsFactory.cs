using System.Collections.Generic;
using System.Linq;
using Runtime.Core.Factory;
using Runtime.Core.Infrastructure.ObjectGetter;
using Runtime.Game.Services.SettingsProvider;
using UnityEngine;
using Zenject;

namespace Runtime.Game.Leaderboard
{
    public class RecordsFactory : IInitializable
    {
        private readonly IObjectGetterService _objectGetterService;
        private readonly ScoresService _scoresService;
        private readonly GameObjectFactory _gameObjectFactory;

        private GameObject _prefab;
        
        public RecordsFactory(IObjectGetterService objectGetterService, ScoresService scoresService,
            GameObjectFactory gameObjectFactory)
        {
            _objectGetterService = objectGetterService;
            _scoresService = scoresService;
            _gameObjectFactory = gameObjectFactory;
        }
        
        public async void Initialize()
        {
            _prefab = await _objectGetterService.Load<GameObject>(PrefabNames.RecordPrefab);
        }

        public List<RecordDisplay> CreateRecords()
        {
            List<RecordDisplay> records = new List<RecordDisplay>();

            var dataList = CreateRecordDataList();

            InitData(dataList, records);

            return records;
        }

        private void InitData(List<ScoreData> dataList, List<RecordDisplay> records)
        {
            int place = 1;
            foreach (var data in dataList)
            {
                var display = _gameObjectFactory.Create<RecordDisplay>(_prefab);
                display.Initialize(data, place);
                records.Add(display);
                
                place++;
            }
        }

        private List<ScoreData> CreateRecordDataList()
        {
            List<ScoreData> records = new List<ScoreData>()
            {
                new (){Name = "Thomas", Score = 32},
                new (){Name = "Merry", Score = 51},
                new (){Name = "Jane", Score = 72},
                new (){Name = "Jacob", Score = 62},
                new (){Name = "Susie", Score = 23},
                new (){Name = "Peter", Score = 52},
                new (){Name = "Meg", Score = 33},
                new (){Name = "Lucy", Score = 23},
                new (){Name = "Mona", Score = 16},
                new (){Name = "Lisa", Score = 22},
                new (){Name = "David", Score = 15},
                new (){Name = "Theresa", Score = 11},
                new (){Name = "Trevor", Score = 12},
                new (){Name = "Denis", Score = 10},
                new (){Name = "Jesse", Score = 8},
                new (){Name = "Walter", Score = 7},
                new (){Name = "Arthur", Score = 7},
                new (){Name = "Eirine", Score = 5},
                new (){Name = "Diane", Score = 3},
            };
            
            records.Add(_scoresService.GetUserRecord());
            
            return records.OrderByDescending(x => x.Score).ToList();
        }
    }
}
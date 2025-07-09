using TMPro;
using UnityEngine;

namespace Runtime.Game.Leaderboard
{
    public class RecordDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _placeText;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        public void Initialize(ScoreData data, int place)
        {
            _placeText.text = place + ".";
            _nameText.text = data.Name;
            _scoreText.text = data.Score.ToString();
        }
    }
}
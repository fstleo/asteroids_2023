using System;
using TMPro;
using UnityEngine;

namespace Asteroids.GameMechanics.Scores.UI
{

    public class ScoresWidgetView : MonoBehaviour, IScoresWidget, IDisposable
    {
        [SerializeField]
        private TextMeshProUGUI _scoreField;
        
        [SerializeField]
        private TextMeshProUGUI _topScoreField;

        public void SetTopScore(int topScore)
        {
            if (_topScoreField != null)
            {
                _topScoreField.text = topScore.ToString();    
            }
        }

        public void SetScore(int score)
        {
            if (_scoreField != null)
            {
                _scoreField.text = score.ToString();    
            }
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}

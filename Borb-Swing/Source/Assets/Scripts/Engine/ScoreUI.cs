using TMPro;
using UnityEngine;

namespace Engine
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        private void Start()
        {
            GameProgressManager.Instance.OnScoreChanged += OnScoreChanged;
        }

        private void OnScoreChanged(float score)
        {
            this._scoreText.text = ((int) score).ToString();
        }
    }
}
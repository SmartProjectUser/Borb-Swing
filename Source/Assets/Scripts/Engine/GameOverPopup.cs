using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Engine
{
    public class GameOverPopup : MonoBehaviour
    {
        [SerializeField] private Button _restart;
        [SerializeField] private TextMeshProUGUI _scoreText;

        private void Awake()
        {
            this._restart.onClick.AddListener(RestartGame);
        }

        private void OnEnable()
        {
            this._scoreText.text = ((int) GameProgressManager.Instance.GetTotalRecord()).ToString();
        }

        private void RestartGame()
        {
            GameProgressManager.Instance.Restart();
        }
    }
}
using UnityEngine;
using UnityEngine.Analytics;

namespace Engine
{
    public class GUI : MonoBehaviour
    {
        [SerializeField] private GameObject _endGamePopup;

        private void Start()
        {
            GameProgressManager.Instance.OnGameOver += GameOver;
        }

        private void GameOver()
        {
            this._endGamePopup.SetActive(true);
        }
    }
}
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

namespace Engine
{
    public class GameProgressManager : MonoBehaviour
    {
        public static GameProgressManager Instance { get; private set; }
        public event Action OnGameOver;
        public event Action<float> OnScoreChanged;
        [SerializeField] private PlayerController _player;
        private float _currentRecord;
        private float _totalRecord;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance.gameObject);
            }

            this._totalRecord = PlayerPrefs.GetFloat("Score", 0);
            this._player.OnGameOver += GameOver;
            Instance = this;
        }

        private void GameOver()
        {
            if (this._totalRecord < this._currentRecord)
            {
                this._totalRecord = this._currentRecord;
                PlayerPrefs.SetFloat("Score", this._currentRecord);
            }
            OnGameOver?.Invoke();
        }

        public void SetSessionRecord(float maxDistance)
        {
            OnScoreChanged?.Invoke(maxDistance);
            this._currentRecord = maxDistance;
        }

        public float GetSessionRecord()
        {
            return this._currentRecord;
        }

        public float GetTotalRecord()
        {
            return this._totalRecord;
        }

        public void Restart()
        {
            SceneManager.LoadScene("Game");
        }
    }
}
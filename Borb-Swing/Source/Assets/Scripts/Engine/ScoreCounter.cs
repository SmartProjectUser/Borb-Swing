using UnityEngine;

namespace Engine
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private Transform _scoreTarget;
        private Vector3 _startPosition;
        private float _maxDistance;

        private void Awake()
        {
            this._startPosition = this._scoreTarget.position;
        }

        private void FixedUpdate()
        {
            Vector3 newPosition = this._scoreTarget.position;
            float distance = Vector3.Distance(this._startPosition, newPosition);

            if (this._maxDistance < distance)
            {
                this._maxDistance = distance;

                GameProgressManager.Instance.SetSessionRecord(this._maxDistance);
            }
        }
    }
}
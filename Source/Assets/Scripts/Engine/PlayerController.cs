using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Engine
{
    public class PlayerController : MonoBehaviour
    {
        public event Action OnGameOver;
        
        [SerializeField] private RopeBlaster _blaster;
        [SerializeField] private PlayerPhysics _physics;
        [SerializeField] private CollisionDetector _ground;
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _spider;
        [SerializeField] private AudioSource _die;
        
        private bool _started;
        private bool _isAlive = true;

        private void Awake()
        {
            this._blaster.OnHitStart += OnBlasterHitStart;
            this._blaster.OnHitEnd += OnCancelHit;
            this._ground.OnCollied += OnGround;
        }

        private void OnGround(Vector3 obj)
        {
            if (this._isAlive)
            {
                this._physics.StopPhysics();
                this._animator.SetTrigger("Dead");
                this._isAlive = false;
                this._blaster.gameObject.SetActive(false);
                OnGameOver?.Invoke();
                _die.PlayOneShot(this._die.clip);
            }
        }

        private void OnDestroy()
        {
            this._blaster.OnHitStart -= OnBlasterHitStart;
            this._blaster.OnHitEnd -= OnCancelHit;
            this._ground.OnCollied -= OnGround;
        }

        private void OnCancelHit()
        {
            this._physics.CancelPendulum();
            this._animator.SetTrigger("Cancel");
        }

        private void OnBlasterHitStart(Vector3 position)
        {
            if (!this._started)
            {
                this._physics.StartPhysics();
                this._started = true;
            }

            Instantiate(this._spider, position, Quaternion.identity).transform.DOScale(Vector3.one * 2.6f, 0.5f);
            this._animator.SetTrigger("Hit");
            this._physics.SetPendulumCenter(position);
        }
    }
}
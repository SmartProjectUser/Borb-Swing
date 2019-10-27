using System;
using System.Collections;
using UnityEngine;

namespace Engine
{
    public class RopeBlaster : MonoBehaviour
    {
        public event Action<Vector3> OnHitStart;
        public event Action OnHitEnd;
        
        [SerializeField] private CollisionDetector _ropeEnd;
        [SerializeField] private Transform _ropeTransform;
        [SerializeField] private float _blasterSpeed;

        private Vector3 _aim;
        private Coroutine _blastHitCoroutine;
        private bool _isFindingCollision;
        private readonly Vector3 _scaleVector = new Vector3(0, 1, 0);
        
        private void OnEnable()
        {
            this._ropeEnd.OnCollied += OnGetCollision;
        }

        private void OnGetCollision(Vector3 hitPoint)
        {
            if (this._isFindingCollision)
            {
                _isFindingCollision = false;
                OnHitStart?.Invoke(hitPoint);
            }
        }

        private void OnDisable()
        {
            this._ropeEnd.OnCollied += OnGetCollision;
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    Ray raycast = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit raycastHit;
                    LayerMask mask = LayerMask.GetMask("Cast");
                    if (UnityEngine.Physics.Raycast(
                                                    raycast,
                                                    out raycastHit,
                                                    1000,
                                                    mask))
                    {
                        SetNewAim(raycastHit.point);
                    }
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    CancelAim();
                }
            }
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit raycastHit;
                LayerMask mask = LayerMask.GetMask("Cast");
                if (UnityEngine.Physics.Raycast(
                                                raycast,
                                                out raycastHit,
                                                1000,
                                                mask))
                {
                    SetNewAim(raycastHit.point);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                CancelAim();
            }
#endif
            transform.LookAt(_aim);
        }

        private void SetNewAim(Vector3 aim)
        {
            this._aim = aim;
            _blastHitCoroutine = StartCoroutine(BlastHit());
        }

        private void CancelAim()
        {
            _ropeTransform.gameObject.SetActive(false);

            if (this._blastHitCoroutine != null)
            {
                StopCoroutine(this._blastHitCoroutine);
            }

            _isFindingCollision = false;
            OnHitEnd?.Invoke();
        }

        private IEnumerator BlastHit()
        {
            _isFindingCollision = true;
            _ropeTransform.gameObject.SetActive(true);
            Vector3 currentScale = this._ropeTransform.localScale;
            currentScale.y = 0;
            this._ropeTransform.localScale = currentScale;

            while (_isFindingCollision)
            {
                currentScale = this._ropeTransform.localScale;
                currentScale += this._scaleVector * this._blasterSpeed * Time.deltaTime;
                this._ropeTransform.localScale = currentScale;

                Vector3 currentPosition = this._ropeTransform.localPosition;
                currentPosition.z = this._ropeTransform.localScale.y;
                this._ropeTransform.localPosition = currentPosition;

                yield return null;
            }
        }
    }
}
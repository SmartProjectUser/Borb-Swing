using System;
using System.Collections;
using System.Collections.Generic;
using Physics;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    private Vector3 _moveDirection = Vector3.zero;
    private bool _isAlive;
    private float _currentLength;
    private Vector3 _currentCenter;

    private bool _isSwinging;
    private float _currentSwingTime;
    private Vector3 Fg = new Vector3(0, -PhysicsConstants.Gravity, 0);
    private Vector3 velocity;

    public void StartPhysics()
    {
        if (this._isAlive)
        {
            StopPhysics();
        }

        this._isAlive = true;
    }

    public void StopPhysics()
    {
        this._isAlive = false;
        this._moveDirection = Vector3.zero;
    }

    private void CalcMovement()
    {
        Vector3 acceleration = this.Fg;

        if (this._isSwinging)
        {
            Vector3 n = this._currentCenter - transform.position;

            float l = n.magnitude;
            n /= l;

            if (l > this._currentLength)
            {
                float a = Vector3.Dot(acceleration + velocity / Time.fixedDeltaTime, n);
                acceleration -= a * n;
            }
        }

        velocity += acceleration * Time.fixedDeltaTime;
        Vector3 pos = transform.position;
        pos += velocity * Time.fixedDeltaTime;
        transform.position = pos;
    }

    private void FixedUpdate()
    {
        if (this._isAlive)
        {
            CalcMovement();
        }
    }

    public void CancelPendulum()
    {
        this._isSwinging = false;
    }
    
    public void SetPendulumCenter(Vector3 center)
    {
        this._isSwinging = true;
        this._currentCenter = center;
        this._currentLength = Vector3.Distance(transform.position, center);
        Vector3 n = (this._currentCenter - transform.position).normalized;
        float v = _moveDirection.magnitude;
        _moveDirection -= Vector3.Dot(_moveDirection, n) * n;
        _moveDirection = _moveDirection.normalized * v;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] float Speed = 15;
    [SerializeField] float TurnSpeed = 15;
    private float _walkMargin = 2;
    private float _turnMargin = 0.99f;
    private Vector3 _target = Vector3.zero;
    Animator _animator = null;
    bool _isrunning = false;
    bool _isChasing = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.Instance.IsPlayerActive)
        {
            if (_isrunning)
            {
                StopRunning();
            }
            return;
        }
        if (_target != Vector3.zero)
        {
            GotoTarget();
        }
    }

    private void GotoTarget()
    {
        if (Vector3.Dot((_target - transform.position).normalized, transform.forward) >= _turnMargin)
        {
            if (Vector3.Distance(transform.position, _target) > _walkMargin)
            {
                float step = Speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, _target, step);
            }
            else if (_isChasing)
            {
                Debug.Log("[AIController] Got my target");
                _isChasing = false;
                StartCoroutine(TargetFound());
            }
        }
        else
        {
            if (!_isrunning)
            {
                StartRunning();
            }
            // Determine which direction to rotate towards
            Vector3 targetDirection = _target - transform.position;
            // The step size is equal to speed times frame time.
            float singleStep = TurnSpeed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

    }

    private void StartRunning()
    {
        Debug.Log("ai running");
        _isrunning = true;
        _animator.SetBool("running", true);
    }

    internal void SetTarget(Vector3 target)
    {
        Debug.Log($"[AIController] Target set: {target}");
        _target = target;
        _isChasing = true;
    }

    IEnumerator TargetFound()
    {
        StopRunning();
        _animator.Play("wave");
        yield return new WaitForSeconds(2);
        GameManager.Instance.SwapFinish();
    }

    private void StopRunning()
    {
        Debug.Log("ai idle");
        _isrunning = false;
        _animator.SetBool("running", false);
    }
}

using System;
using UnityEngine;

[RequireComponent(typeof(SwipeInputController)), RequireComponent(typeof(Rigidbody))]
public class SwerveMovement : MonoBehaviour
{
    [Header("Movement")] [SerializeField] private Vector2 platformXBoundaries;
    [SerializeField] private float verticalSpeed, horizontalSpeed;

    private const float RoundupSpeed = 1f;
    private const float LookPosZGap = 0.5f;

    private SwipeInputController _inputController;
    private Rigidbody _rigidbody;

    private Vector3 _targetPos;
    private float _targetHorizontalPos;
    private Vector3 _targetRot;

    private void Awake()
    {
        InitComponents();
        InitTargets();
    }

    private void InitComponents()
    {
        _inputController = GetComponent<SwipeInputController>();
#if UNITY_EDITOR
        if (!_inputController)
        {
            Debug.LogError("Don't have SwipeInputController Component");
        }
#endif
        _rigidbody = GetComponent<Rigidbody>();
#if UNITY_EDITOR
        if (!_rigidbody)
        {
            Debug.LogError("Don't have Rigidbody Component");
        }
#endif
    }

    private void InitTargets()
    {
        _targetPos = transform.position;
        _targetHorizontalPos = transform.position.x;
    }

    public void OnUpdate()
    {
        _inputController.CalcSwipeValueX();
    }

    public void OnFixedUpdate()
    {
        Move(Time.deltaTime);
    }

    private void VerticalMove(float deltaTime)
    {
        _targetPos.z += (deltaTime * verticalSpeed);
    }

    private void CalcHorizontalMove()
    {
        _targetHorizontalPos += _inputController.swipeValueX;
        _targetHorizontalPos = Mathf.Clamp(_targetHorizontalPos, platformXBoundaries.x, platformXBoundaries.y);
    }

    private void HorizontalMove(float deltaTime)
    {
        CalcHorizontalMove();

        var pos = transform.position;
        var diff = _targetHorizontalPos - pos.x;
        _targetPos.x = pos.x + (diff * (deltaTime * horizontalSpeed));
    }

    private void MovePosition()
    {
        var currentPos = transform.position;
        var newPos = new Vector3(_targetPos.x, currentPos.y, _targetPos.z);

        _rigidbody.MovePosition(newPos);
    }

    private void Move(float deltaTime)
    {
        VerticalMove(deltaTime);
        HorizontalMove(deltaTime);

        MovePosition();
    }
}
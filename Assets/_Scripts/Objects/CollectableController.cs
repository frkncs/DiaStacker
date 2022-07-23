using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectableController : MonoBehaviour
{
    public enum CollectableType
    {
        Currency,
        Stack
    }

    #region Variables

    // Public Variables
    [SerializeField] public int increaseValue = 10;

    // Private Variables
    [SerializeField] private CollectableType collectableType;

    private Transform _playerTrans;
    private CollectableBaseState _currentState;

    private Rigidbody _rb;

    private Animator _animator;

    private float _startY, _zOffset;

    #endregion Variables

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        
        _playerTrans = GameObject.FindGameObjectWithTag("Player").transform;

        _currentState = new CollectableIdleState(this);
    }

    private void Update()
    {
        _currentState.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        _currentState.OnTriggerEnter(other);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }

    public void StartFunc()
    {
        _startY = transform.position.y;
        _zOffset = transform.position.z - _playerTrans.position.z;

        _currentState = new CollectableCollectedState(this);
    }

    public CollectableType GetCollectableType() => collectableType;

    public bool CheckCanAddToStack() => _currentState.GetType() == typeof(CollectableIdleState);

    public void MoveForward()
    {
        transform.position = new Vector3(transform.position.x, _startY, _playerTrans.position.z + _zOffset);
    }

    public void ThrowCollectable()
    {
        _currentState = new CollectableIdleState(this);

        _animator.enabled = false;
        
        _rb.constraints = RigidbodyConstraints.None;
        _rb.AddForce(new Vector3(Random.Range(-1f, 1f), .9f, .6f) * 500);
        _rb.AddTorque(new Vector3(Random.Range(-300, 300),Random.Range(-300, 300),Random.Range(-300, 300)));
    }
}
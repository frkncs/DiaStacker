using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerStackController : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    [SerializeField] private int maxStackLimit = 20;

    private PlayerController _playerController;
    private Vector3 _stackItemCountObjectDefScale;

    private int _stackObjectCounter;

    #endregion Variables

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _stackObjectCounter = PlayerPrefs.GetInt("StartStackCount");
    }

    private void Start()
    {
        GameEvents.AddCollectableToStackEvent += AddCollectableToStack;
        GameEvents.RemoveCollectableFromStackEvent += RemoveCollectableFromStack;
        GameEvents.UpdateStartStackItemCount += UpdateStartStack;
        
        UpdateStartStack();
    }

    public int GetStackObjectCount() => _stackObjectCounter;
    public int GetMaxStackObjectCount() => maxStackLimit;
    
    private void AddCollectableToStack(Transform collectableTrans)
    {
        GameEvents.PlayCollectedFeedbackEvent?.Invoke(CollectableController.CollectableType.Stack, collectableTrans.position);
        
        if (_stackObjectCounter >= maxStackLimit)
        {
            Destroy(collectableTrans.gameObject);
            return;
        }

        _stackObjectCounter++;

        GameEvents.UpdateStackItemCount?.Invoke();
        Destroy(collectableTrans.gameObject);

        if (_stackObjectCounter == 1)
        {
            if (_playerController.CheckIsIdleState())
            {
                _playerController.PlayRun2Anim(0);
            }
            else
            {
                _playerController.PlayRun2Anim(1);
            }
        }
    }

    private void RemoveCollectableFromStack()
    {
        if (_stackObjectCounter <= 0) return;
        
        _stackObjectCounter--;
        
        GameEvents.UpdateStackItemCount?.Invoke();
    }

    private void UpdateStartStack()
    {
        _stackObjectCounter = PlayerPrefs.GetInt("StartStackCount");
        
        GameEvents.UpdateStackItemCount?.Invoke();
    }
}
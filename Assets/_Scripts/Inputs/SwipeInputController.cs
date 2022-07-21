using System;
using UnityEngine;

public class SwipeInputController : MonoBehaviour
{
    [HideInInspector] public float swipeValueX;

    private const float ScreenRatioValue = 19f;
    private float _screenWidthRatio;
    private float _startPosX, _endPosX;

    private void Start()
    {
        CalcScreenWithRatio();
    }

    private void CalcScreenWithRatio()
    {
        _screenWidthRatio = Screen.width / ScreenRatioValue;
    }

    public float CalcSwipeValueX()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            _startPosX = UnityEngine.Input.mousePosition.x;
        }
        else if (UnityEngine.Input.GetMouseButton(0))
        {
            _endPosX = UnityEngine.Input.mousePosition.x;

            swipeValueX = -(_startPosX - _endPosX) / _screenWidthRatio;
            _startPosX = _endPosX;
        }

        if (UnityEngine.Input.GetMouseButtonUp(0))
        {
            swipeValueX = 0f;
        }

        return swipeValueX;
    }
}
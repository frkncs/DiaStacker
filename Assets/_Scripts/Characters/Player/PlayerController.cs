using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SwerveMovement))]
public class PlayerController : MonoBehaviour
{
    #region Variables

	// Public Variables
	[HideInInspector] public SwerveMovement swerveMovement;

	// Private Variables
	private PlayerBaseState _currentState;
	private Animator _animator;

	private readonly int IdleAnim = Animator.StringToHash("Idle");
	private readonly int RunAnim = Animator.StringToHash("Run");
	private readonly int Run2Anim = Animator.StringToHash("Run2");
	private readonly int DanceAnim = Animator.StringToHash("Dance");

	private const float AnimationCrossFadeDuration = .1f;

	#endregion Variables

	private void Awake()
	{
		Application.targetFrameRate = 120;
		
		swerveMovement = GetComponent<SwerveMovement>();
		_animator = GetComponent<Animator>();
	}

	private void Start()
	{
		GameEvents.WinGameEvent += WinState;
	    
        IdleState();
    }

	private void Update()
	{
		_currentState.Update();
	}

	private void FixedUpdate()
	{
		_currentState.FixedUpdate();
	}

	private void OnTriggerEnter(Collider other)
	{
		_currentState.OnTriggerEnter(other);
	}

	public void IdleState() => _currentState = new PlayerIdleState(this);
	public void RunState() => _currentState = new PlayerRunState(this);
	private void WinState()
	{
		_currentState = new PlayerFinishState(this);
	}

	public void PlayIdleAnim() => _animator.CrossFade(IdleAnim, AnimationCrossFadeDuration);
	public void PlayRunAnim() => _animator.CrossFade(RunAnim, AnimationCrossFadeDuration);
	public void PlayRun2Anim() => _animator.CrossFade(Run2Anim, AnimationCrossFadeDuration);
	public void PlayDanceAnim() => _animator.CrossFade(DanceAnim, AnimationCrossFadeDuration);
}

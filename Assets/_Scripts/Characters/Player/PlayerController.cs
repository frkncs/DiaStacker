using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(SwerveMovement))]
public class PlayerController : MonoBehaviour
{
    #region Variables

	// Public Variables
	[HideInInspector] public SwerveMovement swerveMovement;
	[HideInInspector] public PlayerStackController playerStackController;

	// Private Variables
	[SerializeField] private Renderer renderer;
	
	private PlayerBaseState _currentState;
	private Animator _animator;

	private Color _defColor;

	private readonly int IdleAnim = Animator.StringToHash("Idle");
	private readonly int RunAnim = Animator.StringToHash("Run");
	private readonly int Run2Anim = Animator.StringToHash("Run2");
	private readonly int DanceAnim = Animator.StringToHash("Dance");

	private const float AnimationCrossFadeDuration = .2f;

	#endregion Variables

	private void Awake()
	{
		Application.targetFrameRate = 120;
		
		swerveMovement = GetComponent<SwerveMovement>();
		playerStackController = GetComponent<PlayerStackController>();
		_animator = GetComponent<Animator>();

		_defColor = renderer.material.color;
		
		IdleState();
	}

	private void Start()
	{
		GameEvents.WinGameEvent += WinState;
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
		var lookAtPos = Camera.main.transform.position;
		lookAtPos.y = 0;
		
		transform.DOLookAt(lookAtPos, .8f)
			.SetEase(Ease.OutSine);
		
		_currentState = new PlayerFinishState(this);
	}

	public void PlayIdleAnim() => _animator.CrossFade(IdleAnim, AnimationCrossFadeDuration);
	public void PlayRunAnim() => _animator.CrossFade(RunAnim, AnimationCrossFadeDuration);
	public void PlayRun2Anim() => _animator.CrossFade(Run2Anim, AnimationCrossFadeDuration);

	public void PlayRun2Anim(float speed)
	{
		_animator.SetFloat("Run2AnimSpeed", speed);
		_animator.CrossFade(Run2Anim, AnimationCrossFadeDuration);
	}
	public void PlayDanceAnim() => _animator.CrossFade(DanceAnim, AnimationCrossFadeDuration);
	public bool CheckIsIdleState() => _currentState.GetType() == typeof(PlayerIdleState);

	public void PlayRedColorAnim()
	{
		renderer.material.DOColor(Color.red, .1f)
			.SetEase(Ease.Linear)
			.OnComplete(() =>
			{
				renderer.material.DOColor(_defColor, .1f)
					.SetEase(Ease.Linear);
			});
	}
}

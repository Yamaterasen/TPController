using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class PlayerCharacterAnimator : MonoBehaviour
{
    [SerializeField] ThirdPersonMovement _thirdPersonMovement = null;
    //these names align with the naming in our Animator node
    const string IdleState = "Idle";
    const string RunState = "Run";
    const string JumpState = "Jumping";
    const string FallState = "Falling";
    const string LandingState = "Landing";
    const string SprintState = "Sprint";
    const string LiftState = "Lift";

    Animator _animator = null;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnIdle()
    {
        _animator.CrossFadeInFixedTime(IdleState, .2f);
    }

    public void OnStartRunning()
    {
        _animator.CrossFadeInFixedTime(RunState, .2f);
    }

    public void OnJumping()
    {
        _animator.CrossFadeInFixedTime(JumpState, .2f);
    }

    public void OnLanding()
    {
        _animator.CrossFadeInFixedTime(LandingState, .2f);
    }

    public void OnSprint()
    {
        _animator.CrossFadeInFixedTime(SprintState, .2f);
    }

    public void OnLift()
    {
        _animator.CrossFadeInFixedTime(LiftState, .2f);
    }

    private void OnEnable()
    {
        _thirdPersonMovement.Idle += OnIdle;
        _thirdPersonMovement.StartRunning += OnStartRunning;
        _thirdPersonMovement.Jumping += OnJumping;
        _thirdPersonMovement.Landing += OnLanding;
        _thirdPersonMovement.Sprint += OnSprint;
        _thirdPersonMovement.Lift += OnLift;
    }

    private void OnDisable()
    {
        _thirdPersonMovement.Idle -= OnIdle;
        _thirdPersonMovement.StartRunning -= OnStartRunning;
        _thirdPersonMovement.Jumping -= OnJumping;
        _thirdPersonMovement.Landing -= OnLanding;
        _thirdPersonMovement.Sprint -= OnSprint;
        _thirdPersonMovement.Lift -= OnLift;
    }
}

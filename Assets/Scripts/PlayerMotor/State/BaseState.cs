using UnityEngine;


public abstract class BaseState : MonoBehaviour
{
    protected PlayerMotor playerMotor;
    protected RunningState runningState;
    protected SlidingState slidingState;
    protected JumpingState jumpingState;
    protected FallingState fallingState;
    protected DeathState deathState;

    public virtual void Construct() { }

    public virtual void Destruct() { }

    public virtual void Transition() { }

    private void Awake()
    {
        playerMotor = GetComponent<PlayerMotor>();
        runningState = GetComponent<RunningState>();
        slidingState = GetComponent<SlidingState>();
        jumpingState = GetComponent<JumpingState>();
        fallingState = GetComponent<FallingState>();
        deathState = GetComponent<DeathState>();
    }

    public virtual Vector3 ProcessMotion() { return Vector3.zero; }

}

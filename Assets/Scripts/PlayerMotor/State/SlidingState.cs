using UnityEngine;


public class SlidingState : BaseState
{
    private const string SLIDE_ANIMATION = "Slide";
    private const string RUNNING_ANIMATION = "Running";


    [SerializeField] private float slideDuration = 1f;

    private Vector3 initialCenter;
    private float initialSize;
    private float slideStart;

    public override void Construct()
    {
        playerMotor.Animator?.SetTrigger(SLIDE_ANIMATION);
        slideStart = Time.time;

        initialSize = playerMotor.Controller.height;
        initialCenter = playerMotor.Controller.center;

        playerMotor.Controller.height = initialSize * 0.5f;
        playerMotor.Controller.center = initialCenter * 0.5f; 
    }

    public override void Destruct()
    {
        playerMotor.Controller.height = initialSize;
        playerMotor.Controller.center = initialCenter;
        playerMotor.Animator?.SetTrigger(RUNNING_ANIMATION);
    }

    public override void Transition()
    {
        if (InputManager.Instance.SwipeRight)
        {
            playerMotor.ChangeLane(1);
        }
        if (InputManager.Instance.SwipeLeft)
        {
            playerMotor.ChangeLane(-1);
        }
        if (!playerMotor.IsGrounded)
        {
            playerMotor.ChangeState(fallingState);
        }
        if (InputManager.Instance.SwipeUp)
        {
            playerMotor.ChangeState(jumpingState);
        }
        if (Time.time - slideStart > slideDuration)
        {
            playerMotor.ChangeState(runningState);
        }
    }

    public override Vector3 ProcessMotion()
    {
        Vector3 moveVector = Vector3.zero;

        moveVector.x = playerMotor.SnapToLane();
        moveVector.y = -1f;
        moveVector.z = playerMotor.BaseRunSpeed;

        return moveVector;
    }
}

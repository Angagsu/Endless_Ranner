using UnityEngine;


public class JumpingState : BaseState
{
    private const string JUMP_ANIMATION = "Jump";


    [SerializeField] private float JumpForce = 7f;

    public override void Construct()
    {
        playerMotor.VerticalVelocity = JumpForce;
        playerMotor.Animator?.SetTrigger(JUMP_ANIMATION);
    }

    public override Vector3 ProcessMotion()
    {
        playerMotor.ApplyGravity();

        Vector3 moveVector = Vector3.zero;

        moveVector.x = playerMotor.SnapToLane();
        moveVector.y = playerMotor.VerticalVelocity;
        moveVector.z = playerMotor.BaseRunSpeed;

        return moveVector;
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
        if (playerMotor.VerticalVelocity < 0)
        {
            playerMotor.ChangeState(fallingState);
        }
    }
}

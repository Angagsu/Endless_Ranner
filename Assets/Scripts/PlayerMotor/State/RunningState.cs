using UnityEngine;


public class RunningState : BaseState
{
    public override void Construct()
    {
        playerMotor.VerticalVelocity = 0;
    }

    public override Vector3 ProcessMotion()
    {
        Vector3 moveVector = Vector3.zero;

        moveVector.x = playerMotor.SnapToLane();
        moveVector.y = -1f;
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
        if (InputManager.Instance.SwipeUp && playerMotor.IsGrounded)
        {
            playerMotor.ChangeState(jumpingState);
        }
        if (!playerMotor.IsGrounded)
        {
            playerMotor.ChangeState(fallingState);
        }
        if (InputManager.Instance.SwipeDown)
        {
            playerMotor.ChangeState(slidingState);
        }
    }
}

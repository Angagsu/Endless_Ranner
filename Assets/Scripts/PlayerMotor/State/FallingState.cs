using UnityEngine;


public class FallingState : BaseState
{
    private const string FALL_ANIMATION = "Fall";


    public override void Construct()
    {
        playerMotor.Animator?.SetTrigger(FALL_ANIMATION);
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
        if (playerMotor.IsGrounded)
        {
            playerMotor.ChangeState(runningState);
        }
    }
}

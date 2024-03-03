using UnityEngine;

public class FallingState : BaseState
{
    public override void Construct()
    {
        playerMotor.animator?.SetTrigger("Fall");
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
        if (playerMotor.IsGrounded)
        {
            playerMotor.ChangeState(GetComponent<RunningState>());
        }
    }
}

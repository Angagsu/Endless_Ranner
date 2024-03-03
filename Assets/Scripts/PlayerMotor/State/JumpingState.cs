using UnityEngine;

public class JumpingState : BaseState
{
    public float JumpForce = 7f;

    public override void Construct()
    {
        playerMotor.VerticalVelocity = JumpForce;
        playerMotor.animator?.SetTrigger("Jump");
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
        if (playerMotor.VerticalVelocity < 0)
        {
            playerMotor.ChangeState(GetComponent<FallingState>());
        }
    }
}

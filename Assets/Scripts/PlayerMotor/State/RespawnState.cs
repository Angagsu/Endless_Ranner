using UnityEngine;


public class RespawnState : BaseState
{
    private const string RESPAWN_ANIMATION = "Respawn";


    [SerializeField] private float verticalDistance = 25f;
    [SerializeField] private float immunityTime = 1f;
    
    private float startTime;


    public override void Construct()
    {
        startTime = Time.time;

        playerMotor.Controller.enabled = false;
        playerMotor.transform.position = new Vector3(0, verticalDistance, 
            playerMotor.transform.position.z);
        playerMotor.Controller.enabled = true;

        playerMotor.VerticalVelocity = 0;
        playerMotor.ResetCurrentLine();
        playerMotor.Animator?.SetTrigger(RESPAWN_ANIMATION);
    }

    public override void Destruct()
    {
        GameManager.Instance.ChangeCamera(Cameras.Game);
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
        if (playerMotor.IsGrounded && (Time.time - startTime) > immunityTime)
        {
            playerMotor.ChangeState(runningState);
        }
        if (InputManager.Instance.SwipeRight)
        {
            playerMotor.ChangeLane(1);
        }
        if (InputManager.Instance.SwipeLeft)
        {
            playerMotor.ChangeLane(-1);
        }
    }
}

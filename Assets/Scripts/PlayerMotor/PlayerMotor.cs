using UnityEngine;


public class PlayerMotor : MonoBehaviour
{
    private const string IS_GROUNDED_ANIMATION = "IsGrounded";
    private const string SPEED_ANIMATION = "Speed";
    private const string IDLE_ANIMATION = "Idle";

    public float VerticalVelocity { get; set; }
    public bool IsGrounded { get; private set; }
    public int CurrentLane { get; private set; } = 0;
    public Vector3 MoveVector { get; private set; }

    [field: SerializeField] public float BaseRunSpeed { get; private set; } = 5f;
    [field: SerializeField] public float Gravity { get; private set; } = 14f;

    [SerializeField] private float DinstanceInBetweenLanes = 3f;
    [SerializeField] private float BaseSidewaySpeed = 10f;
    [SerializeField] private float TerminalVelocity = 20f;
    [SerializeField] private AudioClip deathSFX;

    public CharacterController Controller { get; private set; }
    public Animator Animator { get; private set; }

    private BaseState state;

    private RunningState runningState;
    private RespawnState respawnState;
    private DeathState deathState;

    private bool isPaused;

    private void Start()
    {
        Controller = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
        state = GetComponent<RunningState>();
        runningState = GetComponent<RunningState>();
        respawnState = GetComponent<RespawnState>();
        deathState = GetComponent<DeathState>();

        state.Construct();
        isPaused = true;
    }

    private void Update()
    {
        if (!isPaused)
        {
            UpdateMotor();
        }   
    }

    private void UpdateMotor()
    {
        IsGrounded = Controller.isGrounded;

        MoveVector = state.ProcessMotion();

        state.Transition();

        Animator?.SetBool(IS_GROUNDED_ANIMATION, IsGrounded);
        Animator?.SetFloat(SPEED_ANIMATION, Mathf.Abs(MoveVector.z));

        Controller.Move(MoveVector * Time.deltaTime);
    }

    public float SnapToLane()
    {
        float r = 0;

        if (transform.position.x != (CurrentLane * DinstanceInBetweenLanes))
        {
            float deltaToDesiredPosition = (CurrentLane * DinstanceInBetweenLanes) - transform.position.x;

            r = (deltaToDesiredPosition > 0) ? 1 : -1;
            r *= BaseSidewaySpeed;

            float actualDistance = r * Time.deltaTime;

            if (Mathf.Abs(actualDistance) > Mathf.Abs(deltaToDesiredPosition))
            {
                r = deltaToDesiredPosition * (1 / Time.deltaTime);
            }
        }
        else
        {
            r = 0;
        }

        return r;
    }

    public void ChangeLane(int direction)
    {
        CurrentLane = Mathf.Clamp((CurrentLane + direction), -1, 1);
    }

    public void ResetCurrentLine()
    {
        CurrentLane = 0;
    }

    public void ChangeState(BaseState state)
    {
        this.state.Destruct();
        this.state = state;
        this.state.Construct();
    }

    public void ApplyGravity()
    {
        VerticalVelocity -= Gravity * Time.deltaTime;

        if (VerticalVelocity < -TerminalVelocity)
        {
            VerticalVelocity = -TerminalVelocity;
        }
    }

    public void PausePlayer()
    {
        isPaused = true;
    }

    public void ResumePlayer()
    {
        isPaused = false;
    }

    public void RespawnPlayer()
    {
        ChangeState(respawnState);
        GameManager.Instance.ChangeCamera(Cameras.Respawn);
    }

    public void ResetPlayer()
    {
        CurrentLane = 0;
        transform.position = Vector3.zero;
        Animator?.SetTrigger(IDLE_ANIMATION);
        ChangeState(runningState);
        PausePlayer();
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        string hitLayerName = LayerMask.LayerToName(hit.gameObject.layer);

        if (hitLayerName == "Death")
        {
            ChangeState(deathState);
            AudioManager.Instance.PlaySFX(deathSFX);
        }
    }
}

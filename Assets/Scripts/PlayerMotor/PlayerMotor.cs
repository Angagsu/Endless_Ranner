using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [HideInInspector] public Vector3 MoveVector;
    [HideInInspector] public float VerticalVelocity;
    [HideInInspector] public bool IsGrounded;
    [HideInInspector] public int CurrentLane = 0;

    public float DinstanceInBetweenLanes = 3f;
    public float BaseRunSpeed = 5f;
    public float BaseSidewaySpeed = 10f;
    public float Gravity = 14f;
    public float TerminalVelocity = 20f;

    private CharacterController Controller;

    private BaseState state;

    private void Start()
    {
        Controller = GetComponent<CharacterController>();
        state = GetComponent<RunningState>();
        state.Construct();
    }

    private void Update()
    {
        UpdateMotor();
    }

    private void UpdateMotor()
    {
        IsGrounded = Controller.isGrounded;

        MoveVector = state.ProcessMotion();

        state.Transition();

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

    public void ChangeState(BaseState state)
    {
        this.state.Destruct();
        this.state = state;
        this.state.Construct();
    }
}

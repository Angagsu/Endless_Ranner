using UnityEngine.InputSystem;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }


    public Vector2 TouchPosition { get; private set; }
    public bool Tap { get; private set; }
    public bool SwipeRight { get; private set; }
    public bool SwipeLeft { get; private set; }
    public bool SwipeUp { get; private set; }
    public bool SwipeDown { get; private set; }


    [SerializeField] private float sqrSwipeDeadzone = 50f;


    private RunnerInputAction actionScheme;
    private Vector2 startDrag;
    

    

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SetupControl();
    }

    private void LateUpdate()
    {
        ResetInputs();
    }

    private void ResetInputs()
    {
        Tap = false;
        SwipeRight = false;
        SwipeLeft = false;
        SwipeUp = false;
        SwipeDown = false;

    }

    private void SetupControl()
    {
        actionScheme = new RunnerInputAction();

        actionScheme.Gameplay.Tap.performed += ctx => OnTap(ctx);
        actionScheme.Gameplay.TouchPosition.performed += ctx => OnPosition(ctx);
        actionScheme.Gameplay.StartDrag.performed += ctx => OnStartDrag(ctx);
        actionScheme.Gameplay.EndDrag.performed += ctx => OnEndDrag(ctx); 
    }

    private void OnEndDrag(InputAction.CallbackContext ctx)
    {
        Vector2 delta = TouchPosition - startDrag;
        float sqrDistance = delta.sqrMagnitude;

        if (sqrDistance > sqrSwipeDeadzone)
        {
            float x = Mathf.Abs(delta.x);
            float y = Mathf.Abs(delta.y);

            if (x > y)
            {
                if (delta.x > 0)
                {
                    SwipeRight = true;
                }
                else
                {
                    SwipeLeft = true;
                }
            }
            else
            {
                if (delta.y > 0)
                {
                    SwipeUp = true;
                }
                else
                {
                    SwipeDown = true;
                }
            }
        }
        
        startDrag = Vector2.zero;
    }

    private void OnStartDrag(InputAction.CallbackContext ctx)
    {
        startDrag = TouchPosition;
    }

    private void OnPosition(InputAction.CallbackContext ctx)
    {
        TouchPosition = ctx.ReadValue<Vector2>();
    }

    private void OnTap(InputAction.CallbackContext ctx)
    {
        Tap = true;
    }

    private void OnEnable()
    {
        actionScheme.Enable();
    }

    private void OnDisable()
    {
        actionScheme.Disable();
    }
}

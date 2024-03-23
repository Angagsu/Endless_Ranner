using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    

    [field: SerializeField] public PlayerMotor PlayerMotor { get; private set; }
    [field: SerializeField] public WorldGeneration WorldGeneration { get; private set; }
    [field: SerializeField] public SceneChunkGeneration SceneChunkGeneration { get; private set; }

    [SerializeField] private GameObject[] cameras;

    private GameState state;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {  
        state = GetComponent<GameStateInit>();
        state.Construct();
    }

    private void Update()
    {
        state.UpdateState();
    }

    public void ChangeState(GameState state)
    {
        this.state.Destruct();
        this.state = state;
        state.Construct();
    }

    public void ChangeCamera(Cameras camera)
    {
        foreach (var go in cameras)
        {
            go.SetActive(false);
        }
        cameras[(int)camera].SetActive(true);
    }
}

public enum Cameras
{
    Init = 0,
    Game = 1,
    Shop = 2,
    Respawn = 3
}

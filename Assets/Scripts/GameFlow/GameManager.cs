using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instance; } }
    private static GameManager instance;

    public PlayerMotor PlayerMotor;
    public WorldGeneration WorldGeneration;
    public SceneChunkGeneration SceneChunkGeneration;
    public GameObject[] cameras;

    private GameState state;


    private void Start()
    {
        instance = this;
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

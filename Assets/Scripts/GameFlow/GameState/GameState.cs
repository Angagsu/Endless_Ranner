using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    protected GameManager gameManager;
    protected GameStateInit gameStateInit;
    protected GameStateGame gameStateGame;
    protected GameStateShop gameStateShop;
    protected GameStateDeath gameStateDeath;

    protected virtual void Awake()
    {
        gameManager = GameManager.Instance;
        gameStateInit = GetComponent<GameStateInit>();
        gameStateGame = GetComponent<GameStateGame>();
        gameStateShop = GetComponent<GameStateShop>();
        gameStateDeath = GetComponent<GameStateDeath>();
    }

    public virtual void Construct() { }
    public virtual void Destruct() { }
    public virtual void UpdateState() { }

}

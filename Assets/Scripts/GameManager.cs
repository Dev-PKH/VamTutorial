using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Player player;
    public PoolManager poolManager;

    public float gameTimer;
    public float maxGameTime = 2 * 10f;

    private void Awake()
    {
        if(Instance == null) Instance = this;
    }

    private void Update()
    {
        gameTimer += Time.deltaTime;

        if(gameTimer > maxGameTime)
        {

        }
    }
}

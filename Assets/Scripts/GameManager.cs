using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("# Game Object")]
    public Player player;
    public PoolManager poolManager;

    [Header("# Game Control")]
    public float gameTimer {  get; private set; }
    private float maxGameTime = 2 * 10f;

    [Header("# Player Info")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 1, 3, 6, 10, 15, 21, 28, 36, 45, 55 };

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

    public void GetExp()
    {
        exp++;

        if(exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("# Game Object")]
    public Player player;
    public PoolManager poolManager;
    public LevelUp uiLevelUp;

    [Header("# Game Control")]
    public bool isLive { get; private set; } = true;
    public float gameTimer {  get; private set; }
    public float maxGameTime { get; private set; } = 2 * 10f;

    [Header("# Player Info")]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 1, 3, 6, 10, 15, 21, 28, 36, 45, 55 };

    private void Awake()
    {
        if(Instance == null) Instance = this;
    }

    private void Start()
    {
        health = maxHealth;

        uiLevelUp.Select(0);
    }

    private void Update()
    {
        if (!isLive) return;

        gameTimer += Time.deltaTime;

        if(gameTimer > maxGameTime)
        {
            gameTimer = maxGameTime;
        }
    }

    public void GetExp()
    {
        exp++;

        if (exp == nextExp[Mathf.Min(level, nextExp.Length-1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}

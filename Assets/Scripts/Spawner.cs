using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public float levelTime;

    private float spawnTimer;

    private int enemyLevel;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>(); // 자기 자신도 포함하여 가져옴
        levelTime = GameManager.Instance.maxGameTime / spawnData.Length;
    }

    private void Update()
    {
        if (!GameManager.Instance.isLive) return;

        spawnTimer += Time.deltaTime;
        enemyLevel = Mathf.FloorToInt(GameManager.Instance.gameTimer / levelTime); // 나머지 버림
        enemyLevel = Mathf.Min(enemyLevel, spawnData.Length - 1);

        if (spawnTimer > spawnData[enemyLevel].spawnTime) // 이건 나중에 로직 완성후 변경
        {
            spawnTimer = 0;

            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject enemy = GameManager.Instance.poolManager.GetPrefab(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position; // 0은 SpawnPoint가 아닌 자기 자신
        enemy.GetComponent<Enemy>().Init(spawnData[enemyLevel]);
    }
}

[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
}
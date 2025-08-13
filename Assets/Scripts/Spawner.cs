using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;

    private float spawnTimer;
    private float spawnRate = 0.2f; // 스폰 간격
    private int enemyTypeCount = 0; // 몬스터 종류의 개수

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>(); // 자기 자신도 포함하여 가져옴
    }

    private void Start()
    {
        enemyTypeCount = GameManager.Instance.poolManager.Prefabs.Length;
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if(spawnTimer > spawnRate)
        {
            spawnTimer = 0;

            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject enemy = GameManager.Instance.poolManager.GetEnemy(Random.Range(0, enemyTypeCount));
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position; // 0은 SpawnPoint가 아닌 자기 자신
    }
}

using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Player player;

    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    private float timer;

    private void Start()
    {
        player = GameManager.Instance.player; // 영상에서는 Awake에서 GetParent 사용
        Init();
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150;
                SetPlace();
                break;
            default:
                speed = 0.3f;
                break;
        }
    }

    private void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;
                
                if(timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }

        if(Input.GetMouseButtonDown(0)) { LevelUp(10, 1); }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if(id == 0)
        {
            SetPlace();
        }
    }

    private void SetPlace()
    {
        for(int i=0; i<count; i++)
        {
            Transform bullet;
            
            if(i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.Instance.poolManager.GetPrefab(prefabId).transform;
                bullet.parent = transform;
            }

            // 플레이어 위치를 기준으로 맞출 수 있도록 local값을 조정
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * i / count; // 무기의 개수만큼 각도를 나눔 ex) 2개면 0, 180 / 3개면 0, 120, 240
            bullet.Rotate(rotVec);

            bullet.Translate(bullet.up * 1.5f, Space.World);
            // 현재 bullet이 바라보는 y축 방향으로 이동함. 부모의 회전에 관계없이

            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // 근접은 무한 관통임으로 -1로 이를 표기
        }
    }

    private void Fire()
    {
        if (player.Scanner.nearestTarget == null) return;

        Vector3 targetPos = player.Scanner.nearestTarget.position;
        Vector3 dir = (targetPos - transform.position).normalized;

        Transform bullet = GameManager.Instance.poolManager.GetPrefab(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir); // 위쪽 방향을 기준으로 dir 방향을 볼 수 있게끔 회전을 적용
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}

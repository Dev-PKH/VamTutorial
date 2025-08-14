using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Collider2D coll;

    public float speed;
    public float health;
    public float maxHealth;
    public Rigidbody2D target;
    public RuntimeAnimatorController[] animatorController; // 런타임에 실행될 애니메이션 컨트롤러

    private WaitForFixedUpdate wait; // FixedUpdate의 실행 주기만큼 대기하는 값

    private bool isLive = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();

        wait = new WaitForFixedUpdate();
    }

    private void OnEnable()
    {
        SetActiveState(true);

        health = maxHealth;
    }

    private void Start()
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isLive) return;
        if (!isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        Vector2 dirVec = target.position - rb.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + nextVec);
        rb.linearVelocity = Vector2.zero; // 이전에 가해진 힘을 0으로 초기화
        // 단 속도가 0이므로 이동을 안해야하지만, movePosition이 이를 강제 실행
    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.isLive) return;
        if (!isLive) return;

        spriteRenderer.flipX = target.position.x < rb.position.x;
    }

    public void Init(SpawnData data)
    {
        animator.runtimeAnimatorController = animatorController[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isLive || !collision.CompareTag("Bullet")) return;


        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if(health > 0)
        {
            animator.SetTrigger("Hit");
            AudioManager.Instance.PlaySfx(AudioManager.Sfx.Hit);
        }
        else
        {
            SetActiveState(false);
            GameManager.Instance.kill++;
            GameManager.Instance.GetExp();

            if(GameManager.Instance.isLive) // GameClear 때 전부 죽으면 테러 당할 수 있으니 방지
                AudioManager.Instance.PlaySfx(AudioManager.Sfx.Dead);
        }
    }

    private IEnumerator KnockBack()
    {
        yield return wait; // 다음 Fixed가 실행될 때까지 대기

        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 dir = (transform.position - playerPos).normalized;
        float power = 3f;

        rb.AddForce(dir * power, ForceMode2D.Impulse);
    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }

    private void SetActiveState(bool isActive) // 활성화 여부를 결정
    {
        isLive = isActive;
        coll.enabled = isActive;
        rb.simulated = isActive;

        animator.SetBool("Dead", !isActive);
        spriteRenderer.sortingOrder = isActive ? 2 : 1;
    }
}

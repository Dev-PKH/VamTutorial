using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if(per > -1) // 관통이 아닐 때 (-1은 관통)
        {
            rb.linearVelocity = dir * 15f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (per == -1 || !collision.CompareTag("Enemy")) return;

        per--;

        if(per == -1)
        {
            rb.linearVelocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    // 총알 비활성화를 하기 위해
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (per == -1 || !collision.CompareTag("Area")) return;

        rb.linearVelocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}

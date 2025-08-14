using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;

    private const int tileGap = 40; // Tile의 이동 간격

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) return;

        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 myPos = transform.position;

        float diffX = playerPos.x - myPos.x; // X의 간격
        float diffY = playerPos.y - myPos.y; // Y의 간격

        int negativeX = diffX < 0 ? -1 : 1; // X가 음수인지 판단
        int negativeY = diffY < 0 ? -1 : 1;

        diffX *= negativeX; diffY *= negativeY;

        switch (transform.tag)
        {
            case "Ground":

                if(diffX > diffY) // X의 간격이 더 클 경우
                {
                    transform.Translate(Vector3.right * negativeX * tileGap);
                }
                else if(diffX < diffY) // Y 간격이 더 클 경우
                {
                    transform.Translate(Vector3.up * negativeY * tileGap);
                }
                else
                {
                    transform.Translate(new Vector3(negativeX, negativeY, 0) * tileGap);
                }
                break;
            case "Enemy":
                if(coll.enabled)
                {
                    Vector3 dist = playerPos - myPos;
                    Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);

                    transform.Translate(ran + dist * 2);
                }
                break;
        }
    }
}

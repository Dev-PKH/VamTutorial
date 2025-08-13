using UnityEngine;

public class Reposition : MonoBehaviour
{

    private const int tileGap = 40; // Tile의 이동 간격

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) return;

        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 myPos = transform.position;

        float diffX = Mathf.Abs(playerPos.x - myPos.x); // X의 간격
        float diffY = Mathf.Abs(playerPos.y - myPos.y); // Y의 간격

        Vector3 playerDir = GameManager.Instance.player.InputVec;
        int dirX = playerDir.x < 0 ? -1 : 1; // 이동 방향
        int dirY = playerDir.y < 0 ? -1 : 1;

        switch(transform.tag)
        {
            case "Ground":
                if(diffX > diffY) // X의 간격이 더 클 경우
                {
                    transform.Translate(Vector3.right * dirX * tileGap);
                }
                else if(diffX < diffY) // Y 간격이 더 클 경우
                {
                    transform.Translate(Vector3.up * dirY * tileGap);
                }
                else
                {
                    transform.Translate(new Vector3(dirX, dirY, 0) * tileGap);
                }
                break;
            case "Enemy":
                break;
        }
    }
}

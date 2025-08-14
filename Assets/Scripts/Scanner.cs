using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    public Transform nearestTarget;

    private void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange,
            Vector2.zero, 0, targetLayer); // 플레이어 자리의 원형으로 타켓을 감지
        nearestTarget = GetNearest();
    }

    private Transform GetNearest()
    {
        Transform result = null;
        float diff = 100;

        foreach(RaycastHit2D hit in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = hit.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);

            if(curDiff < diff)
            {
                diff = curDiff;
                result = hit.transform;
            }
        }

        return result;
    }
}

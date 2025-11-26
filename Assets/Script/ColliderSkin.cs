using UnityEngine;

public class ColliderSkin : MonoBehaviour
{
    public float skinWidth = 0.04f; // 皮肤宽度（检测距离）
    public LayerMask groundLayer; // 地面层级

    private BoxCollider2D col;

    private bool isAdjust = false;

    void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 是否调整
        isAdjust = AdjustCheck();

        // 调试信息
        //Debug.Log("isAdjust=" + isAdjust);

        // 应用 Skin Width
        ApplySkinWidth();

    }

    private bool AdjustCheck()
    {
        Bounds bounds = col.bounds;
        Vector2 origin = new Vector2(bounds.center.x,bounds.min.y);
        Vector2 size = new Vector2(bounds.size.x, skinWidth);

        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0, Vector2.down, 0, groundLayer);

        if (hit.collider != null)
        {
            return hit.distance < skinWidth;
        }
        return false;
        
    }

    private void ApplySkinWidth()
    {
        if (isAdjust)
        {
            // 获取地面信息
            Vector2 colliderBottom = (Vector2)transform.position + col.offset -
                                   new Vector2(0, col.size.y / 2);

            RaycastHit2D hit = Physics2D.Raycast(
                colliderBottom,
                Vector2.down,
                skinWidth,
                groundLayer
            );
            if (hit.collider != null)
            {
                // 计算需要保持的距离
                float targetY = hit.point.y +  skinWidth;
                Vector2 newPosition = new Vector2(transform.position.x, targetY);
                // 平滑应用
                transform.position = Vector2.Lerp(transform.position, newPosition, 0.5f);

                // 调试信息
                //Debug.Log("调整位置");
            }
        }
    }
}


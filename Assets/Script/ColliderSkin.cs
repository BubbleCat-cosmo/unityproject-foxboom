using UnityEngine;

public class ColliderSkin : MonoBehaviour
{
    public float skinWidth = 0.04f; // 皮肤宽度（检测距离）
    public LayerMask groundLayer; // 地面层级

    private BoxCollider2D boxCollider;
    private bool isGrounded = false;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 检测地面
        CheckGround();
        // 应用 Skin Width
        ApplySkinWidth();

    }

    private void CheckGround()
    {
        // 计算碰撞体底部的位置
        Vector2 colliderBottom = (Vector2)transform.position + boxCollider.offset -
                                new Vector2(0, boxCollider.size.y / 2);
        // 射线检测地面
        RaycastHit2D hit = Physics2D.Raycast(colliderBottom, Vector2.down, skinWidth, groundLayer);
        isGrounded = (hit.collider != null);

        // 调试提示
        Debug.DrawRay(colliderBottom, Vector2.down * skinWidth,
                     isGrounded ? Color.green : Color.red);

        // Debug.Log("isGrounded="+isGrounded);
    }

    private void ApplySkinWidth()
    {
        if (isGrounded)
        {
            // 获取地面信息
            Vector2 colliderBottom = (Vector2)transform.position + boxCollider.offset -
                                   new Vector2(0, boxCollider.size.y / 2);

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
                Debug.Log("调整位置");
            }
        }
    }


}
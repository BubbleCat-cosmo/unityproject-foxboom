using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public static GroundChecker instance;
    public float groundCheckWidth = 0.05f;

    private BoxCollider2D col;
    public LayerMask groundLayer;

    private bool isGrounded = false;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        isGrounded = CheckGround();
    }
    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    // BoxCast检测版本
    private bool CheckGround()
    {
        Bounds bounds = col.bounds;
        Vector2 origin = new Vector2(bounds.center.x, bounds.min.y);
        Vector2 size = new Vector2(bounds.size.x, groundCheckWidth * 2);

        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0, Vector2.down, 0, groundLayer);

        if (hit.collider != null)
        {
            return true;
        }
        return false;

    }

    // 射线检测版本
    //private void CheckGround()
    //{
    //    Bounds bounds = col.bounds;

    //    // 多个射线检测
    //    Vector2[] raycastOrigins = new Vector2[3];
    //    raycastOrigins[0] = new Vector2(bounds.center.x, bounds.min.y); // 中间
    //    raycastOrigins[1] = new Vector2(bounds.min.x, bounds.min.y); // 左侧
    //    raycastOrigins[2] = new Vector2(bounds.max.x, bounds.min.y); // 右侧

    //    int hitCount = 0;
    //    // 射线检测地面
    //    foreach (Vector2 origin in raycastOrigins)
    //    {
    //        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, checkWidth, groundLayer);

    //        // 调试提示
    //        Debug.DrawRay(origin, Vector2.down * checkWidth,
    //                     isGrounded ? Color.green : Color.red);

    //        if (hit.collider != null)
    //        {
    //            hitCount++;
    //        }
    //    }
    //    isGrounded = hitCount >= 1;
    //    // Debug.Log("isGrounded="+isGrounded);
    //}

}

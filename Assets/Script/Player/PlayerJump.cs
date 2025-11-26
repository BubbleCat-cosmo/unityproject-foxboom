using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpHeight = 2.5f; //跳跃能到达的预期高度
    public float jumpAcceleration = 10f; // 跳跃加速度
    // private Vector2 jumpDirection;//跳跃的方向
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private BoxCollider2D col;
    private float playerInput;
    private Vector2 velocity;

    private bool isGrounded = false;
    private float checkWidth = 0.05f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    private bool isTryJump;
    void Update()
    {
        // 获取输入
        playerInput = Input.GetAxis("Horizontal");
        playerInput = Mathf.Min(playerInput, 1);
        //记录跳跃按键，这里不能直接用赋值，因为Update执行间隔和FixedUpdate不同
        //可能当前按下了跳跃，但FixedUpdate却还没来得及跳起来，却又执行了一次Update
        //如果直接用等号赋值，此时就会被视作没有按下跳跃键
        //所以，应当严格保证只有在FixedUpdate起跳后，isTryJump才为false
        isTryJump |= Input.GetKeyDown(KeyCode.Space);

        // 地面检测
        isGrounded = CheckGround();
        // Debug.Log("isGrounded=" + isGrounded);

    }
    void FixedUpdate()
    {
        Jump();
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

    // BoxCast检测版本
    private bool CheckGround()
    {
        Bounds bounds = col.bounds;
        Vector2 origin = new Vector2(bounds.center.x, bounds.min.y);
        Vector2 size = new Vector2(bounds.size.x, checkWidth * 2);

        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0, Vector2.down, 0, groundLayer);

        if (hit.collider != null)
        {
            return true;
        }
        return false;

    }


    void Jump()
    {
        if (isTryJump)
        {
            isTryJump = false;
            if (isGrounded)
            {
                // Debug.Log("跳跃");
                Vector2 jumpDirection = Vector2.up;
                float jumpSpeed = Mathf.Sqrt(2 * jumpAcceleration * jumpHeight);
                //通过投影获取当前跳跃方向的速度
                float curSpeed = Vector2.Dot(velocity, jumpDirection);
                if (curSpeed > 0)
                {
                    jumpSpeed = Mathf.Max(jumpSpeed - curSpeed, 0);
                }
                velocity += jumpSpeed * jumpDirection;
                rb.velocity = velocity;
            }
            else
            {
                return;
            }
            
        }
    }
}
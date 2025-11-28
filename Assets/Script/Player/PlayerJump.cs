using System;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpHeight = 2.5f; //跳跃能到达的预期高度
    public float jumpAcceleration = 10f; // 跳跃加速度
    public int jumpCount = 2; // 跳跃段数

    private Rigidbody2D rb;
    private Vector2 velocity;

    private bool isGrounded = false;
    private int curJumpCount;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private bool isTryJump;
    void Update()
    {
        
        //记录跳跃按键，这里不能直接用赋值，因为Update执行间隔和FixedUpdate不同
        //可能当前按下了跳跃，但FixedUpdate却还没来得及跳起来，却又执行了一次Update
        //如果直接用等号赋值，此时就会被视作没有按下跳跃键
        //所以，应当严格保证只有在FixedUpdate起跳后，isTryJump才为false
        isTryJump |= Input.GetKeyDown(KeyCode.Space);

        // 地面检测
        isGrounded = GroundChecker.instance.GetIsGrounded();
        // Debug.Log("isGrounded=" + isGrounded);

        // 跳跃段数更新
        UpdateJumpCount();

    }

    void FixedUpdate()
    {
        Jump();
    }

    private void Jump()
    {
        if (isTryJump)
        {
            isTryJump = false;
            if (curJumpCount > 0)
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
                curJumpCount--;
            }
            else
            {
                return;
            }
            
        }
    }
    private void UpdateJumpCount()
    {
        if (isGrounded)
        {
            curJumpCount = jumpCount;
        }
    }
}
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;

    private float playerInputX;
    private bool isWalled = false; 
    private float checkWidth = 0.05f;
    

    private Collider2D col;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        // 获取移动输入
        playerInputX = Input.GetAxis("Horizontal");
        playerInputX = Mathf.Min(playerInputX, 1);
        // 处理待机-移动切换动画
        HandleAnimation();
        // 墙壁检测
        isWalled = CheckWall();
        Debug.Log("isWalled=" + isWalled);
    }

    private void FixedUpdate()
    {
        if (PlayerAttributes.Instance.Hp <= 0) return;
        // 移动
        Move();
    }

    private bool CheckWall()
    {
        Bounds bound = col.bounds;
        Vector2 rightOrigin = new Vector2(bound.max.x, bound.center.y);
        Vector2 leftOrigin = new Vector2(bound.min.x, bound.center.y);
        Vector2 size = new Vector2(checkWidth * 2, bound.size.y);

        RaycastHit2D rightHit = Physics2D.BoxCast(rightOrigin, size, 0, Vector2.right, 0, groundLayer);
        RaycastHit2D leftHit = Physics2D.BoxCast(leftOrigin, size, 0, Vector2.left, 0, groundLayer);

        if((rightHit.collider != null && playerInputX >= 1)||(leftHit.collider != null && playerInputX <= -1))
        {
            return true;
        }
        return false;
    }
    private void Move()
    {
        if (!isWalled)
        {
            float curVelocityY = rb.velocity.y;
            Vector2 velocity = new Vector2(playerInputX * moveSpeed, curVelocityY);
            rb.velocity = velocity;
        }
        else
        {
            return;
        }
    }
    private void HandleAnimation()
    {

        // 切换动画
        bool isMove = playerInputX != 0;
        animator.SetBool("Move", isMove);
        // 转向
        if (playerInputX > 0)
            transform.localScale = new Vector3(4, 4, 1);
        else if (playerInputX < 0)
            transform.localScale = new Vector3(-4, 4, 1);
    }
}

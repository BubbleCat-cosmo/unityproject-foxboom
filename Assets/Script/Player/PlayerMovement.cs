using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float wallCheckWidth = 0.05f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Collider2D col;
    private Animator animator;

    private float playerInputX;
    private bool isWalled = false;
    // private bool isGrounded = false;

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
        // Debug.Log("isWalled=" + isWalled);
    }

    private void FixedUpdate()
    {
        if (PlayerAttributes.Instance.Hp <= 0) return;
        // 移动
        Move();
        // 边缘检测
        // EdgeDetection();
    }

    private bool CheckWall()
    {
        Bounds bound = col.bounds;
        Vector2 rightOrigin = new Vector2(bound.max.x, bound.center.y);
        Vector2 leftOrigin = new Vector2(bound.min.x, bound.center.y);
        Vector2 size = new Vector2(wallCheckWidth * 2, bound.size.y * 1.05f);

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

    //void EdgeDetection()
    //{
    //    //迈出一步的距离
    //    Vector3 move = rb.velocity * Time.fixedDeltaTime;
    //    //继续前进后的位置
    //    Vector3 furthestPoint = transform.position + move;
    //    //如果前进后的位置有检测到指定层，说明即将发生碰撞
    //    Collider2D hit = Physics2D.OverlapBox(furthestPoint, col.bounds.size, 0, groundLayer);

    //    if (hit)
    //    {
    //        Debug.Log("位置调整");
    //        //远离障碍的方向
    //        Vector3 dir = (transform.position - hit.transform.position).normalized;
    //        //移动1、2步骤后的位置
    //        Vector3 tryPos = furthestPoint + dir * move.magnitude + move;
    //        //如果新位置没有碰撞，说明可以进行偏移
    //        //这里要排除接触地面的情况下，否则会误认为一直有碰撞
    //        if (!isGrounded && !Physics2D.OverlapBox(tryPos, col.bounds.size, 0, groundLayer))
    //        {
    //            transform.position = transform.position + dir * move.magnitude;
    //        }
    //    }
    //}
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

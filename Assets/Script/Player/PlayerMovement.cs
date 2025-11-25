using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;

    private Rigidbody2D rb;
    private Animator animator;

    private float playerInputX;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // 获取移动输入
        playerInputX = Input.GetAxis("Horizontal");
        playerInputX = Mathf.Min(playerInputX, 1);
        // 处理待机-移动切换动画
        HandleAnimation();
    }

    private void FixedUpdate()
    {
        if (PlayerAttributes.Instance.Hp <= 0) return;
        // 移动
        Move();
    }

    private void Move()
    {
        float curVelocityY = rb.velocity.y;
        Vector2 velocity = new Vector2(playerInputX * moveSpeed, curVelocityY);
        rb.velocity = velocity;
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

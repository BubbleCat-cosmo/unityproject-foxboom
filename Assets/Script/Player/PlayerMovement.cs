using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // float moveSpeed = PlayerAttributes.Instance.moveSpeed;
    // Rigidbody rb = PlayerAttributes.Instance.rb;

    void Start()
    {

    }
    private void Update()
    {
        // 处理待机-移动切换动画
        HandleAnimation();
    }

    void FixedUpdate()
    {
        // 移动
        Move();
        // 跳跃
    }

    private void Move()
    {
        // 获取Axis值
        float inputX = Input.GetAxis("Horizontal");
        // float inputY = Input.GetAxis("Vertical");

        // 通过改变速度实现位置变换
        Vector2 velocity = new Vector2(inputX * PlayerAttributes.Instance.moveSpeed, 0);
        PlayerAttributes.Instance.rb.velocity = velocity;

    }
    private void HandleAnimation()
    {
        // 获取Axis值
        float inputX = Input.GetAxis("Horizontal");
        // 切换动画
        bool isMove = inputX != 0;
        PlayerAttributes.Instance.animator.SetBool("Move", isMove);
        // 转向
        if (inputX > 0)
            transform.localScale = new Vector3(4, 4, 1);
        else if (inputX < 0)
            transform.localScale = new Vector3(-4, 4, 1);
    }
}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;

    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
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
        // 获取Axis值
        float inputX = Input.GetAxis("Horizontal");
        // float inputY = Input.GetAxis("Vertical");

        // 通过改变速度实现位置变换
        Vector2 velocity = new Vector2(inputX * moveSpeed, 0);
        rb.velocity = velocity;

    }
    private void HandleAnimation()
    {
        // 获取Axis值
        float inputX = Input.GetAxis("Horizontal");
        // 切换动画
        bool isMove = inputX != 0;
        animator.SetBool("Move", isMove);
        // 转向
        if (inputX > 0)
            transform.localScale = new Vector3(4, 4, 1);
        else if (inputX < 0)
            transform.localScale = new Vector3(-4, 4, 1);
    }
}

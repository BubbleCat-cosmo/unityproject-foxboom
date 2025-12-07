using System;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{
    public int HP = 1;
    public float moveSpeed = 2f;
    public int hurtValue = 1;
    public float acttckCDTime = 2f;
    public float LdleMoveRange = 2f;

    public SensorPlayer sensorPlayer;

    private float acttckCDTimer;
    private bool isActtck;
    // private bool isTherePlayer;
    private bool movingToRight;
    private Vector2 leftPoint;
    private Vector2 rightPoint;
    
    private Animator animator;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        isActtck = false;
        movingToRight = true;

        leftPoint = this.transform.position + new Vector3(LdleMoveRange, 0, 0);
        rightPoint = this.transform.position + new Vector3(-LdleMoveRange, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0) return;

        // 待机（左右移动）
        LdleMove();
        

        // 靠近玩家
        // CloseToPlayer();

        // 攻击
        // Attack();
    }

    private void LdleMove()
    {
        if(!sensorPlayer.GetIsTherePlayer())
        {
            // 转向控制
            if (movingToRight) transform.localScale = new Vector3(-4, 4, 1);
            else transform.localScale = new Vector3(4, 4, 1);

            // 移动控制
            Vector3 target = movingToRight ? leftPoint : rightPoint;
            float curVelocityY = rb.velocity.y;
            Vector2 velocity = new Vector2(moveSpeed, curVelocityY);
            rb.velocity = velocity;

            // 方向切换
            Debug.Log(Vector3.Distance(transform.position, target));
            float checkDistance = (target - transform.position).x;
            if (checkDistance > -0.1 && checkDistance < 0.1)
            {
                movingToRight = !movingToRight;
                moveSpeed = -moveSpeed;
            }
        }
    }

    private void Attack()
    {
        throw new NotImplementedException();
    }

    private void CloseToPlayer()
    {
        throw new NotImplementedException();
    }
    private bool PlayerCheck()
    {
        throw new NotImplementedException();
    }
}

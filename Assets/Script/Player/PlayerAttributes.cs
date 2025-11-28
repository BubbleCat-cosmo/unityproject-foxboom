using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    // 状态管理
    public static PlayerAttributes Instance;
    private int hp = 4;
    public float invincibleTime = 1.5f; // 无敌时间

    private bool isInvincible = false; // 是否处于无敌状态
    private float invincibleTimer; // 无敌状态计时器
    private PlayerState currentState; // 角色状态

    public int Hp
    {
        get => hp;
        set
        {
            hp = value;
            if (hp <= 0)
            {
                // hpSpriteRenderer.sprite = hpSprites[0];
                SetState(PlayerState.Dead);
            }
            // hpSpriteRenderer.sprite = hpSprites[hp];
        }
    }

    // 角色状态列表
    public enum PlayerState
    {
        Alive,
        Dead,
        Invincible
    }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        currentState = PlayerState.Alive;
    }

    void Update()
    {
        HandleInvincibility();
    }

    // 设置状态
    public void SetState(PlayerState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case PlayerState.Dead:
                PlayerDead();
                break;
            case PlayerState.Invincible:
                PlayerInvincible();
                break;
            case PlayerState.Alive:
                PlayerAlive();
                break;
        }
    }

    private void PlayerDead()
    {
        // 播放死亡音效
        // PlayerComponent.instance.playerAudio.PlayDeathSound();
        // 速度置0
        // PlayerComponent.instance.rb.velocity = Vector2.zero;
        // 播放死亡动画
        // PlayerComponent.instance.animator.SetTrigger("Death");
    }
    private void PlayerInvincible()
    {
        // 设置无敌状态标志为真
        isInvincible = true;
        // 无敌状态计时开始
        invincibleTimer = invincibleTime;
        // 忽略与敌人的碰撞
        // Physics2D.IgnoreLayerCollision(gameObject.layer, 10, true);
    }
    private void PlayerAlive()
    {
        // 设置无敌状态标志为假
        isInvincible = true;
        // 启用与敌人的碰撞
        // Physics2D.IgnoreLayerCollision(gameObject.layer, 10, false);
    }

    // 管理无敌状态
    private void HandleInvincibility()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;

            if (invincibleTimer <= 0)
            {
                SetState(PlayerState.Alive);
                invincibleTimer = 0f;
            }
        }
    }

    // 受伤
    public void Hurt()
    {
        if (Hp <= 0) return;
        Hp -= 1;
        if (Hp > 0)
        {
            // PlayerComponent.instance.playerAudio.PlayHurtSound();
            SetState(PlayerState.Invincible);
            // PlayerComponent.instance.animator.SetTrigger("Beat");
        }

    }

    // 恢复
    public void Cure()
    {
        if (Hp <= 0) return;
        // PlayerComponent.instance.playerAudio.PlayCureSound();
        // ScoreManager.instance.AddScore(2);
        if (Hp < 3) Hp += 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] float speed = 2.5f;
    [SerializeField] float bossRange = 6f;
    [SerializeField] PolygonCollider2D swordCollider;
    [SerializeField] float bossAttackRate =2f;
    [SerializeField] float bossNextAttackTime = 2f;
    [SerializeField] GameObject spell, bigSoul, portal;
    [SerializeField] HealthBar barhealth;

    private float idleTimeToWalk = 20f;
    private bool startFight = true;
    private Vector2 spellposition;

    protected override void Start()
    {
        base.Start();
        SoundManager.instance.Play("BossBattle", true);
        animator.SetBool("BossIdle", true);
        barhealth.gameObject.SetActive(true);
        barhealth.setMaxHealth(maxHealth);
    }

    protected override void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("BossWalking"))
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            base.Update();
        }

        if (Time.timeSinceLevelLoad > 2f)
        {
            if (startFight)
            {
                animator.SetBool("BossIdle", false);
                animator.SetBool("BossWalking", true);
                startFight = false;
            }
        }

        if(Time.time > idleTimeToWalk)
        {
            animator.SetBool("BossIdle", false);
            animator.SetBool("BossWalking", true);
        }

        if (Vector2.Distance(target.position, transform.position) <= bossRange && transform.position.y - target.position.y>0)
        {
            if (Time.time > bossNextAttackTime)
            {
                animator.SetTrigger("BossMainAttack");
                SoundManager.instance.Play("BossAttack");
                bossNextAttackTime = bossAttackRate+Time.time;
                idleTimeToWalk = Time.time + 2f;
            }
        }
        else
        {
            if (currentHealth < 300)
            {
                if (Time.time > bossNextAttackTime)
                {
                    animator.SetTrigger("BossCastAttack");
                    bossNextAttackTime = bossAttackRate + Time.time;
                    idleTimeToWalk = Time.time + 2f;
                }
            }
        }

    }
    public void BossMainAttack()
    {
        swordCollider.enabled = true;
        animator.ResetTrigger("BossHurt");
    }

    public void DisableBossCollider()
    {
        animator.ResetTrigger("BossMainAttack");
        swordCollider.enabled = false;
        animator.SetBool("BossWalking", false);
        animator.SetBool("BossIdle", true);
    }

    public override void TakeDamage(float damage)
    {
        SoundManager.instance.Play("EnemyHurt");
        if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("BossMainAttack") || !this.animator.GetCurrentAnimatorStateInfo(0).IsName("Cast"))
        {
            animator.SetTrigger("BossHurt");
        }
        else
        {
            animator.ResetTrigger("BossHurt");
        }
        currentHealth -= damage;
        barhealth.setHealth(currentHealth);

        if (currentHealth <= 0)
        {
            StartCoroutine("BossDeath");
        }
        else
        {
            knockbackCount = knockbackLenght;
        }
    }

    IEnumerator BossDeath()
    {
        ScoreManager.instance.ChangeScore(500);
        animator.SetTrigger("IsDead");
        yield return new WaitForSeconds(1f);
        Instantiate(bigSoul, transform.position, Quaternion.identity);
        portal.SetActive(true);
        barhealth.gameObject.SetActive(false);
        SoundManager.instance.Stop("BossBattle");
        Destroy(gameObject);
    }

    public void InstanciateSpell()
    {
        spellposition = new Vector2(target.position.x, target.position.y+1.8f);
        Instantiate(spell, spellposition, Quaternion.identity);
    }
 
}

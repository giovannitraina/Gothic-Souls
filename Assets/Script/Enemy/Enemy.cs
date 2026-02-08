using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable<float>, IKnockable<Transform>
{
    public float maxHealth;
    public float attackDamage;
    [SerializeField] float attackRate = 0.2f;

    protected float direzioneIniziale, direzioneDopo;

    [SerializeField] float knockback;
    public float knockbackLenght;
    protected float knockbackCount;
    public bool knockFromRight;

    protected float currentHealth;
    protected Transform target;

    protected Animator animator;
    private Rigidbody2D myRigidbody2D;
    private BoxCollider2D boxCollider;
    private CapsuleCollider2D capsuleCollider;
    public EnemyType tipo;

    public enum EnemyType { skeleton, ghost, wizard, boss};
    [SerializeField] float score;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    protected virtual void Start()
    {
        currentHealth = maxHealth;
        direzioneIniziale = target.position.x - transform.position.x;
    }

    protected virtual void Update()
    {
        direzioneDopo = target.position.x - transform.position.x;

        if (direzioneIniziale > 0)
        {
            if (direzioneDopo < 0)
            {
                transform.Rotate(0, 180, 0);
                direzioneIniziale = -1;
            }
        }
        if (direzioneIniziale < 0)
        {
            if (direzioneDopo > 0)
            {
                transform.Rotate(0, 180, 0);
                direzioneIniziale = 1;
            }
        }
    }

    public void Spawn(Transform spawnPoint)
    {
        transform.Spawn(spawnPoint);
    }

    public virtual void TakeDamage(float damage)
    {
        SoundManager.instance.Play("EnemyHurt");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            StartCoroutine("Die");
        }
        else
        {
            knockbackCount = knockbackLenght;
        }     
    }

    protected virtual IEnumerator Die()
    {
        ScoreManager.instance.ChangeScore(score);
        animator.SetTrigger("IsDead");
        boxCollider.enabled = false;
        capsuleCollider.enabled = false;
        myRigidbody2D.bodyType = RigidbodyType2D.Static; //Da vedere se tenerlo
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }

    public void GetKnocked(Transform player)
    {
        if (transform.position.x < player.position.x)
            knockFromRight = true;
        else
            knockFromRight = false;

        StartCoroutine("KnockBackEnemy");
    }

    IEnumerator KnockBackEnemy()
    {
        yield return new WaitForSeconds(0.1f);
        if (knockbackCount > 0)
        {
            if (knockFromRight)
            {
                myRigidbody2D.velocity = new Vector2(-knockback, 0);
            }
            if (!knockFromRight)
            {
                myRigidbody2D.velocity = new Vector2(knockback, 0);
            }

            knockbackCount -= knockbackLenght;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Void"))
        {
            Destroy(gameObject);
        }
    }
}

  
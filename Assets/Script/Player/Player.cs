using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable<float>, IKnockable<Collider2D>
{

    private PlayerMovementController controller;
    private Animator animator;
    private Rigidbody2D myRigidbody2D;

    //PlayerMovement
    private float horizontal = 0f;
    [SerializeField] float speed;
    private bool jump = false;
    private bool crouch = false;
    [SerializeField] float knockback;
    [SerializeField] float knockbackLenght;
    private float knockbackCount;
    public bool knockFromRight;

    //Player Combat
    [SerializeField] Transform attackPoint;
    [SerializeField] float health;
    [SerializeField] float attackDamage;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float attackRate;
    [SerializeField] float attackRange;
    [SerializeField] float enemyAttackRate = 0.2f;
    private float nextAttackTime = 0f;
    private float enemyNextAttackTime = 0f;
    public float currentHealth;

    //Health
    public HealthBar healthbar, healthbarPost;

    //
    private int checkControlls;

    private void Awake()
    {
        controller = GetComponent<PlayerMovementController>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        checkControlls = PlayerPrefs.GetInt("Controlls");

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Livello3"))
        {
            if (PlayerPrefs.HasKey("AttackDmg"))
            {
                attackDamage = PlayerPrefs.GetFloat("AttackDmg");
            }
            if (PlayerPrefs.HasKey("MaxHp"))
            {
                health = PlayerPrefs.GetFloat("MaxHp");
                healthbar.gameObject.SetActive(false);
                healthbarPost.gameObject.SetActive(true);
            }
        }
    }

    private void Start()
    {
        healthbar = GameObject.FindGameObjectWithTag("Healthbar").GetComponent<HealthBar>();
        currentHealth = health;
        healthbar.setMaxHealth(health);
    }

    private void Update()
    {
        if (Time.timeScale!=0)
        {
            //WASD + mouse
            if (checkControlls == 0)
            {
                horizontal = Input.GetAxisRaw("Horizontal2") * speed;
                animator.SetFloat("Speed", Mathf.Abs(horizontal));
            }

            if (Input.GetButtonDown("Jump2") && checkControlls == 0)
            {
                jump = true;
                animator.SetBool("IsJumping", true);
                StartCoroutine("Jump");
            }

            if (Input.GetKeyDown("escape"))
            {
                GameManager.instance.PauseGame();
            }

            if (Input.GetButtonDown("Crouch2") && checkControlls == 0)
            {
                crouch = true;
            }
            else if (Input.GetButtonUp("Crouch2") && checkControlls == 0)
            {
                crouch = false;
            }

            if (Time.time >= nextAttackTime)
            {
                if (Input.GetButtonDown("Fire2") && checkControlls == 0)
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }

            //Freccette
            if (checkControlls == 1)
            {
                horizontal = Input.GetAxisRaw("Horizontal1") * speed;
                animator.SetFloat("Speed", Mathf.Abs(horizontal));
            }

            if (Input.GetButtonDown("Jump1") && checkControlls == 1)
            {
                jump = true;
                animator.SetBool("IsJumping", true);
                StartCoroutine("Jump");
            }

            if (Input.GetButtonDown("Crouch1") && checkControlls == 1)
            {
                crouch = true;
            }
            else if (Input.GetButtonUp("Crouch1") && checkControlls == 1)
            {
                crouch = false;
            }

            if (Time.time >= nextAttackTime)
            {
                if (Input.GetButtonDown("Fire1") && checkControlls == 1)
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    private void FixedUpdate()
    {
        if (knockbackCount <= 0)
        {
            controller.Move(horizontal * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }
        else
        {
            if (knockFromRight)
            {
                myRigidbody2D.velocity = new Vector2(-knockback, 0);
            }
            if (!knockFromRight)
            {
                myRigidbody2D.velocity = new Vector2(knockback, 0);
            }

            knockbackCount -= Time.deltaTime;
        }
    }

    IEnumerator Jump()
    {
        while (controller.IsGrounded)
        {
            yield return null;
        }
        while (true)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack"))
            {
                animator.SetBool("AttackJump", true);
                yield return new WaitForSeconds(0.3f);
                animator.SetBool("AttackJump", false);
            }
            if (controller.IsGrounded)
            {
                animator.SetBool("IsJumping", false);
                StopCoroutine("Jump");
            }
            yield return null;
        }
    }

    //Player Combat

    void Attack()
    {
        animator.SetTrigger("IsAttacking");
        SoundManager.instance.Play("Attack");

        Collider2D hitEnemies = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayer);

        if (hitEnemies != null)
        {
            hitEnemies.GetComponent<Enemy>().TakeDamage(attackDamage);
            hitEnemies.GetComponent<Enemy>().GetKnocked(gameObject.transform);
        }
    }

    //Per il serialize field dell'attack range
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Rispettivamente spine e vuoto (quando cadi)
        if (collision.CompareTag("DamageObject") || collision.CompareTag("Void"))
        {
            GameManager.instance.GameOver();
        }

        //Urto con i nemici
        if (Time.time > enemyNextAttackTime)
        {
            if (collision.CompareTag("Enemy"))
            {
                GetKnocked(collision);
                if (collision.GetComponentInParent<Enemy>().tipo == Enemy.EnemyType.boss)
                {
                    TakeDamage(collision.GetComponentInParent<Enemy>().attackDamage);
                }
                else
                {
                    TakeDamage(collision.GetComponent<Enemy>().attackDamage);
                }
            }
            enemyNextAttackTime = Time.time + enemyAttackRate;
        }
    }

    public void TakeDamage(float dmg)
    {
        animator.SetTrigger("Hurt");
        SoundManager.instance.Play("DamageSound");
        currentHealth -= dmg;
        ScoreManager.instance.ChangeScore(-5f);
        healthbar.setHealth(currentHealth);
        knockbackCount = knockbackLenght;

        if (currentHealth <= 0)
        {
            GameManager.instance.GameOver();
        }
    }
  
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Knockback del personaggio se sale sui nemici
        if (Time.time > enemyNextAttackTime)
        {
            if (collision.CompareTag("Enemy"))
            {
                if (!controller.IsGrounded)
                {
                    GetKnocked(collision);
                }
            }
        }
    }
    public void GetKnocked(Collider2D other)
    {
        if (transform.position.x < other.transform.position.x)
            knockFromRight = true;
        else
            knockFromRight = false;
    }

    public void GetKnocked(Transform other, bool inversa)
    {
        if (transform.position.x < other.position.x)
            knockFromRight = true;
        else
            knockFromRight = false;
    }
    //Heal
    public void PlayerHeal(float heal)
    {
        currentHealth += heal;
        healthbar.setHealth(currentHealth);
    }

    public void boostDamage(float dmg)
    {
        attackDamage += dmg;
    }

    public void boostHp(float hp)
    {
        health += hp;
        healthbar = healthbarPost;
        healthbar.setMaxHealth(health);
        healthbar.setHealth(currentHealth);
    }

    public float GetMaxHp()
    {
        return health;
    }

    public float GetAttackDamage()
    {
        return attackDamage;
    }

}

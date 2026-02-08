using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Transform target;
    private Vector2 direction, bulletPos;
    [SerializeField] float fireballSpeed;
    public float fireballDamage;
    private Player player;
    

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        bulletPos = new Vector2(transform.position.x, transform.position.y);
        direction = (Vector2)target.position - bulletPos;    
    }
    void Update()
    {
        if (direction.x < 0)
            transform.Translate(Vector3.left * fireballSpeed * Time.deltaTime);
        else
            transform.Translate(Vector3.right * fireballSpeed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<Player>();
            player.GetKnocked(transform, true);
            player.TakeDamage(fireballDamage);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Walls"))
        {
            Destroy(gameObject);
        }
    }
}

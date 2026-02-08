using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactite : MonoBehaviour
{
    [SerializeField] float damage = 50;
    [SerializeField] GameObject particle;
    private Player player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Walls"))
        {
            Destroy(gameObject);
            Instantiate(particle, gameObject.transform.position, Quaternion.identity);
            SoundManager.instance.Play("stalactitebreak");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<Player>();
            player.GetKnocked(transform, true);
            player.TakeDamage(damage);
            Instantiate(particle, gameObject.transform.position, Quaternion.identity);
            SoundManager.instance.Play("stalactitebreak");
            Destroy(gameObject);
        }

    }
}

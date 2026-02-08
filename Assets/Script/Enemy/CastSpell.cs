using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSpell : MonoBehaviour
{
    private Player player;
    [SerializeField] float spellDamage = 25;

    public void DamageSpell()
    {
        GetComponent<CapsuleCollider2D>().enabled = true;
        SoundManager.instance.Play("SpellBoss");
    }

    public void StopSpell()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
    }

    public void DestroySpell()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerItemCollider"))
        {
            player = collision.GetComponentInParent<Player>();
            player.GetKnocked(transform, true);
            player.TakeDamage(spellDamage);
            Destroy(gameObject);
        }
    }
}

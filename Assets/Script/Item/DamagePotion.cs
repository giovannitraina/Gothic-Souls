using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePotion : MonoBehaviour
{
    [SerializeField] float damageBoost = 10f;
    private Player player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerItemCollider"))
        {
            player = other.gameObject.GetComponentInParent<Player>();
            player.boostDamage(damageBoost);
            SoundManager.instance.Play("Potions");
            Destroy(gameObject);
        }
    }
}

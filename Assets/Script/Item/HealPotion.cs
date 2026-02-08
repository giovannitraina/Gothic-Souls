using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : MonoBehaviour
{
    [SerializeField] float heal = 20f;
    private Player player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerItemCollider"))
        {
            player = other.gameObject.GetComponentInParent<Player>();
            SoundManager.instance.Play("Potions");
            if (player.currentHealth < player.GetMaxHp())
            {
                player.PlayerHeal(heal);
            }
            Destroy(gameObject);
        }
    }

}


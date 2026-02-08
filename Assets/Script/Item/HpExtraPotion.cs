using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpExtraPotion : MonoBehaviour
{
    [SerializeField] float hpBoost = 20f;
    [SerializeField] GameObject healthInit, healthPost;
    private Player player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerItemCollider"))
        {
            player = other.gameObject.GetComponentInParent<Player>();
            healthInit.SetActive(false);
            healthPost.SetActive(true);
            player.boostHp(hpBoost);
            SoundManager.instance.Play("Potions");
            Destroy(gameObject);
        }
    }
}

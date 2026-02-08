using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int coinvalue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerItemCollider"))
        {
            SoundManager.instance.Play("SoulSound");
            gameObject.SetActive(false);
            CoinManager.instance.AddCoin(coinvalue);
        }
    }
}


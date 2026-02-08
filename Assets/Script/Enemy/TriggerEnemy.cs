using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEnemy : MonoBehaviour
{
    [SerializeField] GameObject stalactite, boss;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Livello1"))
            {
                if (gameObject.name == "Trigger1")
                {
                    EnemyManager.instance.SpawnEnemy(0);
                }
                if (gameObject.name == "Trigger2")
                {
                    EnemyManager.instance.SpawnEnemy(1);
                    EnemyManager.instance.SpawnEnemy(2);
                    EnemyManager.instance.SpawnEnemy(3);
                    EnemyManager.instance.SpawnEnemy(4);
                }
                if (gameObject.name == "Trigger3")
                {
                    EnemyManager.instance.SpawnEnemy(5);
                    EnemyManager.instance.SpawnEnemy(6);
                }
                if (gameObject.name == "Trigger4")
                {
                    EnemyManager.instance.SpawnEnemy(7);
                }
                if (gameObject.name == "Trigger5")
                {
                    EnemyManager.instance.SpawnEnemy(8);
                    EnemyManager.instance.SpawnEnemy(9);
                }
                if (gameObject.name == "Trigger6")
                {
                    EnemyManager.instance.SpawnEnemy(10);
                }
                if (gameObject.name == "Trigger7")
                {
                    EnemyManager.instance.SpawnEnemy(11);
                    EnemyManager.instance.SpawnEnemy(12);
                }
                if (gameObject.name == "Trigger8")
                {
                    EnemyManager.instance.SpawnEnemy(13);
                    EnemyManager.instance.SpawnEnemy(14);
                    EnemyManager.instance.SpawnEnemy(15);
                    EnemyManager.instance.SpawnEnemy(16);
                    EnemyManager.instance.SpawnEnemy(17);
                    EnemyManager.instance.SpawnEnemy(18);
                }
                if (gameObject.name == "Trigger9")
                {
                    EnemyManager.instance.SpawnEnemy(19);
                    EnemyManager.instance.SpawnEnemy(20);
                    EnemyManager.instance.SpawnEnemy(21);
                }
            }

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Livello2"))
            {
                if (stalactite != null)
                {
                    stalactite.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                }

                if (gameObject.name == "Trigger1")
                {
                    EnemyManager.instance.SpawnEnemy(0);
                }
                if (gameObject.name == "Trigger2")
                {
                    EnemyManager.instance.SpawnEnemy(1);
                    EnemyManager.instance.SpawnEnemy(2);
                    EnemyManager.instance.SpawnEnemy(3);
                }
                if (gameObject.name == "Trigger3")
                {
                    EnemyManager.instance.SpawnEnemy(4);
                }
                if (gameObject.name == "Trigger4")
                {
                    EnemyManager.instance.SpawnEnemy(5);
                    EnemyManager.instance.SpawnEnemy(6);
                    EnemyManager.instance.SpawnEnemy(8);
                }
                if (gameObject.name == "Trigger5")
                {
                    EnemyManager.instance.SpawnEnemy(7);
                }
                if (gameObject.name == "Trigger6")
                {
                    EnemyManager.instance.SpawnEnemy(9);
                }
                if (gameObject.name == "Trigger7")
                {
                    EnemyManager.instance.SpawnEnemy(10);
                    EnemyManager.instance.SpawnEnemy(11);
                    EnemyManager.instance.SpawnEnemy(12);
                }
                if (gameObject.name == "Trigger8")
                {
                    EnemyManager.instance.SpawnEnemy(13);
                }
                if (gameObject.name == "Trigger9")
                {
                    EnemyManager.instance.SpawnEnemy(14);
                    EnemyManager.instance.SpawnEnemy(15);
                }
                if (gameObject.name == "Trigger13")
                {
                    EnemyManager.instance.SpawnEnemy(16);
                    EnemyManager.instance.SpawnEnemy(17);
                    EnemyManager.instance.SpawnEnemy(18);
                }
                if (gameObject.name == "Trigger14")
                {
                    EnemyManager.instance.SpawnEnemy(19);
                }
                if (gameObject.name == "Trigger15")
                {
                    EnemyManager.instance.SpawnEnemy(20);
                    EnemyManager.instance.SpawnEnemy(21);
                    EnemyManager.instance.SpawnEnemy(22);
                }
                if (gameObject.name == "Trigger16")
                {
                    EnemyManager.instance.SpawnEnemy(23);
                }
                if (gameObject.name == "Trigger17")
                {
                    EnemyManager.instance.SpawnEnemy(24);
                }
                if (gameObject.name == "Trigger18")
                {
                    EnemyManager.instance.SpawnEnemy(25);
                    EnemyManager.instance.SpawnEnemy(26);
                }
                if (gameObject.name == "Trigger20")
                {
                    EnemyManager.instance.SpawnEnemy(27);
                    EnemyManager.instance.SpawnEnemy(28);
                }

            }

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Livello3"))
            {
                if (gameObject.name == "Trigger1")
                {
                    boss.SetActive(true);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}


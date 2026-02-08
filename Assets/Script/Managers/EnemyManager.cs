using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemy;
    [SerializeField] int[] tipologia; // =0 se scheletri, =1 se maghi
    [SerializeField] GameObject[][] enemyList;
    [SerializeField] int enemyLenghtTotal;
    [SerializeField] int[] enemyLenghtSpawnPoint;
    [SerializeField] Transform[] spawnPoint;
    [SerializeField] int spawnPointLenght;
    [SerializeField] int[] enemyCounter;

    public static EnemyManager instance;


    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        enemyList = new GameObject[spawnPointLenght][];
        for (int h = 0; h < spawnPointLenght; h++)
        {
            enemyList[h] = new GameObject[enemyLenghtSpawnPoint[h]];
        }

        for (int h = 0; h < spawnPointLenght; h++)
        {
            for (int j = 0; j < enemyLenghtSpawnPoint[h]; j++)
            {
                enemyList[h][j] = Instantiate(enemy[tipologia[h]], spawnPoint[h].position, spawnPoint[h].rotation);
                enemyList[h][j].SetActive(false);
            }
        }
    }

    public void SpawnEnemy(int indice)
    {
        for (int j = enemyCounter[indice]; j < enemyLenghtSpawnPoint[indice]; j++)
        {
            if (!enemyList[indice][j].activeInHierarchy)
            {

                enemyList[indice][j].GetComponent<Enemy>().Spawn(spawnPoint[indice]);
                enemyCounter[indice] = j + 1;
                break;
            }
            if (enemyCounter[indice] == enemyLenghtSpawnPoint[indice])
                enemyCounter[indice] = 0;
        }
    }
}

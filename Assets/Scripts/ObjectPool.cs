using UnityEngine;
using System.Collections;

public class ObjectPool : MonoBehaviour
{
    private GameObject[] enemies = null;

    public GameObject enemiesInstantiated;

    public int poolSize = 0;

    void Start ()
    {
        enemies = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            enemies[i] = Instantiate(enemiesInstantiated) as GameObject;
            enemies[i].transform.parent = gameObject.transform;
            enemies[i].SetActive(false);
        }

    }

    public void ActivateEnemiesInstantiated()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (enemies[i].activeInHierarchy == false)
            {
                enemies[i].SetActive(true);
                return;
            }
        }
    }

    public void DisableEnemiesInstantiated()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (enemies[i].activeInHierarchy == true)
            {
                enemies[i].SetActive(false);
            }
        }
    }
}

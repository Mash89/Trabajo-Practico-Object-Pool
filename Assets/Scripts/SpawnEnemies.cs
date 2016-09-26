using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour
{

	void Update ()
    {
        ObjectPool activate = GameObject.Find("ObjectPool").GetComponent<ObjectPool>();
        if (Input.GetKeyDown(KeyCode.Space)) activate.ActivateEnemiesInstantiated();
        if (Input.GetKeyDown(KeyCode.R)) activate.DisableEnemiesInstantiated();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMore : MonoBehaviour
{
    public GameObject itemToSpawn;
    public int numberOfItems = 40;
    public float timer;

	void Start ()
    {
	}
	
	void Update ()
    {
        timer += Time.deltaTime;

        if (numberOfItems > 0 && timer > 0.30f)
        {
            GameObject item = Instantiate(itemToSpawn);
            numberOfItems--;
            timer = 0;
        }
		
	}
}

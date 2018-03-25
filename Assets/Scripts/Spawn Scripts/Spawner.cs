using UnityEngine;


//-----------------------------------------------------------------------------------------------------------------------------------------------------
//
//  SPAWNER
//
//  Contains:   A GameObject item to spawn;
//              A timer Limit;
//              A timer;
//              A bool to check whether items should spawn finitely;
//              An int to keep track of the number of items to spawn, if the above bool is true;
//
//  Use:        Place on an empty GameObject in the scene;
//              Drag your chosen Prefab to spawn to the ItemToSpawn variable in the Inspector;
//              Drag the GameObject around to get it in the desired position;
//
//  Purpose:    To spawn multiple dynamic GameObjects during runtime;
//
//-----------------------------------------------------------------------------------------------------------------------------------------------------
//
//  Notes:      N/A;
//
//-----------------------------------------------------------------------------------------------------------------------------------------------------


public class Spawner : MonoBehaviour
{
    public GameObject itemToSpawn;
    public float timerLimit = 0.30f;
    public float timer;                             [Space(10)]

    public bool spawnFiniteNumber = true;
    public int numberOfItems;

	void Start ()
    {
	}
	
	void Update ()
    {
        timer += Time.deltaTime;

        if (numberOfItems > 0 && timer > timerLimit)
        {
            Instantiate(itemToSpawn, transform.position, transform.rotation);
            timer = 0;

            if(spawnFiniteNumber)
                numberOfItems--;
        }
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//spawns a random object in place of this spawnpoint
public class SpawnPoint : MonoBehaviour
{
    [Tooltip("A random gameobject out of this selection will be spawned in the place of this spawnpoint")]
    [SerializeField]
    private GameObject[] spawnableObjects;

   public void SpawnRandomObject(int seed)
    {
        Random.InitState(seed);
        int randomIndex = Random.Range(0, spawnableObjects.Length);
        
        //spawn and parent to parent of spawnpoint
        GameObject spawnedObject = Instantiate(spawnableObjects[randomIndex], this.transform.position, this.transform.rotation, this.transform.parent);
        
        //destroy spawnpoint
        Destroy(this.gameObject);
    }

}

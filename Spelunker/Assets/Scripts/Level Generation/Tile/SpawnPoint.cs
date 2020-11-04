using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
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

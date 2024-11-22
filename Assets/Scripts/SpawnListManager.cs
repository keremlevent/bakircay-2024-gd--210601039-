using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnListManager : MonoBehaviour
{
    public List<GameObject> PrepareSpawnList(GameObject[] objects)
    {
        // Prefab listesini iki kez ekleyerek her objenin çift olmasýný saðla
        List<GameObject> spawnList = new List<GameObject>();

        foreach (GameObject obj in objects)
        {
            spawnList.Add(obj); // Ýlk kopya
            spawnList.Add(obj); // Ýkinci kopya
        }

        // Spawn sýrasýný karýþtýr
        for (int i = 0; i < spawnList.Count; i++)
        {
            GameObject temp = spawnList[i];
            int randomIndex = Random.Range(0, spawnList.Count);
            spawnList[i] = spawnList[randomIndex];
            spawnList[randomIndex] = temp;
        }

        return spawnList;
    }
}

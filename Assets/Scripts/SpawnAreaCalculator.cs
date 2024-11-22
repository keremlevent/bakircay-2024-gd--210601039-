using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAreaCalculator : MonoBehaviour
{
    private Vector3 spawnAreaMin;
    private Vector3 spawnAreaMax;

    public void CalculateSpawnArea()
    {
        // Kameran�n alt k��esi ve �st k��esini hesapla
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane + 1));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane + 1));

        // Spawn alan�, ekran�n g�r�nen k�sm� kadar olur
        spawnAreaMin = new Vector3(bottomLeft.x , bottomLeft.y + 1, bottomLeft.z);
        spawnAreaMax = new Vector3(topRight.x, topRight.y, topRight.z);
    }

    public Vector3 GetRandomSpawnPosition(Vector3 matchingAreaMin, Vector3 matchingAreaMax)
    {
        Vector3 spawnPosition;

        // Rastgele pozisyon se�
        do
        {
            spawnPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                spawnAreaMin.y,
                Random.Range(spawnAreaMin.z, spawnAreaMax.z)
            );
        }
        // E�le�tirme alan�n�n i�inde spawnlanmamas� i�in kontrol et
        while (spawnPosition.x >= matchingAreaMin.x && spawnPosition.x <= matchingAreaMax.x &&
               spawnPosition.z >= matchingAreaMin.z && spawnPosition.z <= matchingAreaMax.z);

        return spawnPosition;
    }
}

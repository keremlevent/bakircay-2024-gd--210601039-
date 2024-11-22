using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objects; // Spawnlanacak prefab objeleri
    public Vector3 matchingAreaMin;  // E�le�tirme alan�n�n sol alt k��esi
    public Vector3 matchingAreaMax;  // E�le�tirme alan�n�n sa� �st k��esi
    private Vector3 spawnAreaMin;
    private Vector3 spawnAreaMax;

    void Start()
    {
        CalculateSpawnArea(); // Spawn alan�n� hesapla

        // Nesnelerin �ift olarak spawnlanmas� i�in prefab listesini d�zenle
        List<GameObject> spawnList = PrepareSpawnList();

        foreach (GameObject obj in spawnList)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject spawnedObj = Instantiate(obj, spawnPosition, Quaternion.identity);

            // Fiziksel �zellikler ekle
            AddPhysics(spawnedObj);

            // DraggableObject scriptini ekle
            AddDraggableComponent(spawnedObj);
        }
    }

    void CalculateSpawnArea()
    {
        // Kameran�n alt k��esi ve �st k��esini hesapla
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane + 1));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane + 1));

        // Spawn alan�, ekran�n g�r�nen k�sm� kadar olur
        spawnAreaMin = new Vector3(bottomLeft.x, bottomLeft.y + 1, bottomLeft.z);
        spawnAreaMax = new Vector3(topRight.x, topRight.y, topRight.z);
    }

    Vector3 GetRandomSpawnPosition()
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

    List<GameObject> PrepareSpawnList()
    {
        // Prefab listesini iki kez ekleyerek her objenin �ift olmas�n� sa�la
        List<GameObject> spawnList = new List<GameObject>();

        foreach (GameObject obj in objects)
        {
            spawnList.Add(obj); // �lk kopya
            spawnList.Add(obj); // �kinci kopya
        }

        // Spawn s�ras�n� kar��t�r
        for (int i = 0; i < spawnList.Count; i++)
        {
            GameObject temp = spawnList[i];
            int randomIndex = Random.Range(0, spawnList.Count);
            spawnList[i] = spawnList[randomIndex];
            spawnList[randomIndex] = temp;
        }

        return spawnList;
    }

    // Yeni nesnelere fiziksel �zellikler ekleyen metod
    private void AddPhysics(GameObject obj)
    {
        // Rigidbody ekle (yer �ekimi ve fiziksel etkile�imler i�in)
        if (obj.GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = obj.AddComponent<Rigidbody>();
            rb.useGravity = true; // Yer�ekimi etkin
            rb.mass = 1f;         // Varsay�lan k�tle
            rb.isKinematic = false;
            Debug.Log($"Rigidbody eklendi: {obj.name}");
        }

        // Collider ekle (�arp��ma alg�lamas� i�in)
        if (obj.GetComponent<Collider>() == null)
        {
            obj.AddComponent<BoxCollider>(); // Veya ihtiyaca g�re ba�ka bir collider tipi
            Debug.Log($"Collider eklendi: {obj.name}");
        }
    }

    // DraggableObject scriptini ekleyen metod
    private void AddDraggableComponent(GameObject obj)
    {
        if (obj.GetComponent<DraggableObject>() == null)
        {
            obj.AddComponent<DraggableObject>();
            Debug.Log($"DraggableObject script'i eklendi: {obj.name}");
        }
    }
}

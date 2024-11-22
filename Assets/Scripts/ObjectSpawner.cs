using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objects; // Spawnlanacak prefab objeleri
    public Vector3 matchingAreaMin;  // Eþleþtirme alanýnýn sol alt köþesi
    public Vector3 matchingAreaMax;  // Eþleþtirme alanýnýn sað üst köþesi
    private Vector3 spawnAreaMin;
    private Vector3 spawnAreaMax;

    void Start()
    {
        CalculateSpawnArea(); // Spawn alanýný hesapla

        // Nesnelerin çift olarak spawnlanmasý için prefab listesini düzenle
        List<GameObject> spawnList = PrepareSpawnList();

        foreach (GameObject obj in spawnList)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject spawnedObj = Instantiate(obj, spawnPosition, Quaternion.identity);

            // Fiziksel özellikler ekle
            AddPhysics(spawnedObj);

            // DraggableObject scriptini ekle
            AddDraggableComponent(spawnedObj);
        }
    }

    void CalculateSpawnArea()
    {
        // Kameranýn alt köþesi ve üst köþesini hesapla
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane + 1));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane + 1));

        // Spawn alaný, ekranýn görünen kýsmý kadar olur
        spawnAreaMin = new Vector3(bottomLeft.x, bottomLeft.y + 1, bottomLeft.z);
        spawnAreaMax = new Vector3(topRight.x, topRight.y, topRight.z);
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector3 spawnPosition;

        // Rastgele pozisyon seç
        do
        {
            spawnPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                spawnAreaMin.y,
                Random.Range(spawnAreaMin.z, spawnAreaMax.z)
            );
        }
        // Eþleþtirme alanýnýn içinde spawnlanmamasý için kontrol et
        while (spawnPosition.x >= matchingAreaMin.x && spawnPosition.x <= matchingAreaMax.x &&
               spawnPosition.z >= matchingAreaMin.z && spawnPosition.z <= matchingAreaMax.z);

        return spawnPosition;
    }

    List<GameObject> PrepareSpawnList()
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

    // Yeni nesnelere fiziksel özellikler ekleyen metod
    private void AddPhysics(GameObject obj)
    {
        // Rigidbody ekle (yer çekimi ve fiziksel etkileþimler için)
        if (obj.GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = obj.AddComponent<Rigidbody>();
            rb.useGravity = true; // Yerçekimi etkin
            rb.mass = 1f;         // Varsayýlan kütle
            rb.isKinematic = false;
            Debug.Log($"Rigidbody eklendi: {obj.name}");
        }

        // Collider ekle (çarpýþma algýlamasý için)
        if (obj.GetComponent<Collider>() == null)
        {
            obj.AddComponent<BoxCollider>(); // Veya ihtiyaca göre baþka bir collider tipi
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

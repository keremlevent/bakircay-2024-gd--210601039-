using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsAssigner : MonoBehaviour
{
    // Nesnelere fiziksel özellikler ekler
    public void AddPhysics(GameObject obj)
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
}

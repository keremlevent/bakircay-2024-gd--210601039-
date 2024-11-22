using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsAssigner : MonoBehaviour
{
    // Nesnelere fiziksel �zellikler ekler
    public void AddPhysics(GameObject obj)
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
}

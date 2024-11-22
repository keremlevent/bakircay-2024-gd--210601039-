using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Rigidbody objectRigidbody;
    private Collider objectCollider;

    void Start()
    {
        // Nesnenin Rigidbody ve Collider'ýný al
        objectRigidbody = GetComponent<Rigidbody>();
        objectCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    isDragging = true;
                    offset = transform.position - hit.point;

                    // Yer çekimini devre dýþý býrak
                    if (objectRigidbody != null)
                    {
                        objectRigidbody.useGravity = false;
                    }

                    // Collider'ý devre dýþý býrak
                    if (objectCollider != null)
                    {
                        objectCollider.enabled = false;
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;

            // Yer çekimini tekrar etkinleþtir
            if (objectRigidbody != null)
            {
                objectRigidbody.useGravity = true;
            }

            // Collider'ý tekrar etkinleþtir
            if (objectCollider != null)
            {
                objectCollider.enabled = true;
            }
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == transform)
                    {
                        isDragging = true;
                        offset = transform.position - hit.point;

                        // Yer çekimini devre dýþý býrak
                        if (objectRigidbody != null)
                        {
                            objectRigidbody.useGravity = false;
                        }

                        // Collider'ý devre dýþý býrak
                        if (objectCollider != null)
                        {
                            objectCollider.enabled = false;
                        }
                    }
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;

                // Yer çekimini tekrar etkinleþtir
                if (objectRigidbody != null)
                {
                    objectRigidbody.useGravity = true;
                }

                // Collider'ý tekrar etkinleþtir
                if (objectCollider != null)
                {
                    objectCollider.enabled = true;
                }
            }
        }

        if (isDragging)
        {
            Vector3 currentPosition = GetCurrentInputPosition();
            // Y eksenini sabitle ve sadece X ve Z eksenlerinde hareket etmesini saðla
            transform.position = new Vector3(currentPosition.x, transform.position.y, currentPosition.z);
        }
    }

    private Vector3 GetCurrentInputPosition()
    {
        if (Input.touchCount > 0)
        {
            return Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }
        else if (Input.GetMouseButton(0))
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        return transform.position;
    }
}

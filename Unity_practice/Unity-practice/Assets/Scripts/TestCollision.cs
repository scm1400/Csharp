using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    // 1) 나한테 RigidBody가 있어야 한다 (IsKinematic: Off)
    // 2) 나한테 Collider가 있어야 한다 (IsTrigger: Off)
    // 3) 상대한테 Collider가 있어야 한다 (IsTrigger: Off)
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision!");
    }

    // 1) 둘 다 Collider가 있어야 한다.
    // 2) 둘 중 하나는 IsTrigger: On
    // 3) 둘 중 하나는 Rigid body
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger!");
    }

    void Start()
    {
    }

    void Update()
    {
        
        Vector3 look = transform.TransformDirection(Vector3.forward); // world to local
        Debug.DrawRay(transform.position + Vector3.up, look * 10, Color.red);

        //  여러개를 관통할 경우 관통한 모든 오브젝트를 인식함 //
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position + Vector3.up, look, 10);

        foreach (RaycastHit hit in hits)
        {
            Debug.Log($"Raycast {hit.collider.gameObject.name}");
        }

        
        //  여러개를 관통할 경우 처음 관통한 오브젝트를 인식함 //
        // RaycastHit hit;
        // if (Physics.Raycast(transform.position + Vector3.up, look, out hit, 10)) ;
        // {
        //     Debug.Log($"Raycast {hit.collider.gameObject.name}!");
        // }
    }
}
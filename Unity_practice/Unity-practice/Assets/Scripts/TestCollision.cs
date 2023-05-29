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
        // Debug.Log(Input.mousePosition);

        // Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition));

        // 카메라에서 클릭지점으로 ray 발사 ( 유니티에 구현된 기능 활용 )
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
            }
        }
        
        // 카메라에서 클릭지점으로 ray 발사 ( 상세구현 )
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
        //         Camera.main.nearClipPlane));
        //     Vector3 dir = mousePos - Camera.main.transform.position;
        //     dir = dir.normalized;
        //     Debug.DrawRay(Camera.main.transform.position, dir * 100.0f, Color.red, 1.0f);
        //     RaycastHit hit;
        //     if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 100.0f))
        //     {
        //         Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
        //     }
        // }


        //  raycast에 사용하는 공통 코드 //
        // Vector3 look = transform.TransformDirection(Vector3.forward); // local to world
        // Debug.DrawRay(transform.position + Vector3.up, look * 10, Color.red);

        // RaycastAll 여러개를 관통할 경우 관통한 모든 오브젝트를 인식함 //
        // RaycastHit[] hits;
        // hits = Physics.RaycastAll(transform.position + Vector3.up, look, 10);
        //
        // foreach (RaycastHit hit in hits)
        // {
        //     Debug.Log($"Raycast {hit.collider.gameObject.name}");
        // }

        //  Raycast 여러개를 관통할 경우 처음 관통한 오브젝트를 인식함 //
        // RaycastHit hit;
        // if (Physics.Raycast(transform.position + Vector3.up, look, out hit, 10)) ;
        // {
        //     Debug.Log($"Raycast {hit.collider.gameObject.name}!");
        // }
    }
}
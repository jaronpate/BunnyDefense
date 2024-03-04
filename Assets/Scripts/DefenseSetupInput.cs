using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseSetupInput : MonoBehaviour
{
    Camera m_Camera;
    public GameObject tower_prefab;

    void Awake()
    {
        m_Camera = Camera.main;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("Click detected");
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = m_Camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                print(hit.collider.gameObject.name);
                print(hit.point);
                Instantiate(tower_prefab, hit.point, Quaternion.identity);
            }
        }
    }
}

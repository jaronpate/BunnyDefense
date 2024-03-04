using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool moving = true;
    public float speed = 3f;

    private Transform target;
    private int node_index = 0;

    // Start is called before the first frame update
    void Start()
    {
        speed *= Random.Range(1f, 1.5f);
        target = PathController.nodes[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(speed * Time.deltaTime * dir.normalized, Space.World);
        }

        if (Vector3.Distance(transform.position, target.position) <= 0.05f)
        {
            GetNextTargetNode();
        }
    }

    void GetNextTargetNode()
    {
        if (node_index < (PathController.nodes.Length - 1))
        {
            node_index++;
            target = PathController.nodes[node_index];
        } else
        {
            Destroy(gameObject);
            return;
        }
    }
}

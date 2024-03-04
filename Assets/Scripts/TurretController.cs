using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TurretController : MonoBehaviour
{

    public Transform turret_rotation_point;
    public float turret_radius = 3f;
    public float rotation_speed = 150f;
    public LayerMask enemy_layer;
    private GameObject current_target;


    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, turret_radius);
    }

    // Update is called once per frame
    void Update()
    {
        if (!current_target)
        {
            FindTarget();
        } else
        {
            FaceCurrentTarget();

            if (!CheckTargetDistance())
            {
                current_target = null;
            }
        }
    }

    void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, turret_radius, (Vector2)transform.position, 0f, enemy_layer);

        if (hits.Length > 0)
        {
            current_target = hits[hits.Length - 1].collider.gameObject;
        }
    }

    private bool CheckTargetDistance()
    {
        return Vector2.Distance(current_target.transform.position, transform.position) <= turret_radius;
    }

    void FaceCurrentTarget()
    {
        if (current_target)
        {
            float angle = Mathf.Atan2(current_target.transform.position.y - transform.position.y, current_target.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            // transform.rotation = rotation;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotation_speed * Time.deltaTime);
        }
    }
}

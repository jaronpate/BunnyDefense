using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject enemy;
    float time_since_spawn = 0f;
    float spawn_frequency = 1f;

    // Update is called once per frame
    void Update()
    {
        time_since_spawn += Time.deltaTime;

        if (spawn_frequency < time_since_spawn)
        {
            var seed = Random.Range(0f, 1f);

            if (seed > 0.5f)
            {
                Instantiate(enemy, transform.position, Quaternion.identity);
                time_since_spawn = 0f;
            }
        }
    }
}

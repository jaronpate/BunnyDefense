using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridController : MonoBehaviour
{
    // Configured
    public int width, height;
    public GameObject tile_prefab;
    public Transform main_cam;
    public GameObject unit_prefab;
    public GameObject spawner_prefab;
    public GameObject path_manager;
    public GameObject node_prefab;


    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
    }

    void GenerateGrid()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var spawned = Instantiate(tile_prefab, new Vector3(x + 0.5f, y + 0.5f), Quaternion.identity, this.transform);
                spawned.name = $"Tile {x} {y}";

                var spawned_controller = spawned.GetComponent<TileController>();

                var is_offset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawned_controller.Init(is_offset, this);
            }
        }

        main_cam.transform.position = new Vector3((float)width / 2, (float)height / 2, -10);
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawn(GameObject tile, String type)
    {
        var tile_controller = tile.GetComponent<TileController>();

        if (tile_controller.type == "unit")
        {
            if (tile_controller.unit)
            {
                Destroy(tile_controller.unit);
                tile_controller.unit = Instantiate(node_prefab, tile.transform.position, Quaternion.identity, path_manager.transform);
                path_manager.GetComponent<PathController>().Init();
                tile_controller.type = "path";
            } else
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    tile_controller.unit = Instantiate(spawner_prefab, tile.transform.position, Quaternion.identity);
                } else if (Input.GetKey(KeyCode.LeftAlt))
                {
                }
                else
                {
                    tile_controller.unit = Instantiate(unit_prefab, tile.transform.position, Quaternion.identity);
                }
            }
        } else if (tile_controller.type == "path")
        {
            Destroy(tile_controller.unit);
            tile_controller.unit = null;
            tile_controller.type = "unit";
        } else
        {

        }
    }
}

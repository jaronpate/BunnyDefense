using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField]
    private Color base_color, offset_color, highlight_color, path_color;
    private SpriteRenderer _renderer;

    private bool highlighted = false;
    private bool active = false;
    private bool offset_tile = false;
    public string type = "unit";
    public GameObject unit;

    private GridController grid_controller;
    

    public void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
    public void Init(bool is_offset, GridController grid_controller)
    {
        offset_tile = is_offset;
        this.grid_controller = grid_controller;
    }
    private void Update()
    {
        if (type == "path")
        {
            _renderer.color = path_color;
        } else if (highlighted)
        {
            _renderer.color = highlight_color;
        } else
        {
            _renderer.color = offset_tile ? offset_color : base_color;
        }
    }

    private void OnMouseEnter()
    {
        highlighted = true;
        active = true;
    }

    private void OnMouseExit()
    {
        highlighted = false;
        active = false;
    }

    private void OnMouseDown()
    {
        grid_controller.Spawn(gameObject, type);
    }
}

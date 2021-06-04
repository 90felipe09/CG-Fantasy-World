using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridView : MonoBehaviour
{
    [SerializeField] private int gridSize;
    [SerializeField] private int gridFloor;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private List<GameObject> tiles;

    [SerializeField] private Shader outlinedShader;
    [SerializeField] private Shader defaultShader;

    private GridModel gridModel;

    void Start()
    {
        gridModel = new GridModel(gridSize);
        mountGrid();
    }

    void Update()
    {
        
    }

    private void mountGrid()
    {
        for (int tileIndex = 0; tileIndex < gridModel.layout.Count; tileIndex++)
        {
            GameObject newTile = Instantiate(tilePrefab, this.transform);
            newTile.transform.localPosition = gridModel.layout[tileIndex].getPosition();
            tiles.Add(newTile);
        }
    }

    public void setGridFloor(int floor)
    {
        gridFloor = floor;
    }

    public void deactivateShader()
    {
        for (int tileIndex = 0; tileIndex < tiles.Count; tileIndex++)
        {
            tiles[tileIndex].GetComponent<Renderer>().material.shader = defaultShader;
        }
    }

    public void activateShader()
    {
        for (int tileIndex = 0; tileIndex < tiles.Count; tileIndex++)
        {
            tiles[tileIndex].GetComponent<Renderer>().material.shader = outlinedShader;
        }
    }
}

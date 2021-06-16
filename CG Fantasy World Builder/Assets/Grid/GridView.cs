using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridView : MonoBehaviour
{
    [SerializeField] private int gridSize;
    [SerializeField] private int gridFloor;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private List<GameObject> tiles;

    private bool isActive;

    [SerializeField] private Material outlinedMaterial;
    [SerializeField] private Material defaultMaterial;

    private GridModel gridModel;

    void Start()
    {
        gridModel = new GridModel(gridSize);
        mountGrid();
    }

    void Update()
    {
        handleTileControl();
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

    public void deactivateGrid()
    {
        isActive = false;
        for (int tileIndex = 0; tileIndex < tiles.Count; tileIndex++)
        {
            tiles[tileIndex].GetComponent<TileView>().setDefaultTileMaterial(defaultMaterial);
        }
    }

    public void activateGrid()
    {
        isActive = true;
        for (int tileIndex = 0; tileIndex < tiles.Count; tileIndex++)
        {
            tiles[tileIndex].GetComponent<TileView>().setDefaultTileMaterial(outlinedMaterial);
        }
    }

    public void handleTileControl()
    {
        for (int tileIndex = 0; tileIndex < tiles.Count; tileIndex++)
        {
            tiles[tileIndex].GetComponent<TileView>().handleMouseHover(isActive);
        }
    }
}

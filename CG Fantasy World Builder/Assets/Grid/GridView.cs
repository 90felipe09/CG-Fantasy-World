using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridView : MonoBehaviour
{
    [SerializeField] private int gridSize;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private List<GameObject> tiles;

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
        for (int tileIndex = 0; tileIndex < gridSize * gridSize; tileIndex++)
        {
            GameObject newTile = Instantiate(tilePrefab, this.transform);
            newTile.transform.localPosition = gridModel.layout[tileIndex].getPosition();
            tiles.Add(newTile);
        }
    }
}

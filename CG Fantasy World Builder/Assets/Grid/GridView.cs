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

    private Transform hoveredTile;

    private GridModel gridModel;

    [SerializeField] private UserController userController;

    void Start()
    {
        gridModel = new GridModel(gridSize);
        mountGrid();
    }


    public void setUserController(UserController userController)
    {
        this.userController = userController;
    }

    void Update()
    {
        handleTileControl();
    }

    public void putFloor()
    {   
        if (userController.getCurrentEditMode() == UserController.EditModeEnum.floor)
        {
            GameObject floorToPut = userController.getFloorToPut();
            if (floorToPut)
                hoveredTile.gameObject.GetComponent<TileView>().occupyTileWithFloor(floorToPut);
        }
    }

    public void placeWall()
    {
        if (userController.getCurrentEditMode() == UserController.EditModeEnum.wall)
        {
            GameObject wallToPut = userController.getWallToPut();
            if (wallToPut)
                hoveredTile.gameObject.GetComponent<TileView>().occupyTileWithWall(wallToPut, 4, userController.getCurrentPlacingDirection());
        }
    }

    public void putProp()
    {
        if (userController.getCurrentEditMode() == UserController.EditModeEnum.props)
        {
            GameObject propsToPut = userController.getPropsToPut();
            if (propsToPut)
                hoveredTile.gameObject.GetComponent<TileView>().occupyTileWithProp(propsToPut, userController.getCurrentPlacingDirection());
        }
    }

    private void mountGrid()
    {
        for (int tileIndex = 0; tileIndex < gridModel.layout.Count; tileIndex++)
        {
            GameObject newTile = Instantiate(tilePrefab, this.transform);
            newTile.transform.localPosition = gridModel.layout[tileIndex].getPosition();
            tiles.Add(newTile);
        }
        setAdjacentTiles();
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
            tiles[tileIndex].GetComponent<TileView>().setTileActivation(false);
            tiles[tileIndex].GetComponent<TileView>().hoverTile(false);
        }
    }

    public void activateGrid()
    {
        isActive = true;
        for (int tileIndex = 0; tileIndex < tiles.Count; tileIndex++)
        {
            tiles[tileIndex].GetComponent<TileView>().setTileActivation(true);
            tiles[tileIndex].GetComponent<TileView>().hoverTile(false);
        }
    }

    public void setAdjacentTiles()
    {
        for (int tileIndex = 0; tileIndex < tiles.Count; tileIndex++)
        {

            TileView tile = tiles[tileIndex].GetComponent<TileView>();
            List<TileView> tilesSurround = new List<TileView>() {
                getAdjacentTileOrNull(tileIndex, tileIndex - gridSize, gridSize, false),
                getAdjacentTileOrNull(tileIndex, tileIndex -1, gridSize, true),
                getAdjacentTileOrNull(tileIndex, tileIndex +1, gridSize, true),
                getAdjacentTileOrNull(tileIndex, tileIndex + gridSize, gridSize, false),};
            tile.setAdjacentsTiles(tilesSurround);
        }
    }
    public void handleTileControl()
    {
        if (isActive)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Tile" && tiles.Contains(hit.transform.gameObject))
                {
                    if (hit.transform != hoveredTile && hoveredTile != null)
                    {
                        hoveredTile.gameObject.GetComponent<TileView>().hoverTile(false);
                    }
                    hoveredTile = hit.transform;
                    hoveredTile.gameObject.GetComponent<TileView>().hoverTile(true);

                    if (Input.GetMouseButton(0))
                    {
                        putFloor();
                        placeWall();
                        putProp();
                    }

                }
                if (hit.transform.gameObject.tag != "Tile")
                {    
                    if (Input.GetMouseButton(1))
                    {
                        Destroy(hit.transform.gameObject);
                    }
                }
            }
        }
    }

    private TileView getAdjacentTileOrNull(int tileIndex, int adjacentIndex, int size, bool sameLine)
    {
        try
        {
            int adjacentLine = adjacentIndex / size;
            int tileLine = tileIndex / size;
            int adjacentColumn = adjacentIndex % size;
            int tileColumn = tileIndex % size;
            if (sameLine && tileLine != adjacentLine)
            {
                return null;
            }
            if (!sameLine && tileColumn != adjacentColumn)
            {
                return null;
            }
            if(adjacentIndex < 0 || adjacentIndex >= size*size)
            {
                return null;
            }
            return tiles[adjacentIndex].GetComponent<TileView>();
        }
        catch (System.ArgumentOutOfRangeException)
        {
            return null;
        }
    }
}

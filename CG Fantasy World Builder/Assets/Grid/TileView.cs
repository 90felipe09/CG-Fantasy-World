using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileView : MonoBehaviour
{
    [SerializeField] private TileModel tileModel;

    [SerializeField] private Material currentDefaultMaterial;

    [SerializeField] private Material defaultTileMaterial;
    [SerializeField] private Material inactiveTileMaterial;
    [SerializeField] private Material hoveredTileMaterial;

    [SerializeField] private GameObject floorOccupying;
    [SerializeField] private GameObject wallOnTile;
    [SerializeField] private GameObject propOnTile;

    [SerializeField] private List<TileView> adjacentsTiles;

    // Start is called before the first frame update
    public void Start()
    {
        
    }
    public void Update()
    {

    }

    public void occupyTileWithFloor(GameObject floorToPut)
    {
        if (floorOccupying == null)
        {
            floorOccupying = Instantiate(floorToPut, transform);
        }
        else
        {
            Destroy(floorOccupying);
            floorOccupying = Instantiate(floorToPut, transform);
        }
    }

    public void previewTileWithObj(GameObject instantiatedObj, Vector3 initialPos, UserController.Direction dir)
    {
        var wallRotation = getObjRotation(transform, dir);
        instantiatedObj.transform.position = initialPos + transform.position;
        instantiatedObj.transform.rotation = transform.rotation;
        instantiatedObj.transform.RotateAround(transform.position, Vector3.up, wallRotation.eulerAngles.y);
    }

    //TODO: Create wall model with wallSize
    public void occupyTileWithWall(GameObject wallToPut, int wallSize, UserController.Direction dir)
    {
        if (isWallPlacementValid(wallSize, dir))
        {
            var wallRotation = getObjRotation(transform, dir);
            wallOnTile = Instantiate(wallToPut, transform.position + wallToPut.transform.position, wallToPut.transform.rotation);
            wallOnTile.transform.parent = transform;
            wallOnTile.transform.RotateAround(transform.position, Vector3.up, wallRotation.eulerAngles.y);
            TileView nextTile = getNextTile(dir);
            for (int i = 1; i < wallSize; i++)
            {
                nextTile.setWallOnTile(wallOnTile);
                nextTile = nextTile.getNextTile(dir);
            }
        }
    }

    public void occupyTileWithProp(GameObject propToPut, UserController.Direction dir)
    {
        if (wallOnTile == null && propOnTile == null)
        {
            var propRotation = getObjRotation(transform, dir);
            propOnTile = Instantiate(propToPut, transform.position + propToPut.transform.position, propToPut.transform.rotation);
            propOnTile.transform.parent = transform;
            propOnTile.transform.RotateAround(transform.position, Vector3.up, propRotation.eulerAngles.y);

        }
    }

    public bool isWallPlacementValid(int wallSize, UserController.Direction dir)
    {
        return wallOnTile == null && adjacentsAreEmpty(wallSize, dir);
    }

    private Quaternion getObjRotation(Transform baseTransform, UserController.Direction dir)
    {
        switch (dir)
        {
            case UserController.Direction.LEFT:
                return baseTransform.rotation;
            case UserController.Direction.RIGHT:
                return baseTransform.rotation * Quaternion.Euler(0, 180, 0);
            case UserController.Direction.TOP:
                return baseTransform.rotation * Quaternion.Euler(0, 90, 0);
            case UserController.Direction.BOTTOM:
                return baseTransform.rotation * Quaternion.Euler(0, 270, 0);
            default:
                throw new System.Exception("Invalid Direction");
        }
    }


    public void setWallOnTile(GameObject wall)
    {
        wallOnTile = wall;
    }

    public GameObject getWallOnTile()
    {
        return wallOnTile;
    }

    private bool adjacentsAreEmpty(int wallSize, UserController.Direction dir)
    {
        TileView nextTile = this.getNextTile(dir);
        for (int i = 1; i < wallSize; i++)
        {
            if(nextTile == null || nextTile.getWallOnTile() != null)
            {
                return false;
            }
            nextTile = nextTile.getNextTile(dir);
        }
        return true;
    }



    public void setTileModel(TileModel tileModel)
    {
        this.tileModel = tileModel;
    }

    public void setTileActivation(bool newActivation)
    {
        if (newActivation)
        {
            this.currentDefaultMaterial = defaultTileMaterial;
        }
        else
        {
            this.currentDefaultMaterial = inactiveTileMaterial;
        }
    }

    public void hoverTile(bool isHovering)
    {
        if (isHovering)
        {
            GetComponent<Renderer>().material = hoveredTileMaterial;
        }
        else
        {
            GetComponent<Renderer>().material = currentDefaultMaterial;
        }
    }

    public void setAdjacentsTiles(List<TileView> tilesToReference)
    {
        adjacentsTiles = tilesToReference;
    }

    public TileView getNextTile(UserController.Direction dir)
    {
        switch (dir)
        {
            case UserController.Direction.LEFT:
                return getAdjacentTileByIndex(3);
            case UserController.Direction.RIGHT:
                return getAdjacentTileByIndex(0);
            case UserController.Direction.TOP:
                return getAdjacentTileByIndex(2);
            case UserController.Direction.BOTTOM:
                return getAdjacentTileByIndex(1);
            default:
                throw new System.Exception("Invalid Direction");
        }
    }
    private TileView getAdjacentTileByIndex(int index)
    {
        return adjacentsTiles[index];
    }


}

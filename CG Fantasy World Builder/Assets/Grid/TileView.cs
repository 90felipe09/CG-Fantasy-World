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

    public void occupyTileWithWall(GameObject wallToPut, int wallSize, UserController.Direction dir)
    {
        if (wallOnTile == null && adjacentsAreEmpty(wallSize, dir))
        {
            var (wallPosition, wallRotation) = getWallPosRot(transform, dir);
            wallOnTile = Instantiate(wallToPut, wallPosition, wallRotation);
            wallOnTile.transform.parent = transform;
            TileView nextTile = this.getNextTile(dir);
            for (int i = 1; i < wallSize; i++)
            {
                nextTile.setWallOnTile(wallOnTile);
                nextTile = nextTile.getNextTile(dir);
            }
        }
    }


    public (Vector3 pos, Quaternion rot) getWallPosRot(Transform baseTransform, UserController.Direction dir)
    {
        switch (dir)
        {
            case UserController.Direction.LEFT:
                return (baseTransform.position + new Vector3(0, 2, 1.5f), baseTransform.rotation);
            case UserController.Direction.RIGHT:
                return (baseTransform.position + new Vector3(0, 2, -1.5f), baseTransform.rotation * Quaternion.Euler(0, 180, 0));
            case UserController.Direction.TOP:
                return (baseTransform.position + new Vector3(1.5f, 2, 0), baseTransform.rotation * Quaternion.Euler(0, 90, 0));
            case UserController.Direction.BOTTOM:
                return (baseTransform.position + new Vector3(-1.5f, 2, 0), baseTransform.rotation * Quaternion.Euler(0, 270, 0));
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

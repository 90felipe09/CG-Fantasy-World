using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
    [SerializeField] private GameObject floorToPut;
    [SerializeField] private GameObject wallToPut;
    [SerializeField] private GameObject propsToPut;
    [SerializeField] private EditModeEnum currentEditMode;
    [SerializeField] private Direction currentPlacingDirection = Direction.LEFT;
    [SerializeField] private GameObject wallPreview;
    [SerializeField] private Material currentPlacingMaterial;
    [SerializeField] private Material previewSucessMaterial;
    [SerializeField] private Material previewFailMaterial;

    //TODO: will fix this by adding the wall model
    private int WALLSIZE = 4;


    public enum EditModeEnum {position, floor, wall, props};

    public enum Direction
    {
        LEFT, RIGHT, TOP, BOTTOM
    }

    public void setCurrentEditMode (EditModeEnum newMode) 
    {
        currentEditMode = newMode;
        destroyWallPreview();
    }

    public void setFloorToPut(GameObject floorToPut)
    {
        this.floorToPut = floorToPut;
    }

    public void setWallToPut(GameObject wallToPut)
    {
        this.wallToPut = wallToPut;
        instantiateWallPreview();
    }

    public void setPropsToPut(GameObject propsToPut)
    {
        this.propsToPut = propsToPut;
    }

    public EditModeEnum getCurrentEditMode()
    {
        return currentEditMode;
    }

    public GameObject getFloorToPut()
    {
        return floorToPut;
    }

    public GameObject getWallToPut()
    {
        return wallToPut;
    }

    public GameObject getPropsToPut()
    {
        return propsToPut;
    }

    public void putFloor(TileView hoveredTile)
    {
        if (getCurrentEditMode() == EditModeEnum.floor)
        {
            GameObject floorToPut = getFloorToPut();
            if (floorToPut)
                hoveredTile.occupyTileWithFloor(floorToPut);
        }
    }

    public void hoverWall(TileView hoveredTile)
    {
        if (getCurrentEditMode() == EditModeEnum.wall)
        {
            if (wallPreview)
                if (hoveredTile.isWallPlacementValid(WALLSIZE, getCurrentPlacingDirection()))
                {
                    setPlacingPreviewSucess();
                }
                else
                {
                    setPlacingPreviewFail();
                }
            hoveredTile.previewTileWithWall(wallPreview, wallToPut.transform.position, getCurrentPlacingDirection());
        }
    }

    public void placeWall(TileView hoveredTile)
    {
        if (getCurrentEditMode() == EditModeEnum.wall)
        {
            GameObject wallToPut = getWallToPut();
            if (wallToPut)
                hoveredTile.occupyTileWithWall(wallToPut, WALLSIZE, getCurrentPlacingDirection());
        }
    }

    public void putProp(TileView hoveredTile)
    {
        if (getCurrentEditMode() == EditModeEnum.props)
        {
            GameObject propsToPut = getPropsToPut();
            if (propsToPut)
                hoveredTile.occupyTileWithProp(propsToPut, getCurrentPlacingDirection());
        }
    }

    public void increasePlacingDirection()
    {
        switch (currentPlacingDirection)
        {
            case Direction.LEFT:
                setCurrentPlacingDirection(Direction.TOP);
                break;
            case Direction.RIGHT:
                setCurrentPlacingDirection(Direction.BOTTOM);
                break;
            case Direction.TOP:
                setCurrentPlacingDirection(Direction.RIGHT);
                break;
            case Direction.BOTTOM:
                setCurrentPlacingDirection(Direction.LEFT);
                break;
            default:
                break;
        }
    }

    public void decreasePlacingDirection()
    {
        switch (currentPlacingDirection)
        {
            case Direction.LEFT:
                setCurrentPlacingDirection(Direction.BOTTOM);
                break;
            case Direction.RIGHT:
                setCurrentPlacingDirection(Direction.TOP);
                break;
            case Direction.TOP:
                setCurrentPlacingDirection(Direction.LEFT);
                break;
            case Direction.BOTTOM:
                setCurrentPlacingDirection(Direction.RIGHT);
                break;
            default:
                break;
        }
    }

    private void setCurrentPlacingDirection(Direction newDirection)
    {
        currentPlacingDirection = newDirection;
    }

    public Direction getCurrentPlacingDirection()
    {
        return currentPlacingDirection;
    }

    public void setPlacingPreviewFail()
    {
        currentPlacingMaterial = previewFailMaterial;
        wallPreview.GetComponent<Renderer>().material = currentPlacingMaterial;

    }

    public void setPlacingPreviewSucess()
    {
        currentPlacingMaterial = previewSucessMaterial;
        wallPreview.GetComponent<Renderer>().material = currentPlacingMaterial;

    }

    public void instantiateWallPreview()
    {
        currentPlacingMaterial = previewSucessMaterial;
        wallPreview = Instantiate(getWallToPut());
        wallPreview.GetComponent<BoxCollider>().enabled = false;
        wallPreview.GetComponent<Renderer>().material = currentPlacingMaterial;
    }

    public void destroyWallPreview()
    {
        Destroy(wallPreview);
    }
}

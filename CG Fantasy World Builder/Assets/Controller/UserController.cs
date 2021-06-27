using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
    [SerializeField] private GameObject floorToPut;
    [SerializeField] private GameObject objToPut;
    [SerializeField] private EditModeEnum currentEditMode;
    [SerializeField] private Direction currentPlacingDirection = Direction.LEFT;
    [SerializeField] private GameObject baseObjectToPut;
    [SerializeField] private GameObject previewObject;
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
        destroyPreviews();
    }

    public void setFloorToPut(GameObject floorToPut)
    {
        this.floorToPut = floorToPut;
    }

    public void setWallToPut(GameObject wallToPut)
    {
        this.objToPut = wallToPut;
        instantiatePreviewObj(wallToPut);
    }

    public void setPropsToPut(GameObject propsToPut)
    {
        this.objToPut = propsToPut;
        instantiatePreviewObj(propsToPut);
    }

    public EditModeEnum getCurrentEditMode()
    {
        return currentEditMode;
    }

    public GameObject getFloorToPut()
    {
        return floorToPut;
    }

    public GameObject getObjToPut()
    {
        return objToPut;
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

    private void setPlacementValidity(TileView hoveredTile)
    {
        if (getCurrentEditMode() == EditModeEnum.wall)
        {
            if (hoveredTile.isWallPlacementValid(WALLSIZE, getCurrentPlacingDirection()))
            {
                setPlacingPreviewSucess();
            }
            else
            {
                setPlacingPreviewFail();
            }            
        }
    }

    public void placeObj(TileView hoveredTile)
    {
        GameObject objToPut = getObjToPut();
        if (getCurrentEditMode() == EditModeEnum.wall)
        {
            hoveredTile.occupyTileWithWall(objToPut, WALLSIZE, getCurrentPlacingDirection());
        }

        if (getCurrentEditMode() == EditModeEnum.props)
        {
            hoveredTile.occupyTileWithProp(objToPut, getCurrentPlacingDirection());
        }
    }


    public void hoverPreview(TileView hoveredTile)
    {
        if (previewObject)
        {
            setPlacementValidity(hoveredTile);
            hoveredTile.previewTileWithObj(previewObject, objToPut.transform.position, getCurrentPlacingDirection());
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

    private void setPlacingPreviewFail()
    {
        setAllObjMaterials(previewObject, previewFailMaterial);

    }

    private void setPlacingPreviewSucess()
    {
        setAllObjMaterials(previewObject, previewSucessMaterial);
    }

    private void instantiatePreviewObj(GameObject objToPut)
    {
        previewObject = Instantiate(objToPut);
        setPreviewDefaults(previewObject);
    }

    private void setPreviewDefaults(GameObject previewObject)
    {
        previewObject.GetComponent<BoxCollider>().enabled = false;
        setPlacingPreviewSucess();
    }

    private void setAllObjMaterials(GameObject obj, Material material)
    {
        int materialsLength = obj.GetComponent<Renderer>().materials.Length;
        Material[] newMaterials = new Material[materialsLength];
        for (int i = 0; i < materialsLength; i++)
        {
            newMaterials[i] = material;
        }
        obj.GetComponent<Renderer>().materials = newMaterials;
    }

    public void destroyPreviews()
    {
        Destroy(previewObject);
    }
}

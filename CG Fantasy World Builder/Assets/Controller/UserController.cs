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

    public enum EditModeEnum {position, floor, wall, props};

    public enum Direction
    {
        LEFT, RIGHT, TOP, BOTTOM
    }

    public void setCurrentEditMode (EditModeEnum newMode) 
    {
        currentEditMode = newMode;
    }

    public void setFloorToPut(GameObject floorToPut)
    {
        this.floorToPut = floorToPut;
    }

    public void setWallToPut(GameObject wallToPut)
    {
        this.wallToPut = wallToPut;
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
        this.currentPlacingDirection = newDirection;
    }

    public Direction getCurrentPlacingDirection()
    {
        return currentPlacingDirection;
    }
}

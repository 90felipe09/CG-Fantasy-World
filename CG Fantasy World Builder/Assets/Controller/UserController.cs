using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
    [SerializeField] private GameObject floorToPut;
    [SerializeField] private GameObject wallToPut;
    [SerializeField] private GameObject propsToPut;
    [SerializeField] private EditModeEnum currentEditMode;

    public enum EditModeEnum {position, floor, wall, props};

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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

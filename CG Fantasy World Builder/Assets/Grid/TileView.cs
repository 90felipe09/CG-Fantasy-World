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

    // Start is called before the first frame update
    public void Start()
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

    // Update is called once per frame
    public void Update()
    {
        
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
}

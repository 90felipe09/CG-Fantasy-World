using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileView : MonoBehaviour
{
    [SerializeField] private TileModel tileModel;

    [SerializeField] private Material defaultTileMaterial;
    [SerializeField] private Material hoveredTileMaterial;

    // Start is called before the first frame update
    public void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void setTileModel(TileModel tileModel)
    {
        this.tileModel = tileModel;
    }

    public void setDefaultTileMaterial(Material newDefaultMaterial)
    {
        this.defaultTileMaterial = newDefaultMaterial;
    }

    public void handleMouseHover(bool hoveringIsEnabled)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform && hoveringIsEnabled)
            {
                GetComponent<Renderer>().material = hoveredTileMaterial;
            }
            else
            {
                GetComponent<Renderer>().material = defaultTileMaterial;
            }
        }
        else
        {
            GetComponent<Renderer>().material = defaultTileMaterial;
        }
    }

}

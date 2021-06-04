using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private int floorShowing = 0;
    [SerializeField] private GameObject gridPrefab;
    [SerializeField] private List<GameObject> gridFloors;
    [SerializeField] private int floorsAmount = 3;

    [SerializeField] private float floorHeight = 3.0f;

    private void Start()
    {
        instantiateGrids();
        showGrid(0);
    }

    private void Update()
    {
        handleFloorNavigation();
    }

    private void instantiateGrids()
    {
        for (int floorIndex = 0; floorIndex < floorsAmount; floorIndex++)
        {
            GameObject grid = Instantiate(gridPrefab, transform);
            grid.transform.position += Vector3.up * floorHeight * floorIndex;
            grid.GetComponent<GridView>().setGridFloor(floorIndex);
            grid.SetActive(false);
            gridFloors.Add(grid);
        }
    }

    public void showGrid(int floorToShow)
    {
        for (int floorIndex = 0; floorIndex < floorsAmount; floorIndex++)
        {
            if (floorIndex > floorToShow)
            {
                gridFloors[floorIndex].SetActive(false);
                gridFloors[floorIndex].GetComponent<GridView>().deactivateGrid();
            }
            else
            {
                gridFloors[floorIndex].SetActive(true);
                gridFloors[floorIndex].GetComponent<GridView>().deactivateGrid();
            }
        }

        gridFloors[floorToShow].GetComponent<GridView>().activateGrid();

        floorShowing = floorToShow;
    }

    private void handleFloorNavigation()
    {
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            if (floorShowing > 0)
            {
                showGrid(floorShowing - 1);
            }
        }

        if (Input.GetKeyDown(KeyCode.Equals))
        {
            if (floorShowing < floorsAmount - 1)
            {
                showGrid(floorShowing + 1);
            }
        }
    }
}

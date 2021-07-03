using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDCategoryView : MonoBehaviour
{
    [SerializeField] private List<GameObject> options;
    [SerializeField] private List<GameObject> palletes;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private RectTransform hudPallete;

    [SerializeField] private UserController userController;

    private enum PalletesOptionsEnums { Floors = 0, Walls = 1, Props = 2 };
    private int numberOfPalletes = 3;

    private float hudWidthPerOption = 100;

    void Start()
    {
        hudPallete = GetComponent<RectTransform>();

        for (int i = 0; i < numberOfPalletes; i++)
        {
            addOption(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        hudPallete.sizeDelta = new Vector2(hudWidthPerOption * options.Count, hudPallete.rect.height);
    }

    private string getButtonName(int index)
    {
        switch (index)
        {
            case (int) PalletesOptionsEnums.Floors:
                return "Chão";
            case (int) PalletesOptionsEnums.Walls:
                return "Estrutura";
            case (int) PalletesOptionsEnums.Props:
                return "Props";
            default:
                return "";
        }
    }

    private void selectFloorsPallete()
    {
        palletes[(int)PalletesOptionsEnums.Floors].SetActive(true);
        userController.setCurrentEditMode(UserController.EditModeEnum.floor);
        gameObject.SetActive(false);
    }

    private void selectWallsPallete()
    {
        palletes[(int)PalletesOptionsEnums.Walls].SetActive(true);
        userController.setCurrentEditMode(UserController.EditModeEnum.wall);
        gameObject.SetActive(false);
    }

    private void selectPropsPallete()
    {
        palletes[(int)PalletesOptionsEnums.Props].SetActive(true);
        userController.setCurrentEditMode(UserController.EditModeEnum.props);
        gameObject.SetActive(false);
    }

    private void addOption(int optionToAdd)
    {
        GameObject newButton = Instantiate(buttonPrefab, transform);
        newButton.transform.GetChild(0).GetComponent<Text>().text = getButtonName(optionToAdd);
        Button buttonReference = newButton.GetComponent<Button>();

        switch (optionToAdd)
        {
            case 0:
                buttonReference.onClick.AddListener(selectFloorsPallete);
                break;
            case 1:
                buttonReference.onClick.AddListener(selectWallsPallete);
                break;
            case 2:
                buttonReference.onClick.AddListener(selectPropsPallete);
                break;
        }
        
        newButton.GetComponent<RectTransform>().position += new Vector3(100, 0) * options.Count;
        options.Add(newButton);
    }
}

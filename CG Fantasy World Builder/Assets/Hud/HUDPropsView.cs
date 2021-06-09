using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDPropsView : MonoBehaviour
{
    [SerializeField] private List<GameObject> options;
    [SerializeField] private List<GameObject> propsPrefabsOptions;

    [SerializeField] private GameObject categoryHud;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private RectTransform hudPallete;

    [SerializeField] private UserController userController;

    private float hudWidthPerOption = 100;
    // Start is called before the first frame update
    void Start()
    {
        hudPallete = GetComponent<RectTransform>();

        for (int i = 0; i < propsPrefabsOptions.Count; i++)
        {
            addOption(propsPrefabsOptions[i]);
            hudPallete.sizeDelta = new Vector2(hudWidthPerOption * options.Count, hudPallete.rect.height);
        }

        addReturnToCategoriesOption();
    }

    private void selectPropsToPut(GameObject propsOption)
    {
        GameObject wallToPut = Instantiate(propsOption);
        userController.setWallToPut(wallToPut);
    }

    private void addOption(GameObject optionToAdd)
    {
        GameObject newButton = Instantiate(buttonPrefab, transform);
        newButton.transform.GetChild(0).GetComponent<Text>().text = optionToAdd.name;
        Button buttonReference = newButton.GetComponent<Button>();

        buttonReference.onClick.AddListener(() => selectPropsToPut(optionToAdd));

        newButton.GetComponent<RectTransform>().position += new Vector3(100, 0) * options.Count;
        options.Add(newButton);
    }

    private void returnToCategory()
    {
        categoryHud.SetActive(true);
        userController.setCurrentEditMode(UserController.EditModeEnum.position);
        gameObject.SetActive(false);
    }

    private void addReturnToCategoriesOption()
    {
        GameObject newButton = Instantiate(buttonPrefab, transform);
        newButton.transform.GetChild(0).GetComponent<Text>().text = "Back";
        Button buttonReference = newButton.GetComponent<Button>();

        buttonReference.onClick.AddListener(returnToCategory);

        newButton.GetComponent<RectTransform>().position += new Vector3(100, 0) * options.Count;
        options.Add(newButton);
    }
}

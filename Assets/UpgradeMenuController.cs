using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class UpgradeMenuController : MonoBehaviour {
    public static UpgradeMenuController instance;
    public List<GameObject> moduleSelectButtons;
    private int[] numModules;
    public GameObject shipGrid;
    public GameObject InventoryTextContainer;
    public GameObject buttonPrefab;
    public GameObject InventoryTextPrefab;
    public EventSystem eventSystem;
    public Sprite[] moduleSprites;

    private SpawnStartingShip shipData;
    private int[,] shipSample;
    private GameObject[,] buttonGrid;
    private GameObject[] InventoryTextObjects;
    private int selectedModule;
    private Inventory inventory;
  
    // Use this for initialization
    void Awake ()
    {
        shipData = GameObject.Find("BuildController").GetComponent<SpawnStartingShip>();
        buttonGrid = new GameObject[5, 5];
        inventory = GameObject.Find("GameController").GetComponent<Inventory>();
        InventoryTextObjects = new GameObject[(int)GameController.ItemTypes.NUMBEROFTYPES];
        for (int j = 0; j < 5; j++)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject newObj = Instantiate(buttonPrefab);
                newObj.transform.SetParent(shipGrid.transform, false);
                int newI = i; //Thank you compiler
                int newJ = j;
                newObj.GetComponent<Button>().onClick.AddListener(() => updateShip(newI, newJ));
                buttonGrid[i, j] = newObj;
            }
        }
        for (int i = 0; i < InventoryTextObjects.Length; i++)
        {
            GameObject newObj = Instantiate(InventoryTextPrefab);
            newObj.transform.SetParent(InventoryTextContainer.transform, false);
            InventoryTextObjects[i] = newObj;
        }
        shipGrid.SetActive(false);
    }
	
	public void ActivateUpgrade () {

        numModules = inventory.GetInventory();
        shipSample = shipData.GetShipLayout();
        InventoryTextUpdate();

        for (int j = 0; j < 5; j++)
        {
            for (int i = 0; i < 5; i++)
            {
                int x = shipSample[i, j];
                if (shipData.ValidPlacement(i, j) && shipData.ValidRemoval(i,j))
                {
                    buttonGrid[i, j].GetComponent<Button>().interactable = true;
                }
                else
                {
                    buttonGrid[i, j].GetComponent<Button>().interactable = false;
                }
                //buttonGrid[i, j].GetComponentInChildren<Text>().text = "" + x;
                if (x > -1)
                {
                    Sprite s = moduleSprites[x];
                    buttonGrid[i, j].GetComponentsInChildren<Image>()[1].sprite = s;
                }
                else
                    buttonGrid[i, j].GetComponentsInChildren<Image>()[1].enabled = false;
            }
        }
        shipGrid.SetActive(false);
        eventSystem.SetSelectedGameObject(moduleSelectButtons[0]);
    }
    void InventoryTextUpdate()
    {
        for (int i = 0; i < InventoryTextObjects.Length; i++)
        {
            InventoryTextObjects[i].GetComponent<Text>().text = "" + /*numModules[i]*/;
        }
    }
    void disableUnreachable()
    {
        for (int j = 0; j < 5; j++)
        {
            for (int i = 0; i < 5; i++)
            {
                if (shipData.ValidPlacement(i, j) && shipData.ValidRemoval(i,j))
                {
                    buttonGrid[i, j].GetComponent<Button>().interactable = true;
                }
                else
                {
                    buttonGrid[i, j].GetComponent<Button>().interactable = false;
                }
            }
        }
    }
    void updateShip(int x, int y)
    {
        shipSample[x, y] = selectedModule;
        inventory.RemoveItem(selectedModule);
        InventoryTextUpdate();
        shipData.SetShipLayout(shipSample);
        disableUnreachable();
        //buttonGrid[x, y].GetComponentInChildren<Text>().text = "" + selectedModule;
        if (selectedModule > -1)
        {
            buttonGrid[x, y].GetComponentsInChildren<Image>()[1].enabled = true;
            buttonGrid[x, y].GetComponentsInChildren<Image>()[1].sprite = moduleSprites[selectedModule];
        }
        else
            buttonGrid[x, y].GetComponentInChildren<Image>().enabled = false;
        shipGrid.SetActive(false);
        foreach (GameObject g in moduleSelectButtons)
        {
            g.SetActive(true);
        }
        eventSystem.SetSelectedGameObject(moduleSelectButtons[0]);
    }
    public void addModule(int modType)
    {
        selectedModule = modType;
        foreach (GameObject g in moduleSelectButtons)
        {
            if (g.GetComponent<Image>() != null)
            {
                g.GetComponent<Image>().color = Color.white;
                g.transform.localScale = Vector3.one;
            }
            g.SetActive(false);
        }
        shipGrid.SetActive(true);
        eventSystem.SetSelectedGameObject(buttonGrid[0,0]);
    }

    public void commitUpgrade()
    {
        GameController.instance.DisableUpgradeMenu();
    }
}

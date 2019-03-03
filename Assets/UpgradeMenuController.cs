using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class UpgradeMenuController : MonoBehaviour {
    public static UpgradeMenuController instance;
    public List<GameObject> moduleSelectButtons;
    public GameObject shipGrid;
    public GameObject buttonPrefab;
    public EventSystem eventSystem;
    public Sprite[] moduleSprites;

    private SpawnStartingShip shipData;
    private int[,] shipSample;
    private GameObject[,] buttonGrid;
    private int selectedModule;
  
    // Use this for initialization
    void Awake ()
    {
        shipData = GameObject.Find("BuildController").GetComponent<SpawnStartingShip>();
        buttonGrid = new GameObject[5, 5];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject newObj = Instantiate(buttonPrefab);
                newObj.transform.SetParent(shipGrid.transform, false);
                int newI = i; //Thank you compiler
                int newJ = j;
                newObj.GetComponent<Button>().onClick.AddListener(() => updateShip(newI, newJ));
                buttonGrid[i, j] = newObj;
            }
        }
        shipGrid.SetActive(false);
    }
	
	// Update is called once per frame
	public void ActivateUpgrade () {

        shipSample = shipData.GetShipLayout();
        for (int j = 0; j < 5; j++)
        {
            for (int i = 0; i < 5; i++)
            {
                int x = shipSample[i, j];
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

    void updateShip(int x, int y)
    {
        shipSample[x, y] = selectedModule;
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
        shipData.SetShipLayout(shipSample);
    }
}

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
    private int[,] shipBlueprint;
    private int[,] shipSample;
    private GameObject[,] buttonGrid;
    private int selectedModule;
  
    // Use this for initialization
    void Start () {
        shipSample = new int[5, 5];
        buttonGrid = new GameObject[5, 5];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                shipSample[i, j] = -1; //FOR DEBUGGING ONLY
                GameObject newObj = Instantiate(buttonPrefab);
                newObj.transform.SetParent(shipGrid.transform, false);
                int newI = i; //Thank you compiler
                int newJ = j;
                newObj.GetComponent<Button>().onClick.AddListener(() => updateShip(newI, newJ));
                int x = shipSample[i, j];
                newObj.GetComponentInChildren<Text>().text = "" + x;
                if (x > -1)
                    newObj.GetComponentsInChildren<Image>()[1].sprite = moduleSprites[shipSample[i, j]];
                else
                    newObj.GetComponentsInChildren<Image>()[1].enabled = false;
                buttonGrid[i, j] = newObj;
            }
        }
        shipGrid.SetActive(false);
        //ship = GameObject.Find("BuildController").GetComponent<SpawnStartingShip>();
        //shipBlueprint = shipData.getLayout();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void updateShip(int x, int y)
    {
        Debug.Log("X: " + x + " Y: " + y);
        shipSample[x, y] = selectedModule;
        buttonGrid[x, y].GetComponentInChildren<Text>().text = "" + selectedModule;
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
        //shipBlueprint = shipData.setLayout(shipBlueprint);
    }
}

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
    public GameObject ImageContainer;
    public GameObject buttonPrefab;
    public GameObject InventoryTextPrefab;
    public EventSystem eventSystem;
    public Sprite[] moduleSprites;
    private AudioSource audio;

    private SpawnStartingShip shipData;
    private int[,] shipSample;
    private GameObject[,] buttonGrid;
    private GameObject[] InventoryTextObjects;
    private int selectedModule;
    private Inventory inventory;
    private AudioSource pressSource;
    public AudioClip press;
  
    // Use this for initialization
    void Awake ()
    {
        audio = GetComponentInChildren<AudioSource>(); //Audio source must be in child
        shipData = GameObject.Find("BuildController").GetComponent<SpawnStartingShip>();
        buttonGrid = new GameObject[5, 5];
        inventory = GameObject.Find("GameController").GetComponent<Inventory>();
        InventoryTextObjects = new GameObject[moduleSelectButtons.Count];
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
        for (int i = 0; i < moduleSelectButtons.Count - 2; i++)
        {
            int newI = i;
            moduleSelectButtons[i].GetComponent<Button>().onClick.AddListener(() => addModule(newI));
            moduleSelectButtons[i].GetComponent<Button>().onClick.AddListener(() => PressSound());
        }
        moduleSelectButtons[moduleSelectButtons.Count - 2].GetComponent<Button>().onClick.AddListener(() => addModule(-1));
        moduleSelectButtons[moduleSelectButtons.Count - 2].GetComponent<Button>().onClick.AddListener(() => PressSound());
        moduleSelectButtons[moduleSelectButtons.Count - 1].GetComponent<Button>().onClick.AddListener(() => commitUpgrade());
        moduleSelectButtons[moduleSelectButtons.Count - 1].GetComponent<Button>().onClick.AddListener(() => PressSound());

        shipGrid.SetActive(false);
        pressSource = AddAudio(press, 1.0f);
    }
	
    private AudioSource AddAudio(AudioClip clip, float vol)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.loop = false;
        newAudio.playOnAwake = false;
        newAudio.volume = vol;
        return newAudio;
    }
    
    public void PressSound()
    {
        pressSource.PlayOneShot(press);
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
        for (int i = 0; i < InventoryTextObjects.Length - 2; i++)
        {
            InventoryTextObjects[i].GetComponent<Text>().text = "" + numModules[i];
            //moduleSelectButtons[i].GetComponent<Button>().interactable = numModules[i] > 0;
        }
    }
    void disableUnreachable()
    {
        for (int j = 0; j < 5; j++)
        {
            for (int i = 0; i < 5; i++)
            {
                if (shipData.ValidPlacement(i, j) && shipData.ValidRemoval(i, j))
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
        int removedModule = shipSample[x, y];
        shipSample[x, y] = selectedModule;
        if (selectedModule > -1)
        {
            inventory.RemoveItem(selectedModule);
        }
        if (removedModule > -1)
        {
            inventory.AddItem(removedModule);
        }
        InventoryTextUpdate();
        shipData.SetShipLayout(shipSample);
        disableUnreachable();
        //Update ship grid sprites before we leave
        if (selectedModule > -1)
        {
            buttonGrid[x, y].GetComponentsInChildren<Image>()[1].enabled = true;
            buttonGrid[x, y].GetComponentsInChildren<Image>()[1].sprite = moduleSprites[selectedModule];
        }
        else
        {
            buttonGrid[x, y].GetComponentsInChildren<Image>()[1].enabled = false;
        }
        shipGrid.SetActive(false);
        foreach (GameObject g in moduleSelectButtons)
        {
            g.SetActive(true);
        }
        InventoryTextContainer.SetActive(true);
        ImageContainer.SetActive(true);
        eventSystem.SetSelectedGameObject(moduleSelectButtons[0]);
    }
    public void addModule(int modType)
    {
        selectedModule = modType;
        audio.Play();
        //Remove artifacts of animation
        foreach (GameObject g in moduleSelectButtons)
        {
            if (g.GetComponent<Image>() != null)
            {
                g.GetComponent<Image>().color = Color.white;
                g.transform.localScale = Vector3.one;
            }
            g.SetActive(false);
        }
        //Hide inventory numbers and images linked to the module select buttons
        InventoryTextContainer.SetActive(false);
        ImageContainer.SetActive(false);
        
        shipGrid.SetActive(true);
        eventSystem.SetSelectedGameObject(buttonGrid[2,2]);
    }
    
    public void commitUpgrade()
    {
        //if (shipData.ValidShip())
        GameController.instance.DisableUpgradeMenu();
        GameObject.Find("Player 1").GetComponent<CameraHolder>().playerCamera.SetActive(true);
		GameObject.Find("Player 2").GetComponent<CameraHolder>().playerCamera.SetActive(true);
        GameObject.Find("Ship").GetComponent<ShipInfoDavin>().unfreezePlayer(GameObject.Find("Players").transform.GetChild(0).gameObject);
        GameObject.Find("Ship").GetComponent<ShipInfoDavin>().unfreezePlayer(GameObject.Find("Players").transform.GetChild(1).gameObject);
    }
}

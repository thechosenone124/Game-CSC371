using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStartingShip : MonoBehaviour {

	// Use this for initialization
	public GameObject[] modules;
	private GameObject[,] rooms;
	private GameObject engineRoom;
	private GameObject cockpit;
	private GameObject weaponsRoom;
	private GameObject gun1,gun2;
	private int[,] shipLayout;

    private const uint NOTONE = 4294967294;
    private const uint NOTTWO = 4294967293;
    private const uint NOTFOUR = 4294967291;
    private const uint NOTEIGHT = 4294967287;

	//private int roomSwitch = 0;
	void Start () {
		int childNum = 0;
		rooms = new GameObject[5,5];
		shipLayout = new int[5,5];
		for(int i = 0; i < 5; i++){
			for(int j = 0; j < 5; j++){
				transform.GetChild(childNum).gameObject.GetComponent<CreateRoom>().InitializeRoom(childNum,j,i);
				childNum++;
				shipLayout[j,i] = -1;
			}
		}
		SpawnStartShip();
    }
	void SpawnStartShip(){
		shipLayout[2,4] = (int)GameController.ItemTypes.ENGINEROOM;
		shipLayout[2,3] = (int)GameController.ItemTypes.WEAPONSROOM;
		shipLayout[3,3] = (int)GameController.ItemTypes.NOAHGUN;
		shipLayout[2,2] = (int)GameController.ItemTypes.COCKPIT;
		BuildShip();
	}
	public void SpawnModuleAtLocation(int x, int y, int moduleType){
		int shipSize = rooms.GetLength(0);
		GameObject shipPart = transform.GetChild(y*shipSize + x).gameObject;
		shipPart.GetComponent<CreateRoom>().BuildRoom(modules[moduleType],HasNeighbors(x,y),moduleType);
		rooms[x,y] = shipPart;
		UpdateNeighbors(x,y);
	}

	public void RemoveModuleAtLocation(int x, int y){
		int shipSize = rooms.GetLength(0);
		GameObject shipPart = transform.GetChild(y*shipSize + x).gameObject;
		shipPart.GetComponent<CreateRoom>().RemoveRoom();
		rooms[x,y] = null;
		UpdateNeighbors(x,y);
	}
	void SpawnModule(int child, GameObject module, int x, int y, int moduleType){
		int neighbors = HasNeighbors(x,y);
		if(HasNeighbors(x,y) > 0){
			rooms[x,y] = transform.GetChild(child).gameObject;
			rooms[x,y].GetComponent<CreateRoom>().BuildRoom(module,neighbors,moduleType);
			UpdateNeighbors(x,y);
		}
	}

	public void RoomClicked(int x, int y, int childNum){
		gun2 = transform.GetChild(18).gameObject;
		gun2.GetComponent<CreateRoom>().BuildRoom(modules[(int)GameController.ItemTypes.NOAHGUN],HasNeighbors(3,3),(int)GameController.ItemTypes.NOAHGUN);
		rooms[3,3] = gun2;
		UpdateNeighbors(3,3);
	}
	bool CheckDown(int x, int y){
		if(y == rooms.GetLength(1) - 1){
			return false;
		}
		else if(rooms[x,y+1] == null){
			return false;
		}
		else if(rooms[x,y+1].GetComponent<CreateRoom>().GetModuleType() == (int)GameController.ItemTypes.COCKPIT){
			return false;
		}
		else if(rooms[x,y+1].GetComponent<CreateRoom>().GetModuleType() == (int)GameController.ItemTypes.GUN){
			return false;
		}
		else if(rooms[x,y+1].GetComponent<CreateRoom>().GetModuleType() == (int)GameController.ItemTypes.NOAHGUN){
			return false;
		}
		else{
			return true;
		}
	}
	bool CheckUp(int x, int y){
		if(y == 0){
			return false;
		}
		else if(rooms[x,y-1] == null){
			return false;
		}
		else if(rooms[x,y-1].GetComponent<CreateRoom>().GetModuleType() == (int)GameController.ItemTypes.GUN){
			return false;
		}
		else if(rooms[x,y-1].GetComponent<CreateRoom>().GetModuleType() == (int)GameController.ItemTypes.NOAHGUN){
			return false;
		}
		else if(rooms[x,y-1].GetComponent<CreateRoom>().GetModuleType() == (int)GameController.ItemTypes.ENGINEROOM){
			return false;
		}
		else{
			return true;
		}
	}
	bool CheckLeft(int x, int y){
		if(x == 0){
			return false;
		}
		if(rooms[x-1,y] == null){
			return false;
		}
		else if(rooms[x-1,y].GetComponent<CreateRoom>().GetModuleType() == (int)GameController.ItemTypes.COCKPIT){
			return false;
		}
		else if(rooms[x-1,y].GetComponent<CreateRoom>().GetModuleType() == (int)GameController.ItemTypes.GUN){
			return false;
		}
		else if(rooms[x-1,y].GetComponent<CreateRoom>().GetModuleType() == (int)GameController.ItemTypes.NOAHGUN){
			return false;
		}
		else{
			return true;
		}
	}
	bool CheckRight(int x, int y){
		if(x == rooms.GetLength(0) - 1){
			return false;
		}
		if(rooms[x+1,y] == null){
			return false;
		}
		else if(rooms[x+1,y].GetComponent<CreateRoom>().GetModuleType() == (int)GameController.ItemTypes.COCKPIT){
			return false;
		}
		else if(rooms[x+1,y].GetComponent<CreateRoom>().GetModuleType() == (int)GameController.ItemTypes.GUN){
			return false;
		}
		else if(rooms[x+1,y].GetComponent<CreateRoom>().GetModuleType() == (int)GameController.ItemTypes.NOAHGUN){
			return false;
		}
		else{
			return true;
		}
	}

	int HasNeighbors(int x, int y){
		int neighbors = 0;
		if(CheckUp(x,y)){
			neighbors += 1;
		}
		if(CheckDown(x,y)){
			neighbors += 2;
		}
		if(CheckLeft(x,y)){
			neighbors += 4;
		}
		if(CheckRight(x,y)){
			neighbors += 8;
		}
		return neighbors;
	}

	public bool ValidPlacement(int x, int y){
		if(HasNeighbors(x,y) > 0){
			return true;
		}
		else{
			return false;
		}
	}

    /*The code below determines if a locatio is a valid location to remove.
    It does this by first checking that it is not an empty space, because those are fine to remove.
    It check to see if any of its neighbors would be left without a valid neighbor if they were to disappear.
    And it check to make sure that it is the only neighbor of a the rooms around it. */
	public bool ValidRemoval(int x, int y){
        if((y+1) != rooms.GetLength(1) && (HasNeighbors(x,y+1) & NOTONE) == 0 && shipLayout[x,y] != -1 && HasNeighbors(x,y+1) != 0 && shipLayout[x,y+1] != -1){
            return false;
        }
		else if((y-1) != -1 && (HasNeighbors(x,y-1) & NOTTWO) == 0 && shipLayout[x,y] != -1 && HasNeighbors(x,y-1) != 0 && shipLayout[x,y-1] != -1) {
			return false;
		}
        else if((x+1) != rooms.GetLength(0) && (HasNeighbors(x+1,y) & NOTFOUR) == 0 && shipLayout[x,y] != -1 && HasNeighbors(x+1,y) != 0 && shipLayout[x+1,y] != -1){
            return false;
        }
		else if((x-1) != -1 && (HasNeighbors(x-1,y) & NOTEIGHT) == 0 && shipLayout[x,y] != -1 && HasNeighbors(x-1,y) != 0 && shipLayout[x-1,y] != -1){
            return false;
        }
        else{
            return true;
        }
	}

	void UpdateNeighbors(int x, int y){
		if(CheckUp(x,y)){
			rooms[x,y-1].GetComponent<CreateRoom>().UpdateModule(HasNeighbors(x,y-1));
		}
		if(CheckDown(x,y)){
			rooms[x,y+1].GetComponent<CreateRoom>().UpdateModule(HasNeighbors(x,y+1));
		}
		if(CheckLeft(x,y)){
			rooms[x-1,y].GetComponent<CreateRoom>().UpdateModule(HasNeighbors(x-1,y));
		}
		if(CheckRight(x,y)){
			rooms[x+1,y].GetComponent<CreateRoom>().UpdateModule(HasNeighbors(x+1,y));
		}	
	}

	public int[,] GetShipLayout(){
		return shipLayout;
	}

    public void SetShipLayout(int[,] newShipLayout){
		shipLayout = newShipLayout;
		BuildShip();
	}

	void BuildShip(){
		for(int i = 0; i < 5; i++){
			for(int j = 0; j < 5; j++){
				if(shipLayout[j,i] != -1){
					SpawnModuleAtLocation(j,i,shipLayout[j,i]);
				}
				else if(shipLayout[j,i] == -1){
					RemoveModuleAtLocation(j,i);
				}
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStartingShip : MonoBehaviour {

	// Use this for initialization
	public GameObject[] modules;
	const int COCKPIT = 0;
	const int WEAPONSROOM = 1;
	const int ENGINEROOM = 2;
	const int GUN = 3;
	const int FOURWAYROOM = 4;
	const int NOAHGUN = 5;

	private GameObject[,] rooms;
	private GameObject engineRoom;
	private GameObject cockpit;
	private GameObject weaponsRoom;
	private GameObject gun1,gun2;

	private int roomSwitch = 0;
	void Start () {
		int childNum = 0;
		rooms = new GameObject[5,5];
		for(int i = 0; i < 5; i++){
			for(int j = 0; j < 5; j++){
				transform.GetChild(childNum).gameObject.GetComponent<CreateRoom>().InitializeRoom(childNum,j,i);
				childNum++;
			}
		}
		SpawnStartShip();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnStartShip(){
		engineRoom = transform.GetChild(22).gameObject;
		engineRoom.GetComponent<CreateRoom>().BuildRoom(modules[ENGINEROOM],HasNeighbors(2,4),ENGINEROOM);
		rooms[2,4] = engineRoom;
		UpdateNeighbors(2,4);

		weaponsRoom = transform.GetChild(17).gameObject;
		weaponsRoom.GetComponent<CreateRoom>().BuildRoom(modules[WEAPONSROOM],HasNeighbors(2,3),WEAPONSROOM);
		rooms[2,3] = weaponsRoom;
		UpdateNeighbors(2,3);

		cockpit = transform.GetChild(12).gameObject;
		cockpit.GetComponent<CreateRoom>().BuildRoom(modules[COCKPIT],HasNeighbors(2,2),COCKPIT);
		rooms[2,2] = cockpit;
		UpdateNeighbors(2,2);

		gun1 = transform.GetChild(16).gameObject;
		gun1.GetComponent<CreateRoom>().BuildRoom(modules[NOAHGUN],HasNeighbors(1,3),NOAHGUN);
		rooms[1,3] = gun1;
		UpdateNeighbors(1,3);

		gun2 = transform.GetChild(18).gameObject;
		gun2.GetComponent<CreateRoom>().BuildRoom(modules[NOAHGUN],HasNeighbors(3,3),NOAHGUN);
		rooms[3,3] = gun2;
		UpdateNeighbors(3,3);
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
		if(roomSwitch == 0){
			SpawnModule(childNum,modules[FOURWAYROOM],x,y,FOURWAYROOM);
			roomSwitch++;
		}
		
		else if(roomSwitch == 1){
			SpawnModule(childNum,modules[GUN],x,y,GUN);
			roomSwitch = 0;
		}
		
	}
	bool CheckDown(int x, int y){
		if(y == rooms.GetLength(1) - 1){
			return false;
		}
		else if(rooms[x,y+1] == null){
			return false;
		}
		else if(rooms[x,y+1].GetComponent<CreateRoom>().GetModuleType() == COCKPIT){
			return false;
		}
		else if(rooms[x,y+1].GetComponent<CreateRoom>().GetModuleType() == GUN){
			return false;
		}
		else if(rooms[x,y+1].GetComponent<CreateRoom>().GetModuleType() == NOAHGUN){
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
		else if(rooms[x,y-1].GetComponent<CreateRoom>().GetModuleType() == GUN){
			return false;
		}
		else if(rooms[x,y-1].GetComponent<CreateRoom>().GetModuleType() == NOAHGUN){
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
		else if(rooms[x-1,y].GetComponent<CreateRoom>().GetModuleType() == COCKPIT){
			return false;
		}
		else if(rooms[x-1,y].GetComponent<CreateRoom>().GetModuleType() == GUN){
			return false;
		}
		else if(rooms[x-1,y].GetComponent<CreateRoom>().GetModuleType() == NOAHGUN){
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
		else if(rooms[x+1,y].GetComponent<CreateRoom>().GetModuleType() == COCKPIT){
			return false;
		}
		else if(rooms[x+1,y].GetComponent<CreateRoom>().GetModuleType() == GUN){
			return false;
		}
		else if(rooms[x+1,y].GetComponent<CreateRoom>().GetModuleType() == NOAHGUN){
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
}

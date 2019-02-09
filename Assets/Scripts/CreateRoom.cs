using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoom : MonoBehaviour {
	const int COCKPIT = 0;
	const int WEAPONSROOM = 1;
	const int ENGINEROOM = 2;
	const int GUN = 3;
	const int FOURWAYROOM = 4;
	private int x,y;
	private int childNum;
	private int flag = 0;
	private int moduleType = -1;
	private int hasChild = 0;
	private GameObject child;

	private int switchCount = 0;
	void OnMouseDown(){
		this.transform.parent.GetComponent<SpawnStartingShip>().RoomClicked(x,y,childNum);
	}

	public void BuildRoom(GameObject module, int neighbors, int moduleType){
		if(hasChild == 1){
			Destroy(transform.GetChild(0).gameObject);
			hasChild = 0;
			if(switchCount == 0){
				switchCount = 1;
			}
		}
		Instantiate(module,transform.position,Quaternion.identity,transform);
		this.child = transform.GetChild(switchCount).gameObject;
		this.moduleType = moduleType;
		ModuleHandler(moduleType,neighbors);
		hasChild = 1;
	}

	public void InitializeRoom(int childNum, int x, int y){
		this.childNum = childNum;
		this.x = x;
		this.y = y;
	}

	public int GetChildNum(){
		return childNum;
	}

	public void UpdateModule(int neighbors){
		switch(moduleType){
			case COCKPIT:
				child.GetComponent<CockpitConfigurer>().Doors(neighbors);
				break;
			case WEAPONSROOM:
				child.GetComponent<WeaponsRoomConfigurer>().Doors(neighbors);
				break;
			case ENGINEROOM:
				child.GetComponent<EngineConfigurer>().Doors(neighbors);
				break;
			case GUN:
				break;
			case FOURWAYROOM:
				child.GetComponent<RoomConfigurer>().Doors(neighbors);
				break;
		}
	}

	public int GetModuleType(){
		return moduleType;
	}

	private void ModuleHandler(int moduleType, int neighbors){
		switch(moduleType){
			case COCKPIT:
				child.GetComponent<CockpitConfigurer>().Doors(neighbors);
				break;
			case WEAPONSROOM:
				child.GetComponent<WeaponsRoomConfigurer>().Doors(neighbors);
				break;
			case ENGINEROOM:
				child.GetComponent<EngineConfigurer>().Doors(neighbors);
				break;
			case GUN:
				child.gameObject.GetComponent<GunConfigurer>().FaceNeighbor(neighbors);
				break;
			case FOURWAYROOM:
				child.GetComponent<RoomConfigurer>().Doors(neighbors);
				break;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoom : MonoBehaviour {
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
			if(moduleType == (int)GameController.ItemTypes.GUN || moduleType == (int)GameController.ItemTypes.NOAHGUN){
				GameObject.Find("Ship").GetComponent<ShipWeaponController>().RemoveGun(child);
			}
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
			case (int)GameController.ItemTypes.COCKPIT:
				child.GetComponent<CockpitConfigurer>().Doors(neighbors);
				break;
			case (int)GameController.ItemTypes.WEAPONSROOM:
				child.GetComponent<WeaponsRoomConfigurer>().Doors(neighbors);
				break;
			case (int)GameController.ItemTypes.ENGINEROOM:
				child.GetComponent<EngineConfigurer>().Doors(neighbors);
				break;
			case (int)GameController.ItemTypes.GUN:
				break;
			case (int)GameController.ItemTypes.FOURWAYROOM:
				child.GetComponent<RoomConfigurer>().Doors(neighbors);
				break;
			case (int)GameController.ItemTypes.NOAHGUN:
				break;
		}
	}

	public int GetModuleType(){
		return moduleType;
	}

	private void ModuleHandler(int moduleType, int neighbors){
		switch(moduleType){
			case (int)GameController.ItemTypes.COCKPIT:
				child.GetComponent<CockpitConfigurer>().Doors(neighbors);
				break;
			case (int)GameController.ItemTypes.WEAPONSROOM:
				child.GetComponent<WeaponsRoomConfigurer>().Doors(neighbors);
				break;
			case (int)GameController.ItemTypes.ENGINEROOM:
				child.GetComponent<EngineConfigurer>().Doors(neighbors);
				break;
			case (int)GameController.ItemTypes.GUN:
				child.gameObject.GetComponent<GunConfigurer>().FaceNeighbor(neighbors);
				break;
			case (int)GameController.ItemTypes.FOURWAYROOM:
				child.GetComponent<RoomConfigurer>().Doors(neighbors);
				break;
			case (int)GameController.ItemTypes.NOAHGUN:
				child.gameObject.GetComponent<GunConfigurer>().FaceNeighbor(neighbors);
				child.gameObject.GetComponent<GunConfigurer>().AddSelfToGuns();
				break;
		}
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrop : MonoBehaviour {
	public void DropLoot(){
		GameController.instance.AddToInventory();
	}

}
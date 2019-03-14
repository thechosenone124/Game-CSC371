﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBarriersInvulnerable : MonoBehaviour {
	public GameObject barrier;
	public void WeakenBarrier(){
		barrier.GetComponent<DestructableBarrier>().barrierHealth = 15;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Authro: Bryan Tan
public class MouseReticleControl : MonoBehaviour {

   public Texture2D reticle;

	void Start () {
      Vector2 hotSpot = new Vector2(reticle.width / 2, reticle.height / 2);
      Cursor.SetCursor(reticle, hotSpot, CursorMode.Auto);
	}
}

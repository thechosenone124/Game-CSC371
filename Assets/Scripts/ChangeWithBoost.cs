using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Contributors:
 * Zachary Barram
 */
public class ChangeWithBoost : MonoBehaviour {
	private Image boostBar;
	private Color green = new Color(((float)9/255),((float)207/255),((float)116/255),((float)255/255));
	private Color red = new Color(((float)169/255),((float)31/255),((float)19/255),((float)255/255));

	private void Start(){
		boostBar = GetComponent<Image>();
	}
	private void Update(){
		if(GameController.instance.boostBroken){
			boostBar.color = red;
		}
		else{
			boostBar.color = green;
		}
	}
}

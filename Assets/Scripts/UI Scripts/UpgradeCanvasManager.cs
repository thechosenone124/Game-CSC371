using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Contributors:
 * Bryan Tan
 */
public class UpgradeCanvasManager : MonoBehaviour {
	public void disableUI(){
		transform.GetChild(0).gameObject.SetActive(false);
	}
	public void enableUI(){
		transform.GetChild(0).gameObject.SetActive(true);
	}
}

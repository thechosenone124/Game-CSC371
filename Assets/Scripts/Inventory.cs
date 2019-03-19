using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Contributors:
 * Noah Paige
 */
public class Inventory : MonoBehaviour {

    [Range(8, 32)]
    public int MaxItemsPerType = 16;
    private int[] items = new int[(int)GameController.ItemTypes.NUMBEROFTYPES];

    public bool AddItem(int itemCode)
    {
        Debug.Log("Inventory: Adding a " + ((GameController.ItemTypes)itemCode).ToString());
        GameController.instance.AddToInventory();
        if (itemCode >= (int)GameController.ItemTypes.NUMBEROFTYPES) return false; // if index of item type doesn't exist
        else if (itemCode == (int)GameController.ItemTypes.COCKPIT || itemCode == (int)GameController.ItemTypes.WEAPONSROOM || itemCode == (int)GameController.ItemTypes.ENGINEROOM) //cockpit, weaponsroom, and engineroom can only be 0 or 1
        {
            if (items[(int)itemCode] > 0) return false; // don't add another if 1 is already there
        }
        items[(int)itemCode] = (int)Mathf.Min(items[(int)itemCode] + 1, MaxItemsPerType); // try to add the item. If I am already holding the max amount, do not add another.
        return true;
    }

    public bool RemoveItem(int itemCode)
    {
        if (itemCode >= (int)GameController.ItemTypes.NUMBEROFTYPES) return false;  // return false if index of item type doesn't exist
        else if (items[itemCode] == 0) return false;                                //return false if there is nothing to remove
        items[(int)itemCode] = (int)Mathf.Max(items[(int)itemCode] - 1, 0);
        return true;
    }

    public int GetAmountOfType(int itemCode)
    {
        return items[itemCode];
    }

    public int[] GetInventory() { return items; }
}

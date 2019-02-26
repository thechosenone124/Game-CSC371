using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour {

    [Range(8, 32)]
    public int MaxItemsPerType = 16;
    public enum ItemTypes {COCKPIT, WEAPONSROOM, ENGINEROOM, GUN, FOURWAYROOM, NOAHGUN, NUMBEROFTYPES};

    private int[] items = new int[(int)ItemTypes.NUMBEROFTYPES];

    public bool AddItem(int itemCode)
    {
        if (itemCode >= (int)ItemTypes.NUMBEROFTYPES) return false; // if index of item type doesn't exist
        else if(itemCode == (int)ItemTypes.COCKPIT || itemCode == (int)ItemTypes.WEAPONSROOM || itemCode == (int)ItemTypes.ENGINEROOM) //cockpit, weaponsroom, and engineroom can only be 0 or 1
        {
            if (items[(int)itemCode] > 0) return false; // don't add another if 1 is already there
        }
        items[(int)itemCode] = (int)Mathf.Min(items[(int)itemCode] + 1, MaxItemsPerType); // try to add the item. If I am already holding the max amount, do not add another.
        return true;
    }
    
    public bool RemoveItem(int itemCode)
    {
        if (itemCode >= (int)ItemTypes.NUMBEROFTYPES) return false; // if index of item type doesn't exist
        items[(int)itemCode] = (int)Mathf.Max(items[(int)itemCode] - 1, 0);
        return true;
    }
}

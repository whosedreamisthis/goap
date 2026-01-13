using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GInventory
{
    List<GameObject> items = new List<GameObject>();

    public GameObject FindItemByTag(string tagToFind)
    {
        // Finds the first object with the matching tag
        GameObject result = items.FirstOrDefault(obj => obj.CompareTag(tagToFind));
        return result;
    }

    public void AddItem(GameObject g)
    {
        items.Add(g);
    }

    public GameObject RemoveItemWithTag(string tag)
    {
        int goIndex = -1;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].tag == tag)
            {
                goIndex = i;
                break;
            }
        }

        if (goIndex != -1)
        {
            GameObject found = items[goIndex];
            items.RemoveAt(goIndex);
            return found;
        }
        return null;
    }
}

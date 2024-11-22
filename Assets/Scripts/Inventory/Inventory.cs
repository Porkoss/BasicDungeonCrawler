using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public int maxSlots = 20;

    public void AddItem(Item newItem)
    {
        if (newItem.isStackable)
        {
            Item existingItem = items.Find(item => item.itemName == newItem.itemName);

            if (existingItem != null)
            {
                existingItem.quantity += newItem.quantity;
                return;
            }
        }

        if (items.Count < maxSlots)
        {
            items.Add(newItem);
            Debug.Log($"{newItem.itemName} ajouté à l'inventaire.");
        }
        else
        {
            Debug.Log("Inventaire plein !");
        }
    }

    public void RemoveItem(Item itemToRemove)
    {
        if (items.Contains(itemToRemove))
        {
            if(itemToRemove.quantity>1){
                itemToRemove.quantity--;
            }
            else{
                items.Remove(itemToRemove);
                Debug.Log($"{itemToRemove.itemName} retiré de l'inventaire.");
            }

        }
    }

    public void PrintInventory()
    {
        Debug.Log("Contenu de l'inventaire :");
        foreach (Item item in items)
        {
            Debug.Log($"{item.itemName} x{item.quantity}");
        }
    }
}

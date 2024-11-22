using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite icon;
    public int quantity;
    public bool isStackable;

    public string type;

    public Item(string itemName, Sprite icon, int quantity, bool isStackable,string type)
    {
        this.itemName = itemName;
        this.icon = icon;
        this.quantity = quantity;
        this.isStackable = isStackable;
        this.type=type;
    }

    public Item(ItemMono item){
        this.itemName = item.itemName;
        this.icon = item.icon;
        this.quantity = item.quantity;
        this.isStackable = item.isStackable;
        this.type=item.type;
    }

    public virtual void ActivateItem(){
    }
}

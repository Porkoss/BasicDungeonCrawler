using UnityEngine;

public class ItemChildManager
{
    //class to hold the logic of the creation of child item in order to no clutter the player controller function


    public Item CreateItem(ItemMono itemMono){
        if(itemMono.type=="Potion"){
            return new HealthPot(itemMono);
        }
        if(itemMono.type=="Weapon"){
            return new ItemWeapon(itemMono);
        }
        return new Item(itemMono);
    }


}

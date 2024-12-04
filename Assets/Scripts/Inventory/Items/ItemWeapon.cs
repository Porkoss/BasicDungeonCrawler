
using UnityEngine;

public class ItemWeapon : Item
{
    // Start is called before the first frame update

    public override void ActivateItem()
    {
        base.ActivateItem();
        
        PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.weapon.gameObject.SetActive(true);
        playerController.weapon.ResetDurability();
        playerController.GetComponent<EntitySoundManager>().PlayAxeSound();
    }

    public ItemWeapon(ItemMono item) : base(item.itemName, item.icon, item.quantity, item.isStackable, item.type)
    {

    }
}

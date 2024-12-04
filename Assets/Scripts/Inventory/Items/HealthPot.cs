
using UnityEngine;

public class HealthPot : Item
{
    private float healthRecovered;
    // Start is called before the first frame update    public float HealthGained=1;
    public override void ActivateItem()
    {
        base.ActivateItem();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Health health = player.GetComponent<Health>();
        health.Heal(healthRecovered);
        player.GetComponent<EntitySoundManager>().PlayPotSound();
    }

    public HealthPot(ItemMono item) : base(item.itemName, item.icon, item.quantity, item.isStackable, item.type)
    {
        this.healthRecovered=3;
    }




}

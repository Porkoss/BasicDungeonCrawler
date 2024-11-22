using UnityEngine;

public class TestInventorySetup : MonoBehaviour
{
    public Inventory playerInventory;

    public Sprite testIcon1;
    public Sprite testIcon2;
    public Sprite testIcon3;

    void Start()
    {
        if (playerInventory == null)
        {
            Debug.LogError("Player Inventory n'est pas assigné !");
            return;
        }

        // Créer quelques items de test
        Item potion = new Item("Potion de soin", testIcon1, 5, true);
        Item sword = new Item("Épée rouillée", testIcon2, 1, false);
        Item shield = new Item("Bouclier en bois", testIcon3, 1, false);

        // Ajouter ces items à l'inventaire
        playerInventory.AddItem(potion);
        playerInventory.AddItem(sword);
        playerInventory.AddItem(shield);

        // Afficher les items dans la console
        FindObjectOfType<InventoryUI>().UpdateInventoryUI();
    }
}

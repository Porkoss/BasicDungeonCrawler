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
        Item potion = new Item("potion", testIcon1, 1, true, "test");
        Item sword = new Item("Épée rouillée", testIcon2, 1, false,"test");
        Item shield = new Item("Bouclier en bois", testIcon3, 1, false,"test");

        // Ajouter ces items à l'inventaire
        playerInventory.AddItem(potion);
        playerInventory.AddItem(sword);
        playerInventory.AddItem(shield);

        // Afficher les items dans la console
        
    }
}

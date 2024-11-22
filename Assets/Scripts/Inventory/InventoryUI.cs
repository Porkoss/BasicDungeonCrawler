using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Inventory playerInventory; // Référence à l'inventaire du joueur
    public GameObject slotPrefab;  // Prefab pour chaque item
    public Transform inventoryPanel; // Le parent contenant les boutons (Panel avec Grid Layout Group)


    private void Start() {
        UpdateInventoryUI();
    }
    private void Update() {
        CheckItemActivation();
    }

    private void CheckItemActivation()
    {
        for (int i = 0; i < 10; i++)
        {
            // Vérifie si une touche numérique correspondant à l'index est pressée
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) // Alpha1 correspond à la touche "1"
            {
                ActivateItemByIndex(i);
                break; // Stoppe la boucle après avoir trouvé l'index
            }
        }
    }
    private void ActivateItemByIndex(int index)
    {
        if (index >= 0 && index < playerInventory.items.Count)
        {
            var item = playerInventory.items[index];
            Debug.Log($"Activation de l'item : {item.itemName}");
            item.ActivateItem();
            playerInventory.RemoveItem(item);
            UpdateInventoryUI();
        }
    }

    public void UpdateInventoryUI()
    {
        for (int i = 0; i < inventoryPanel.childCount; i++)
        {
            Transform slotTransform = inventoryPanel.GetChild(i);
            InventorySlot slot = slotTransform.GetComponent<InventorySlot>();

            if (i < playerInventory.items.Count)
            {
                slot.InitializeSlot(playerInventory.items[i]);
            }
            else
            {
                slot.ClearSlot();
            }
        }
    }





    /*
    private void OnItemButtonClicked(Item item)
    {
        item.ActivateItem();
        playerInventory.RemoveItem(item);
        
        UpdateInventoryUI();

        // Ajoute ici une action comme afficher un panneau de détails ou utiliser l'item
        
    }
    */
}

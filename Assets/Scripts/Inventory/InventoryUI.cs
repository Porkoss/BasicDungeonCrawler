using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Inventory playerInventory; // Référence à l'inventaire du joueur
    public GameObject slotPrefab;  // Prefab pour chaque item
    public Transform inventoryPanel; // Le parent contenant les boutons (Panel avec Grid Layout Group)


    private void Start() {
        //Debug.Log("Initializing Inventory UI...");
        UpdateInventoryUICACA();
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
        Transform slotTransform = inventoryPanel.GetChild(index);
        InventorySlot slot= slotTransform.GetComponent<InventorySlot>();
        if(!slot.IsEmpty()){
            Item item = slot.item;
            //Debug.Log($"Activation de l'item : {item.itemName}");
            item.ActivateItem();
            Item itemsLeft=playerInventory.RemoveItem(item);
            slot.InitializeSlot(itemsLeft);
        }

    }

    public void UpdateInventoryUICACA()
    {
        //Debug.Log("Resetting layout");
        for (int i = 0; i < 10; i++)
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


    public void AddItemToInventoryVisual(Item item){
        foreach (Transform slotTransform in inventoryPanel){
            InventorySlot inventorySlot=slotTransform.GetComponent<InventorySlot>();
            if(inventorySlot.IsEmpty()){
                inventorySlot.InitializeSlot(item);
                return;
            }
        }
    }

    public void UpdateQuantityOnVisual(Item item){
        InventorySlot inventorySlot=FindSlotWithItem(item);
        inventorySlot.InitializeSlot(item);
    }

    public InventorySlot FindSlotWithItem(Item targetItem)
    {
        // Iterate through all the child slots in the inventory panel
        foreach (Transform slotTransform in inventoryPanel)
        {
            InventorySlot slot = slotTransform.GetComponent<InventorySlot>();
            
            if (slot != null && slot.item == targetItem) // Check if the item in the slot matches the target item
            {
                return slot; // Return the slot containing the item
            }
        }
        return null; // Return null if no slot is found with the target item
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

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Inventory playerInventory; // Référence à l'inventaire du joueur
    public GameObject buttonPrefab;  // Prefab pour chaque item
    public Transform inventoryPanel; // Le parent contenant les boutons (Panel avec Grid Layout Group)

    public void UpdateInventoryUI()
    {
        // Supprime tous les anciens boutons avant d'en ajouter de nouveaux
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }

        // Parcourt tous les items de l'inventaire
        foreach (var item in playerInventory.items)
        {
            //Debug.Log($"Création du bouton pour : {item.itemName}"); // Debug pour chaque item
            // Instancie un bouton à partir du prefab
            GameObject newButton = Instantiate(buttonPrefab, inventoryPanel);

            // Configure le bouton
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = item.itemName; // Affiche le nom de l'item


            TextMeshProUGUI quantityText = newButton.transform.Find("QuantityText")?.GetComponent<TextMeshProUGUI>();//affiche quantité
            quantityText.text = $"x{item.quantity}";
            newButton.GetComponent<Button>().onClick.AddListener(() => OnItemButtonClicked(item));

            // Optionnel : Associe une icône
            if (item.icon != null)
            {
                Image iconImage = newButton.GetComponentInChildren<Image>();
                if (iconImage != null)
                {
                    iconImage.sprite = item.icon;
                }
            }
        }
    }

    private void OnItemButtonClicked(Item item)
    {
        item.ActivateItem();
        playerInventory.RemoveItem(item);
        FindObjectOfType<InventoryUI>().UpdateInventoryUI();

        // Ajoute ici une action comme afficher un panneau de détails ou utiliser l'item
        
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item; // L'item stocké dans ce slot
    public Image icon; // L'icône affichée
    public TextMeshProUGUI quantityText; // Texte pour la quantité
    public Transform originalParent;
    //private Vector3 originPosition; 

    private GameObject dragCopy;

    private Canvas canvas; // Référence au canvas parent pour gérer le drag
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void InitializeSlot(Item newItem)
    {
        item = newItem;
        if (item != null)
        {
            icon.sprite = item.icon;
            icon.enabled = true;
            quantityText.text = item.quantity > 1 ? $"x{item.quantity}" : ""; // Affiche la quantité si > 1
        }
        else
        {
            ClearSlot();
        }
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        quantityText.text = "";
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item == null) return;
        dragCopy = new GameObject("DragCopy", typeof(RectTransform), typeof(CanvasGroup));
        dragCopy.transform.SetParent(canvas.transform, false);

        RectTransform copyRect = dragCopy.GetComponent<RectTransform>();
        copyRect.sizeDelta = rectTransform.sizeDelta;

        // Copy the visuals (icon, text, etc.)
        // Example: Use a prefab or manually clone UI components
        CopyVisuals(dragCopy);

        canvasGroup.alpha = 0.5f; // Fade the original slot
        canvasGroup.blocksRaycasts = false;
        
    }
    private void CopyVisuals(GameObject dragCopy)
    {
        // Example of cloning visuals:
        // Add an Image to the drag copy and set its sprite
        var iconImage = dragCopy.AddComponent<UnityEngine.UI.Image>();
        iconImage.sprite = GetComponentInChildren<UnityEngine.UI.Image>().sprite;
        iconImage.raycastTarget = false; // Prevent the copy from blocking raycasts
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragCopy != null)
        {
            RectTransform copyRect = dragCopy.GetComponent<RectTransform>();
            copyRect.position = eventData.position; // Follow the mouse position
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f; // Restore the original slot
        canvasGroup.blocksRaycasts = true;

        if (dragCopy != null)
        {
            Destroy(dragCopy); // Destroy the visual drag copy
        }

        // Check for valid drop target
        if (eventData.pointerEnter != null && eventData.pointerEnter.GetComponentInParent<InventorySlot>() != null)
        {
            InventorySlot targetSlot = eventData.pointerEnter.GetComponentInParent<InventorySlot>();
            SwapItems(targetSlot);
        }
    }

    private void SwapItems(InventorySlot targetSlot)
    {
        Item tempItem = targetSlot.item;
        targetSlot.InitializeSlot(item);
        InitializeSlot(tempItem);
    }

    public void SetItem(Item newItem)
    {
        item = newItem;
        UpdateSlotUI();
    }

    public void UpdateSlotUI()
    {
        if (item != null)
        {
            icon.sprite = item.icon;
            icon.enabled = true;
            quantityText.text = item.quantity > 1 ? $"x{item.quantity}" : "";
        }
        else
        {
            ClearSlot();
        }
    }

    public bool IsEmpty(){
        return item ==null;
    }
}
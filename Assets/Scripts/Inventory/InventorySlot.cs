using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item; // L'item stocké dans ce slot
    public Image icon; // L'icône affichée
    public TextMeshProUGUI quantityText; // Texte pour la quantité
    public Transform originalParent; // Sauvegarde du parent d'origine pour revenir à sa position initiale

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

        canvasGroup.alpha = 0.6f; // Rendre le slot semi-transparent
        canvasGroup.blocksRaycasts = false; // Permettre au raycast de traverser cet objet
        originalParent = transform.parent; // Sauvegarde du parent actuel
        transform.SetParent(canvas.transform); // Déplace l'objet au niveau du canvas pour un mouvement fluide
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item == null) return;

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; // Suit le curseur
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f; // Rendre le slot opaque à nouveau
        canvasGroup.blocksRaycasts = true;

        // Vérifie si on a relâché le drag sur un autre slot
        if (eventData.pointerEnter != null && eventData.pointerEnter.GetComponent<InventorySlot>() != null)
        {
            InventorySlot targetSlot = eventData.pointerEnter.GetComponent<InventorySlot>();
            SwapItems(targetSlot); // Échange les items entre les deux slots
        }
        else
        {
            // Retourne à la position d'origine si aucun slot valide n'est trouvé
            transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
        }
    }

    private void SwapItems(InventorySlot targetSlot)
    {
        // Échange l'item entre ce slot et le slot cible
        Item tempItem = targetSlot.item;
        targetSlot.InitializeSlot(item);
        InitializeSlot(tempItem);

        // Replace le slot à son parent d'origine
        transform.SetParent(originalParent);
        transform.localPosition = Vector3.zero;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UI_Inventory : MonoBehaviour
{ 

    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private Transform border;
    private PlayerControls player;

    private void Awake() {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }

    public void SetPlayer(PlayerControls player) {
        this.player = player;
    }

    public void SetInventory(Inventory inventory) {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e) {
        RefreshInventoryItems();
    }

    public void RefreshInventoryItems() {
        foreach (Transform child in itemSlotContainer) {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 75f;
        foreach (Item item in inventory.GetItemList()) {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, -y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            if (player.GetActiveItem() == item)
            {
                Image border = itemSlotRectTransform.Find("border").GetComponent<Image>();
                border.GetComponent<Image>().color = Color.red;
            }

            TextMeshProUGUI uiText = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1) {
                uiText.SetText(item.amount.ToString());
            } else {
                uiText.SetText("");
            }

            x++;
            if (x >= 4) {
                x = 0;
                y++;
            }
        }
    }


}

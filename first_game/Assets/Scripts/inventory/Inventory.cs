﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

    public event EventHandler OnItemListChanged;

    public List<Item> itemList;
    private Action<Item> useItemAction;

    public Inventory(Action<Item> useItemAction) {
        this.useItemAction = useItemAction;
        itemList = new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.Coin, amount = 19 });
        AddItem(new Item { itemType = Item.ItemType.Muszla1, amount = 10 });
        AddItem(new Item { itemType = Item.ItemType.Muszla2, amount = 4 });
        AddItem(new Item { itemType = Item.ItemType.Muszla3, amount = 3 });
        AddItem(new Item { itemType = Item.ItemType.Muszla4, amount = 2 });
        AddItem(new Item { itemType = Item.ItemType.Muszla5, amount = 1 });
    }

    public void AddItem(Item item) {
        if (item.IsStackable()) {
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemList) {
                if (inventoryItem.itemType == item.itemType) {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory) {
                itemList.Add(item);
            }
        } else {
            itemList.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item) {
        if (item.IsStackable()) {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList) {
                if (inventoryItem.itemType == item.itemType) {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0) {
                itemList.Remove(itemInInventory);
            }
        } else {
            itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UseItem(Item item) {
        useItemAction(item);
    }

    public List<Item> GetItemList() {
        return itemList;
    }

    public int GetItemListLength()
    {
        int x = 0;
        foreach (Item item in itemList) x++;

        return x;
    }

}

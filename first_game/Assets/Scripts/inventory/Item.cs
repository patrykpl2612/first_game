using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item {

    public enum ItemType {
        Muszla1,
        Muszla2,
        Muszla3,
        Muszla4,
        Muszla5,
        Coin,
        HealthPotion,
        SodaCane,
    }

    public ItemType itemType;
    public int amount;
    public bool active = false;


    public Sprite GetSprite() {
        switch (itemType) {
        default:
        case ItemType.HealthPotion:    return ItemAssets.Instance.healthPotionSprite;
        case ItemType.Coin:            return ItemAssets.Instance.coinSprite;
        case ItemType.Muszla1:         return ItemAssets.Instance.muszla1Sprite;
        case ItemType.Muszla2:         return ItemAssets.Instance.muszla2Sprite;
        case ItemType.Muszla3:         return ItemAssets.Instance.muszla3Sprite;
        case ItemType.Muszla4:         return ItemAssets.Instance.muszla4Sprite;
        case ItemType.Muszla5:         return ItemAssets.Instance.muszla5Sprite;
        case ItemType.SodaCane:        return ItemAssets.Instance.SodaCaneSprite;
        }
    }

    //public Color GetColor() {
    //    switch (itemType) {
    //    default:
    //    case ItemType.Sword:        return new Color(1, 1, 1);
    //    case ItemType.HealthPotion: return new Color(1, 0, 0);
    //    case ItemType.ManaPotion:   return new Color(0, 0, 1);
    //    case ItemType.Coin:         return new Color(1, 1, 0);
    //    case ItemType.Medkit:       return new Color(1, 0, 1);
    //    }
    //}

    public bool IsStackable() {
        switch (itemType) {
        default:
            case ItemType.Coin:
            case ItemType.Muszla1:
            case ItemType.Muszla2:
            case ItemType.Muszla3:
            case ItemType.Muszla4:
            case ItemType.Muszla5:
            case ItemType.SodaCane:
                return true;
        case ItemType.HealthPotion:
            return false;
        }
    }

}

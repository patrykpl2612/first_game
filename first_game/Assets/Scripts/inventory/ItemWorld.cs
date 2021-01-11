using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemWorld : MonoBehaviour {

    public static ItemWorld SpawnItemWorld(Vector3 position, Item item) {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    public static ItemWorld DropItem(Vector3 dropPosition, string Facing, Item item) {
        Vector3 Dir = new Vector3(0,0);
        if (Facing == "S") Dir = new Vector3(0, -1).normalized;
        if (Facing == "N") Dir = new Vector3(0, 1).normalized;
        if (Facing == "E") Dir = new Vector3(1, 0).normalized;
        if (Facing == "W") Dir = new Vector3(-1, 0).normalized;
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + Dir * 2f, item);
        itemWorld.GetComponent<Rigidbody2D>().AddForce(Dir * 4f, ForceMode2D.Impulse);
        return itemWorld;
    }


    private Item item;
    private SpriteRenderer spriteRenderer;
    private TextMeshPro textMeshPro;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    public void SetItem(Item item) {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        if (item.amount > 1) {
            textMeshPro.SetText(item.amount.ToString());
        } else {
            textMeshPro.SetText("");
        }
    }

    public Item GetItem() {
        return item;
    }

    public void DestroySelf() {
        Destroy(gameObject);
    }

}

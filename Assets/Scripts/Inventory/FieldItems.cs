using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItems : MonoBehaviour
{
    public Item item;
    public SpriteRenderer image;

    public void SetItem(Item _item)
    {
        item.itemName = _item.itemName;
        item.itemImage = _item.itemImage;
        item.itemType = _item.itemType;
        item.efts = _item.efts;
        item.itemDesc = _item.itemDesc;

        image.sprite = item.itemImage;
    }
    public Item GetItem() // ������ ȹ��
    {
        return item;
    }
    public void DestroyItem() // ȹ���ϸ� �ʵ� ������ �ı�
    {
        Destroy(gameObject);
    }
}

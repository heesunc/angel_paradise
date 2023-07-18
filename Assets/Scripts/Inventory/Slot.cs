using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int slotnum;
    public Item item;
    public Image itemIcon;
    private ItemDatabase itemDatabase;

    public void Start()
    {
        itemDatabase = FindObjectOfType<ItemDatabase>();
        if (itemDatabase == null)
        {
            Debug.LogError("ItemDatabase not found in the scene.");
        }
    }

    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage;
        itemIcon.gameObject.SetActive(true);
    }

    public void RemoveSlot()
    {
        item = null;
        itemIcon.gameObject.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Slot�� �ִ� item.Use �޼��带 ȣ��
        bool isUse = item.Use();

        // ������ ��뿡 �����ϸ� RemoveItem�� ȣ��
        // RemoveItem�� Inventory�� items���� �˸��� �Ӽ��� ����
        if (isUse)
        {
            // ����â �����
            itemDatabase.HideToolTip();

            // ������ ����
            Inventory.instance.RemoveItem(slotnum);
        }
    }

    // ���콺 Ŀ���� ���Կ� �� �� �ߵ�
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
            itemDatabase.ShowToolTip(item, transform.position);
    }

    // ���콺 Ŀ���� ���Կ��� ���� �� �ߵ�
    public void OnPointerExit(PointerEventData eventData)
    {
        itemDatabase.HideToolTip();
    }
}

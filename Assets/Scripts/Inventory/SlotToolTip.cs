using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotToolTip : MonoBehaviour
{
    [SerializeField]
    private GameObject go_Base; // ��ü���� ���� �г� UI

    [SerializeField]
    private Text txt_ItemName;
    [SerializeField]
    private Text txt_ItemDesc;
    [SerializeField]
    private Text txt_ItemHowtoUsed;

    // Slot���� MouseEnter �̺�Ʈ �߻� �� ȣ���ϱ� ���� public
    public void ShowToolTip(Item item, Vector3 pos) // ������ ������ ��ġ�� Ȱ��ȭ���ֱ� ���� Vector3 �μ� �߰�
    {
        go_Base.SetActive(true); // ���� �г� Ȱ��ȭ
        // �μ��� ������ ������ ��ġ�� ������, �� ��ġ���� ������ Base_Outer�г� �ʺ��� ����,
        // �Ʒ��� Base_Outer �г� ������ ���ݸ�ŭ ������ ��ġ�� Ȱ��ȭ
        pos += new Vector3(go_Base.GetComponent<RectTransform>().rect.width * 1.5f,
                            -go_Base.GetComponent<RectTransform>().rect.height * 1.5f,
                            0);
        go_Base.transform.position = pos; // Base_Outer ���� �г��� ��ġ ����

        // �μ��� ���� �������� �̸��� �������� �ؽ�Ʈ ����
        txt_ItemName.text = item.itemName;
        txt_ItemDesc.text = item.itemDesc;

        // ������ ���� �ؽ�Ʈ�� ���/�Ҹ�ǰ/�ƹ��͵� �ƴ� ���
        /*if (_item.itemType == Item.ItemType.Equipment)
            txt_ItemHowtoUsed.text = "�� Ŭ�� - ����";
        else if (_item.itemType == Item.ItemType.Used)
            txt_ItemHowtoUsed.text = "�� Ŭ�� - �Ա�";
        else*/
        txt_ItemHowtoUsed.text = "�� Ŭ������ ���";
    }

    // Slot.cs���� MouseExit �̺�Ʈ�� �߻����� �� ȣ��� ���̶� public
    public void HideToolTip()
    {
        go_Base.SetActive(false); // ���� �г� ��Ȱ��ȭ
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ItemEffect ��ӹ���
[CreateAssetMenu(menuName ="ItemEft/Consumable/Quest")]
public class ItemTestEft : ItemEffect
{
    public int QuestPoint = 0;

    public override bool ExecuteRole()
    {
        Debug.Log("�÷��̾� ������ ��� Ȯ��" + QuestPoint);        
        return true;
    }    
}

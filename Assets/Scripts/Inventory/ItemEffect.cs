using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ItemEffect ��ũ��Ʈ�� �߻�Ŭ�����̰�, ScriptableObject�� ��ӹ���
public abstract class ItemEffect : ScriptableObject
{
    public abstract bool ExecuteRole(); // �߻�޼���
}

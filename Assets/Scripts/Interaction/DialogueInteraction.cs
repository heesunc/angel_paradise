using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : MonoBehaviour //���� �̺�Ʈ ������Ʈ ��ȣ�ۿ�
{
    public string eventName;
 
    public virtual string GetEvent()
    {

        return eventName;
    }

}

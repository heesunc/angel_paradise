using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touristInteraction : Interaction
{
    public override InteractionEvent GetEvent()
    {
        if (GameManager.Instance.etcProgress[2] == 0) //train ���� ���� ���̶��,
        {
            return Events[0]; //���ఴ ����
        }
        else if(GameManager.Instance.etcProgress[2] == 1) //train ���� ���� ��
        {
            return Events[1]; //���ఴ
        }
        else //train ���� Ŭ���� ��
        {
            return null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

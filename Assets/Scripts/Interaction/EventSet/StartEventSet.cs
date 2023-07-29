using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StartEventSet: MonoBehaviour
{
    public Button[] optionButton;
    private UIManager uiManager;
    //UnityAction[] actionNames;
    public void SetOptionEvent(string eventName, int num)
    {
        InitOptionEvent(); //������ ��ϵ� OnClickEvent ����
        UnityAction[] actionNames = new UnityAction[num];
        switch (eventName)
        {
            case "������ ��1":
                actionNames[0] = ExitDoor1;
                actionNames[1] = UIClose;
                break;
            case "â��":
                actionNames[0] = UIClose;
                actionNames[1] = ExitDoor1;
                actionNames[2] = ExitDoor1;
                actionNames[3] = ExitDoor1;
                break;
            default:
                for (int i = 0; i < num; i++)
                    actionNames[i] = UIClose;
                Debug.Log("�ش� �̺�Ʈ ����");
                break;
        }

        for(int i=0; i<num; i++)
        {
            optionButton[i].onClick.AddListener(actionNames[num - i - 1]); //��ư ���� -> �Ʒ����� ��, �̺�Ʈ ���� -> ������ �Ʒ�
        }
    }

    public void UIClose()
    {
        uiManager.setInActiveUI();
        Debug.Log("UI ����");
        
    }

    public void ExitDoor1()
    {
        Debug.Log("������");
    }

    public void InitOptionEvent()
    {
        for (int i = 0; i < 5; i++)
        {
            optionButton[i].onClick.RemoveAllListeners(); //��ư�� ��ϵ� �Լ� �ʱ�ȭ
        }
    }
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();

    }
}

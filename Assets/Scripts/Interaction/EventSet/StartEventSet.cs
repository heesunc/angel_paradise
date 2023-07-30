using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StartEventSet: MonoBehaviour
{
    public Button[] optionButton;
    private UIManager uiManager;
    private GameObject player;

   
    public void SetOptionEvent(string eventName, int num)
    {
        InitOptionEvent(); //������ ��ϵ� OnClickEvent ����
        UnityAction[] actionNames = new UnityAction[num];
        switch (eventName)
        {
            case "â�� ������":
                actionNames[0] = ExitStorage;
                actionNames[1] = UIClose;
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

    public void InitOptionEvent()
    {
        for (int i = 0; i < 5; i++)
        {
            optionButton[i].onClick.RemoveAllListeners(); //��ư�� ��ϵ� �Լ� �ʱ�ȭ
        }
    }

    public void UIClose()
    {
        uiManager.setInActiveUI();
        Debug.Log("UI ����");
        
    }

    public void ExitStorage()
    {
        uiManager.dialogueUI.GetComponent<DialogueUI>().SetCurrentEvent("â�� ������"); //UI�� event ����
        uiManager.setActiveUI(UIType.talk); //UI Ȱ��ȭ
        StartCoroutine(ExitStorageCoroutine());        
        Debug.Log("������");

    }

    IEnumerator ExitStorageCoroutine()
    {
        yield return new WaitUntil(() => Input.GetKey(KeyCode.Space));
        player.transform.position = new Vector3(-10, 15, 0);
    }

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        player = GameObject.FindGameObjectWithTag("Player");

    }
}

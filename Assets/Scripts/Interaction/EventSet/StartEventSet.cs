using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StartEventSet: MonoBehaviour //��ŸƮ ���� ������ ����
{
    public Button[] optionButton;
    private UIManager uiManager;
    private GameObject player;
    private FadeManager theFade;
    private Inventory inventory;

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
            case "�ŷ�":
                actionNames[0] = Transaction;
                actionNames[1] = UIClose;
                break;
            case "���͸� �ֱ�":
                actionNames[0] = InsertBattery;
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

    public void ExitStorage() //â�� ������
    {
        uiManager.dialogueUI.GetComponent<DialogueUI>().SetCurrentEvent("â�� ������"); //UI�� event ����
        uiManager.setActiveUI(UIType.talk); //UI Ȱ��ȭ
        StartCoroutine(ExitStorageCoroutine());        
        Debug.Log("������");

    }

    IEnumerator ExitStorageCoroutine() //��ũ��Ʈ ������ �ٷ� ������ ���� �ڷ�ƾ
    {
        yield return new WaitUntil(() => Input.GetKey(KeyCode.Space));
        theFade.FadeOut();
        yield return new WaitForSeconds(1f);
        player.transform.position = new Vector3(-10, 15, 0);
        theFade.FadeIn();
    }

    public void Transaction() //�ŷ� ������
    {
        Debug.Log("�ŷ�,��");
        inventory.RemoveItem("�������� ��������");
        uiManager.dialogueUI.GetComponent<DialogueUI>().SetCurrentEvent("�ŷ�2"); //UI�� event ����
        uiManager.setActiveUI(UIType.talk); //UI Ȱ��ȭ
    }

    public void InsertBattery() //���͸� �ֱ� ������
    {
        inventory.RemoveItem("���͸�");
        uiManager.dialogueUI.GetComponent<DialogueUI>().SetCurrentEvent("���� �簡��"); //UI�� event ����
        uiManager.setActiveUI(UIType.talk); //UI Ȱ��ȭ
    }

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        theFade = FindObjectOfType<FadeManager>();
        inventory = FindObjectOfType<Inventory>();
    }
}

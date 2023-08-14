using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInputUI : MonoBehaviour
{
    public InputField inputField;
    public UIManager uiManager;
    private string currentEvent; //���� �̺�Ʈ ����
    private string gladEmotionPassword = "E311y";

    public void SetCurrentEvent(InteractionEvent _event)
    {
        currentEvent = _event.eventName;
        inputField.onSubmit.RemoveAllListeners(); //�Է� �̺�Ʈ �ʱ�ȭ

        switch (_event.eventName)
        {
            case "��� ���� �ޱ� ��ȣ":
                inputField.onSubmit.AddListener(delegate { Password(gladEmotionPassword); });
                break;
        }
    }

    public void UIClose()
    {
        uiManager.setInActiveUI();
        Debug.Log("UI ����");

    }

    public void Password(string password)
    {
        if(inputField.text == gladEmotionPassword)
        {
            LoadNewDialogue("��� ���� �ޱ�");
            uiManager.textInputUI.SetActive(false);
        }
        else
        {
            LoadNewDialogue("��ȣ Ʋ��");
            uiManager.textInputUI.SetActive(false);
        }
    }

    public void LoadNewDialogue(string eventName)
    {
        uiManager.dialogueUI.GetComponent<DialogueUI>().SetCurrentEvent(eventName); //UI�� event ����
        uiManager.setActiveUI(UIType.talk); //UI Ȱ��ȭ
    }

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class DialogueUI : MonoBehaviour //��ȭ UI
{
    //��ȭ UI (TalkUI)
    public Text speaker;
    public Text context;
    private UIManager uiManager;
    public bool isKeyDown = false;

    //������
    private int numOption = 5;
    public GameObject optionsParent;
    public GameObject[] optionButtons;
    private Text[] buttonText;
    public StartEventSet eventData1;

    //��ȭ data
    [SerializeField] TalkData[] talkData; //�̸�, ��� �迭 �� �̷���� ����ü
    private string currentEvent; //���� �̺�Ʈ ����
    private int index1 = 0;
    private int index2 = 0;

    public void IndexInit() //�ε��� �ʱ�ȭ
    {
        index1 = 0;
        index2 = 0;
    }

    public void SetCurrentEvent(string eventName)
    {
        IndexInit();
        currentEvent = eventName;
        talkData = DialogueData.GetDialogue(currentEvent); //��ȭ ������ �ε�
        SetSentence(index1, index2);

    }

    public void SetSentence(int index1, int index2)
    {
        speaker.text = talkData[index1].name + ": ";
        context.text = talkData[index1].constexts[index2]; //�̸�, ������ �ؽ�Ʈ�� ����

        SetOption(talkData[index1].options[index2]);
    }

    public void SetOption(string options)
    {
        if (options.Trim() != "") //�������� ��ȿ�ϸ�,
        {
            optionsParent.SetActive(true); //�θ� Ȱ��ȭ
            string[] option = options.Split("/"); //�ɼ� ������
            for (int i = 0; i < option.Length; i++) //������ �ɼ� ������ŭ ��ư Ȱ��ȭ
            {
                optionButtons[i].GetComponentInChildren<Text>().text = option[option.Length - i - 1]; //�ؽ�Ʈ�� ������ ����
                optionButtons[i].SetActive(true);
                //��ư �̺�Ʈ ����
            }
            for (int i = numOption - 1; i > option.Length - 1; i--) //������ ��ư ��Ȱ��ȭ
            {
                optionButtons[i] = optionsParent.transform.GetChild(i).gameObject;
                optionButtons[i].SetActive(false);
            }
            //��ư �̺�Ʈ ����
            eventData1.SetOptionEvent(currentEvent, option.Length);
        }
        else
            optionsParent.SetActive(false); //������ ������ �θ� ��Ȱ��ȭ
    }

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && uiManager.currentUI == UIType.talk)
        {
            isKeyDown = true;
            if (index1 < talkData.Length && index2 + 1 < talkData[index1].constexts.Length) //��� ������Ʈ
            {
                index2++;
                SetSentence(index1, index2);
            }
            else if (index1 + 1 < talkData.Length) //���� ��� ���
            {
                index1++;
                index2 = 0;
                SetSentence(index1, index2);
            }
            else //��� ��
            {
                uiManager.setInActiveUI(); //UI ��Ȱ��ȭ
            }

        }
        else
            isKeyDown = false;
    }
}

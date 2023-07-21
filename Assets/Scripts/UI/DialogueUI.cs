using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class DialogueUI : MonoBehaviour //��ȭ UI
{
    //��ȭ UI (TalkUI)
    public Text talkText;
    public GameObject scanObject;
    private UIManager uiManager;

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
        talkText.text = talkData[index1].name + ": " + talkData[index1].constexts[index2];
    }

    public void SetSentence(int index1, int index2)
    {
        talkText.text = talkData[index1].name + ": " + talkData[index1].constexts[index2]; //�̸�, ������ �ؽ�Ʈ�� ����
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
    }
}

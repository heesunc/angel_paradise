using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class DialogueUI : MonoBehaviour //��ȭ UI
{
    //��ȭ UI (TalkUI)
    public GameObject nameBox;
    public Text speaker;
    public Text context;
    private UIManager uiManager;
    public bool isKeyDown = false;
    //Ÿ���� ȿ��
    public float typingDelay = 0.015f;
    public bool isTyping;


    //Image
    public GameObject nextImage;
    public GameObject faceImage;

    //������
    private int numOption = 5;
    public GameObject optionsParent;
    public GameObject[] optionButtons;
    private Text[] buttonText;
    public StartEventSet eventData1;
    bool haveOption;

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

    public void SetCurrentEvent(InteractionEvent _event)
    {
        if (_event == null)
            return;
        IndexInit();
        currentEvent = _event.eventName;
        talkData = DialogueData.GetDialogue(currentEvent); //��ȭ ������ �ε�
        SetSentence(index1, index2);

        //if(GameManager.progress < _event.scriptNumber)
        //    GameManager.progress = _event.scriptNumber;
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
        if (talkData[index1].name.Trim() == ".")
        {
            nameBox.SetActive(false);
        }
        else
        {
            nameBox.SetActive(true);
        }
        speaker.text = talkData[index1].name;
        //context.text = talkData[index1].constexts[index2]; //�̸�, ������ �ؽ�Ʈ�� ����
        StartCoroutine(SetContext(talkData[index1].constexts[index2]));

        SetOption(talkData[index1].options[index2]);
        //SetImage(talkData[index1].images[index2]);
        
        if (index1 + 1 >= talkData.Length && index2 + 1 >= talkData[index1].constexts.Length)
            nextImage.SetActive(false);
        else
            nextImage.SetActive(true);
    }

   IEnumerator SetContext(string a)
    {
        isTyping = true;
        context.text = string.Empty;
        for(int i=0; i<a.Length; i++)
        {
            context.text += a[i];
            yield return new WaitForSeconds(typingDelay);
        }
        isTyping = false;

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
            haveOption = true;
        }
        else
        {
            optionsParent.SetActive(false); //������ ������ �θ� ��Ȱ��ȭ
            haveOption = false;
        }
            
    }

    public void SetImage(string imageName)
    {
        if(imageName.Trim()!="")
        {
            faceImage.SetActive(true);
            string PATH = "Sprites/" + imageName.Trim();
            faceImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(PATH);
            context.GetComponent<RectTransform>().anchoredPosition = new Vector3(300, 0, 0);
            context.GetComponent<RectTransform>().sizeDelta = new Vector2(1500, 200);
        }
        else
        {
            faceImage.SetActive(false);
            context.GetComponent<RectTransform>().anchoredPosition = new Vector3(20, 0, 0);
            context.GetComponent<RectTransform>().sizeDelta = new Vector2(1800, 200);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && uiManager.currentUI == UIType.talk && !haveOption && !isTyping)
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
            else if(!haveOption)//��� �� && �������� ���ٸ�
            {
                uiManager.setInActiveUI(); //UI ��Ȱ��ȭ
            }
            //Debug.Log(index1 + " " + index2);
        }
        else
            isKeyDown = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StartEventSet : MonoBehaviour //��ŸƮ ���� ������ ����
{
    public Button[] optionButton;
    private UIManager uiManager;
    private Inventory inventory;
    private trainPuzzleManager tPuzzle;

    public void SetOptionEvent(string eventName, int num)
    {
        InitOptionEvent(); //������ ��ϵ� OnClickEvent ����
        UnityAction[] actionNames = new UnityAction[num];
        switch (eventName)
        {
            case "���ӽ���1":
                actionNames[0] = () => LoadNewDialogue("���ӽ���3"); //��
                actionNames[1] = () => LoadNewDialogue("���ӽ���2"); //�ƴ�
                break;
            case "���ӽ���2":
                actionNames[0] = () => LoadNewDialogue("���ӽ���3"); //��
                actionNames[1] = () => LoadNewDialogue("���ӽ���2"); //�ƴ�
                break;
            case "�������� ���� ����1":
                actionNames[0] = () => LoadNewDialogue("�������� ���� ����2");
                break;
            case "�������� ���� ����2":
                actionNames[0] = () => LoadNewDialogue("�������� ���� ����3"); //������ ������ �ߵ������� �� ����
                actionNames[1] = () => LoadNewDialogue("�������� ���� ����4"); //�����󿡰� ���� ���� ���� �� ����
                break;
            case "â�� ������":
                actionNames[0] = ExitStorage; //��
                actionNames[1] = UIClose; //�ƴ�
                break;
            case "�Ž� ȭ��":
                actionNames[0] = FlowerPot;
                break;
            case "�� ���� �ƽ�":
                actionNames[0] = GoOutMax;
                break;
            case "�ι�° ���ζ���":
                actionNames[0] = PostGet;
                break;
            case "�������� ���� 0":
                actionNames[0] = () => LoadNewDialogue("�������� ���� 1-1");
                actionNames[1] = () => LoadNewDialogue("�������� ���� 1-2");
                actionNames[2] = () => LoadNewDialogue("�������� ���� 1-3");
                break;
            case "�������� ���� 1-2":
                actionNames[0] = () => LoadNewDialogue("�������� ���� 2-1");
                actionNames[1] = () => LoadNewDialogue("�������� ���� 2-2");
                actionNames[2] = () => LoadNewDialogue("�������� ���� 2-3");
                break;
            case "�������� ���� 2-2":
                actionNames[0] = () => LoadNewDialogue("�������� ���� 3-1");
                actionNames[1] = () => LoadNewDialogue("�������� ���� 3-2");
                actionNames[2] = () => LoadNewDialogue("�������� ���� 3-3");
                break;
            case "���ఴ ����":
                actionNames[0] = TrainRoadPuzzleStart;
                actionNames[1] = () => LoadNewDialogue("ö�� ���� ������");
                break;
            case "�ǳʰ���001":
                actionNames[0] = () => TrainPuzzleToggle(2);
                actionNames[1] = () => TrainPuzzleToggle(3);
                break;
            case "�ǳʰ���010":
                actionNames[0] = () => TrainPuzzleToggle(1);
                actionNames[1] = () => TrainPuzzleToggle(3);
                break;
            case "�ǳʰ���011":
                actionNames[0] = () => TrainPuzzleToggle(1);
                actionNames[1] = () => TrainPuzzleToggle(2);
                actionNames[2] = () => TrainPuzzleToggle(3);
                break;
            case "�ǳʰ���100":
                actionNames[0] = () => TrainPuzzleToggle(0);
                actionNames[1] = () => TrainPuzzleToggle(3);
                break;
            case "�ǳʰ���101":
                actionNames[0] = () => TrainPuzzleToggle(0);
                actionNames[1] = () => TrainPuzzleToggle(2);
                actionNames[2] = () => TrainPuzzleToggle(3);
                break;
            case "�ǳʰ���110":
                actionNames[0] = () => TrainPuzzleToggle(0);
                actionNames[1] = () => TrainPuzzleToggle(1);
                actionNames[2] = () => TrainPuzzleToggle(3);
                break;
            case "�ǳʰ���111":
                actionNames[0] = () => TrainPuzzleToggle(0);
                actionNames[1] = () => TrainPuzzleToggle(1);
                actionNames[2] = () => TrainPuzzleToggle(2);
                actionNames[3] = () => TrainPuzzleToggle(3);
                break;
            default:
                for (int i = 0; i < num; i++)
                    actionNames[i] = UIClose;
                Debug.Log("�ش� �̺�Ʈ ����");
                break;
        }

        EventSystem.current.SetSelectedGameObject(optionButton[num - 1].gameObject); //UI ��Ŀ�� ����
        for (int i = 0; i < num; i++)
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

    public void LoadNewDialogue(string eventName)
    {
        StartCoroutine(LoadNewDialogueCoroutine(eventName));
    }

    IEnumerator LoadNewDialogueCoroutine(string eventName)
    {
        yield return new WaitUntil(()=>!uiManager.dialogueUI.GetComponent<DialogueUI>().isTyping);
        //UIClose();
        uiManager.setActiveUI(UIType.talk); //UI Ȱ��ȭ
        uiManager.dialogueUI.GetComponent<DialogueUI>().SetCurrentEvent(eventName); //UI�� event ����
    }

    public void ExitStorage() //â�� ������
    {
        if (GameManager.Instance.progress < 2)
        {
            LoadNewDialogue("â�� ������");           
        }
        StartCoroutine(ExitStorageCoroutine());    
    }

    IEnumerator ExitStorageCoroutine() //��ũ��Ʈ ������ �ٷ� ������ ���� �ڷ�ƾ
    {
        if (GameManager.Instance.progress < 2)
        {
            yield return new WaitUntil(() => (uiManager.currentUI == UIType.none)); //��ũ��Ʈ ���� ������ ��ٸ���
            GameManager.Instance.progress = 2;
        }
        UIClose();
        FadeManager.Instance.FadeOut();
        yield return new WaitForSeconds(1f);
        Player.Instance.transform.position = new Vector3(-12, 24, 0);
        FadeManager.Instance.FadeIn();
    }

    public void FlowerPot()
    {
        LoadNewDialogue("�������� ȭ��");
        if(GameManager.Instance.etcProgress[0] < 3)
            GameManager.Instance.etcProgress[0]++; //����1 ����� 3��
    }

    public void GoOutMax()
    {
        StartCoroutine(GoOutMaxCoroutine());
        if (GameManager.Instance.etcProgress[0] < 4)
            GameManager.Instance.etcProgress[0]++; //����1 ����� 4��
        
        //Destroy(dog);
    }

    IEnumerator GoOutMaxCoroutine()
    {
        FadeManager.Instance.FadeOut();

        postInteraction[] asd = FindObjectsOfType<postInteraction>();
        GameObject dog = null;
        for (int i = 0; i < asd.Length; i++)
        {
            if (asd[i].condition == 3)
            {
                dog = asd[i].gameObject;
                break;
            }
        }
        if (dog == null)
            Debug.LogWarning("Dog is NULL");
        dog.transform.position = new Vector3(10, 18, 0);

        yield return new WaitForSeconds(0.5f);
        FadeManager.Instance.FadeIn();

        //yield return new WaitUntil(() => (FadeManager.Instance.isFade));
        LoadNewDialogue("�ƽ� �Ʒ� ����");
    }

    public void PostGet()
    {
        LoadNewDialogue("���� ȹ��");
        GameManager.Instance.etcProgress[0]++; //����1 ����� 5��
    }

    public void TrainRoadPuzzleStart()
    {
        LoadNewDialogue("ö�� ���� ����");
        GameManager.Instance.etcProgress[2]++; //etcProgress[2]�� 1��
        
    }    

    public void TrainPuzzleToggle(int index)
    {
        UIClose();
        tPuzzle.toggleState(index);
        StartCoroutine(TrainRoadToCross());

        if (tPuzzle.state[0] == tPuzzle.state[1] && tPuzzle.state[0] != tPuzzle.state[3]) //�� & ����̸� ����, �÷��̾� X
        {
            StartCoroutine(RollBackTrainPuzzle());
        }
        else if (tPuzzle.state[1] == tPuzzle.state[2] && tPuzzle.state[2] != tPuzzle.state[3]) //����� & ���� ����, �÷��̾� X
        {
            StartCoroutine(RollBackTrainPuzzle());
        }

    }

    IEnumerator TrainRoadToCross()
    {
        FadeManager.Instance.FadeOut();
        yield return new WaitForSeconds(0.5f);
        tPuzzle.MoveAnimal();
        yield return new WaitForSeconds(0.5f);
        FadeManager.Instance.FadeIn();

        if (tPuzzle.state[0] && tPuzzle.state[1] && tPuzzle.state[2])
        {
            Debug.Log("Clear");
            LoadNewDialogue("ö�� ���� �Ϸ�");
            GameManager.Instance.etcProgress[2]++; //2�� ����
        }
    }

    IEnumerator RollBackTrainPuzzle()
    {
        yield return new WaitForSeconds(2f);
        tPuzzle.InitState();
        StartCoroutine(TrainRoadToCross());
    }

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        inventory = FindObjectOfType<Inventory>();
        tPuzzle = FindObjectOfType<trainPuzzleManager>();
    }
}

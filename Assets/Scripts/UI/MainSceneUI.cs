using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MainSceneUI : MonoBehaviour
{
    public GameObject loadSlot;
    public GameObject loadBtn;
    public Text[] slotText; // slot ��������� ���� ����
    bool[] saveFile = new bool[5]; // ���̺� ���� ���� ����
    string[] slotTextInfo = new string[5];

    // Start is called before the first frame update
    void Start()
    {
        SlotSaveData();
    }

    void SlotSaveData()
    {
        // ���Ժ��� ����� �����Ͱ� �����ϴ��� �Ǵ�
        for (int i = 0; i < 5; i++)
        {
            if (File.Exists(DataManager.instance.path + $"{i}"))
            {
                saveFile[i] = true; // ���Կ� �ִ��� üũ�ϰ�
                DataManager.instance.nowSlot = i; // �� ��° ��������
                DataManager.instance.LoadData(); // �� ���� ������ ������
                slotText[i].text = i + "���� �׽�Ʈ";
            }
            else
            {
                slotText[i].text = "�������";
            }
        }
        //DataManager.instance.DataClear();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            loadSlot.gameObject.SetActive(false);
        }
    }

    public void LoadSlot(int number) // ���� ���� �Ű�����
    {
        DataManager.instance.nowSlot = number;

        if (saveFile[number]) // ����� �����Ͱ� �ִٸ�
        {
            // ���� ������ �Ѿ����
            DataManager.instance.LoadData();
            GoGame();
            Debug.Log(number + "�ε���");
        }
    }

    public void SaveSlot(int number)
    {
        DataManager.instance.nowSlot = number;

        if (!saveFile[DataManager.instance.nowSlot]) // ���� ���� ��ȣ�� �����Ͱ� ���ٸ�
        {
            DataManager.instance.SaveData(); // ���� ������ ������
            Debug.Log(number + "������");
        }
    }

    public void OpenLoadSlot()
    {
        loadSlot.gameObject.SetActive(true);
    }

    public void OpenLoadBtn()
    {
        loadBtn.gameObject.SetActive(true);
    }

    public void GoGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Save()
    {
        DataManager.instance.SaveData();
    }
}

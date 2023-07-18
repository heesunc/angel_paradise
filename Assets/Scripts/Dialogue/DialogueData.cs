using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TalkData
{
    public string name;
    public string[] constexts;
}

public class DialogueData : MonoBehaviour
{
    ////��ȭ �̺�Ʈ �̸�
    //[SerializeField] string eventName;
    //������ ������ TalkData �迭
    TalkData[] talkDatas;
    public static Dictionary<string, TalkData[]> DialogueDictionary = new Dictionary<string, TalkData[]>();
    public TextAsset csvFile = null;
    public string csvText;
    public string[] rows;

    // Start is called before the first frame update
    void Awake()
    {
        SetDialogueDictionary();      
    }

    public void SetDialogueDictionary()
    {
        csvText = csvFile.text.Substring(0, csvFile.text.Length - 1); //������ �� �� ����
        rows = csvText.Split(new char[] { '\n' }); //�� ������ ������
        for (int i = 1; i < rows.Length; i++)
        {
            string[] rowValues = rows[i].Split(new char[] { ',' });
            if (rowValues[0].Trim() == "" || rowValues[0].Trim() == "end") //��ȿ �̺�Ʈ �̸��� �ƴ� ��� continue;
                continue;

            List<TalkData> talkDataList = new List<TalkData>();
            string eventName = rowValues[0]; //��ȿ �̺�Ʈ �̸��� ��� ����

            while (rowValues[0].Trim() != "end")
            {
                List<string> contextList = new List<string>();

                TalkData talkData;
                talkData.name = rowValues[1]; //����ü�� �̸� ����

                do
                {
                    contextList.Add(rowValues[2].ToString());  //��� ����
                    if (++i < rows.Length)
                        rowValues = rows[i].Split(new char[] { ',' });  //���� ��絵 ������
                    else break;

                } while (rowValues[1] == "" && rowValues[0] != "end"); //���� �ι��� ���ϴ� ���� �ݺ�

                talkData.constexts = contextList.ToArray(); //�̸�, ���� ��� talkData ����ü �ϼ�
                talkDataList.Add(talkData); //�ϳ��� �̺�Ʈ�� �ش��ϴ� ���� ����
            }
            DialogueDictionary.Add(eventName, talkDataList.ToArray()); //�̺�Ʈ �̸� - ���� �� key,value ��� ��ųʸ� �߰�
        }
    }

    public static TalkData[] GetDialogue(string eventName)
    {
        return DialogueDictionary[eventName];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

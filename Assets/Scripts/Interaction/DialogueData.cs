using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct TalkData
{
    public string name;
    public string[] constexts;
    public string[] options;
}

[System.Serializable]
public class DialogueData : MonoBehaviour
{
    ////��ȭ �̺�Ʈ �̸�
    //[SerializeField] string eventName;
    //������ ������ TalkData �迭
    TalkData[] talkDatas;
    public static Dictionary<string, TalkData[]> DialogueDictionary = new Dictionary<string, TalkData[]>();
    public TextAsset[] csvFile = null;
    public string tempText;
    public string csvText;
    public string[] rows;

    // Start is called before the first frame update
    void Awake()
    {
        SetDialogueDictionary();      
    }

    public void SetDialogueDictionary()
    {
        
        for(int i=0; i<csvFile.Length; i++)
        {
            csvText = csvFile[i].text.Substring(0, csvFile[i].text.Length - 1); //������ �� �� ����
            if (i > 0)
                tempText += '\n';
            tempText += csvText;
        }
        csvText = tempText;

        rows = csvText.Split(new char[] { '\n' }); //�� ������ ������
        
        for(int i=0; i<rows.Length; i++)
        {
            rows[i] = rows[i].Replace("\\n", "\n");
            rows[i] = rows[i].Replace("\"\"\"", "\"");
        }

        for (int i = 1; i < rows.Length; i++)
        {
            string[] rowValues = rows[i].Split(new char[] { ',' });
            if (rowValues[0].Trim() == "" || rowValues[0].Trim() == "end" || rowValues[0].Trim() == "EventName") //��ȿ �̺�Ʈ �̸��� �ƴ� ��� continue;
                continue;

            List<TalkData> talkDataList = new List<TalkData>();
            string eventName = rowValues[0]; //��ȿ �̺�Ʈ �̸��� ��� ����

            while (rowValues[0].Trim() != "end")
            {
                List<string> contextList = new List<string>();
                List<string> optionList = new List<string>();

                TalkData talkData;
                talkData.name = rowValues[1]; //����ü�� �̸� ����

                do
                {
                    contextList.Add(rowValues[2].ToString());  //��� ����
                    optionList.Add(rowValues[3].ToString());
                    if (rowValues[3].Trim() != "") //������
                        Debug.Log("������");

                    if (++i < rows.Length)
                    {
                        rowValues = rows[i].Split(new char[] { ',' });  //���� ��絵 ������
                        for (int j=0; j<rowValues.Length; j++)
                        {
                            rowValues[j] = rowValues[j].Replace("@", ",");
                        }
                    }  
                    else break;

                } while (rowValues[1] == "" && rowValues[0] != "end"); //���� �ι��� ���ϴ� ���� �ݺ�

                talkData.constexts = contextList.ToArray(); 
                talkData.options = optionList.ToArray(); //�̸�, ���, �������� ��� talkData ����ü �ϼ�
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

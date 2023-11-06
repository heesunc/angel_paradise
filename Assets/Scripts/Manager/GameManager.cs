using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public int progress { get; set; } //����

    public int [] etcProgress { get; set; } //��Ÿ ���� ���൵ / 0: ����, 1: �������� ����, 2: ������
    public int [] progress1 { get; set; } //���� 1 ���� ���൵ / ũ��: 3
    public int [] progress2 { get; set; } //���� 2 ���� ���൵ / ���൵ ũ��: 5
    public int [] progress3 { get; set; } //���� 3 ���� ���൵ / ���൵ ũ��: 7

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
      
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        Initialized();
    }
    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void Initialized()
    {
        etcProgress = new int[3];
        progress1 = new int[3];
        progress2 = new int[5];
        progress3 = new int[7];

    }
}

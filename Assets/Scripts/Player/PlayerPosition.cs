using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    // �÷��̾� ��ġ ����, �ҷ�����
    private Vector3 playerPosition = new Vector3(0f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        // �÷��̾� ��ġ �ҷ�����
        playerPosition = DataManager.instance.nowPlayer.playerPosition;
        transform.position = playerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // �÷��̾� ��ġ ����
        Vector3 playerPosition = transform.position;
        DataManager.instance.nowPlayer.playerPosition = playerPosition;        
    }
}

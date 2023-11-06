using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trainPuzzleManager : MonoBehaviour
{
    public bool[] state; //0: ��, 1: �����, 2: ��, 3: �÷��̾�, ���� false�� �ǳʱ� ��, true�̸� �ǳ� ��
    public bool [] beforeState; //���� ����
    public GameObject[] animals;

    // Start is called before the first frame update
    void Awake()
    {
        state = new bool[4];
        beforeState = new bool[3];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleState(int index)
    {
        if (index < 0 || index > 3)
            Debug.LogWarning("indexError");

        for(int i=0; i<3; i++)
        {
            beforeState[i] = state[i];
        }

        if (index != 3) //index = 3 -> �ƹ��� ������ ���� �ʴ´�.
        {
            state[index] = !state[index];
        }
        state[3] = !state[3]; //player state �ٲٱ�.
    }

    public void rollbackState()
    {
        for (int i = 0; i < 3; i++)
            state[i] = beforeState[i];
        state[3] = !state[3];
    }

    public void MoveAnimal()
    {
        if (state[0]) //dog
            animals[0].transform.position = new Vector3(230, 17, 0);
        else
            animals[0].transform.position = new Vector3(225, 36, 0);

        if (state[1]) //bird
            animals[1].transform.position = new Vector3(233, 17, 0);
        else
            animals[1].transform.position = new Vector3(228, 36, 0);

        if (state[2]) //cat
            animals[2].transform.position = new Vector3(236, 17, 0);
        else
            animals[2].transform.position = new Vector3(231, 36, 0);

        if (state[3]) //player
            Player.Instance.transform.position = new Vector3(233, 20, 0);
        else
            Player.Instance.transform.position = new Vector3(225, 40, 0);
    }
}

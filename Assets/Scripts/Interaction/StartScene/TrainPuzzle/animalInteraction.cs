using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animalInteraction : Interaction
{
    public trainPuzzleManager puzzleManager;

    public override InteractionEvent GetEvent()
    {
        Debug.Log(GameManager.Instance.etcProgress[2]);
        if (GameManager.Instance.etcProgress[2] != 1) //������ ������ ���� �� X
        {
            return null;
        }

        InteractionEvent e = new InteractionEvent();
        e.eventType = InteractionType.Dialogue;
        string eName = "�ǳʰ���";

        if (puzzleManager.state[3] == puzzleManager.state[0]) //player�� ���°� ������ 1, �ƴϸ� 0
            eName += "1";
        else
            eName += "0";

        if (puzzleManager.state[3] == puzzleManager.state[1])
            eName += "1";
        else
            eName += "0";

        if (puzzleManager.state[3] == puzzleManager.state[2])
            eName += "1";
        else
            eName += "0";

        e.eventName = eName;
        Debug.Log(e);
        Debug.Log(e.eventName);
        Debug.Log(e.eventType);
        return e;
    }

    // Start is called before the first frame update
    void Start()
    {
        puzzleManager = FindObjectOfType<trainPuzzleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

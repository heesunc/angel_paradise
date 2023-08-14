using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorInteraction : Interaction
{
    Inventory inventory;

    private void Start()
    {
        inventory = GameObject.FindObjectOfType<Inventory>();
    }
    public override InteractionEvent GetEvent()
    {
        if (inventory.SearchInventory("����") != 0 && inventory.SearchInventory("�������� ��������") != 0 || GameManager.Instance.progress >= 2)
            return Events[0];
        else
            return Events[1];

    }
}

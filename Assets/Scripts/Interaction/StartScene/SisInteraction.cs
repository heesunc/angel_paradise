using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SisInteraction : Interaction
{

    public override InteractionEvent GetEvent()
    {
        if (GameManager.Instance.progress >= 5)
            GameManager.Instance.progress++;
        return Events[0];
    }
}
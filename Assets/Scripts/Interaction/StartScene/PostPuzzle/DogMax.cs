using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMax : Interaction
{
    public override InteractionEvent GetEvent()
    {
        if (GameManager.Instance.etcProgress[0] == 3)
        {
            return Events[0];
        }
        else
            return null;
    }
}


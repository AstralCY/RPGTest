using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healitem : Item, IUse
{

    public bool Use()
    {
        if (PlayerManager.Instance.status.CanHeal())
        {
            PlayerManager.Instance.Restore(this);
            return true;
        }
        return false;
    }
}

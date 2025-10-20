using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : CharacterStatus
{
    protected override void Die()
    {
        base.Die();
        Debug.Log("player die");
    }
}

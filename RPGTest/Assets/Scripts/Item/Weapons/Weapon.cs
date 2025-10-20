using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon :Item, IEquip
{
    
    public virtual void Attack()
    {
    }

    public void Arm()
    {
        PlayerManager.Instance.ArmWeapon(this);
    }


    public void UnArm()
    {
        PlayerManager.Instance.UnArmWeapon(this);
    }
}

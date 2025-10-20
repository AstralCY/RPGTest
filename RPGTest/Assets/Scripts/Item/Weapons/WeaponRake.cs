using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRake : Weapon
{
    [SerializeField]private Animator anim;
    private GameObject ringPrefab;
    [SerializeField] private Transform rakeTip;


    private void Start()
    {
        anim = GetComponent<Animator>();
        ringPrefab = Resources.Load<GameObject>("Prefabs/Weapons/RakeRing");
    }

    public override void Attack()
    {
        anim.SetTrigger("IsAttack");
    }

    public void CallRing()
    {
        GameObject ring = GameObject.Instantiate(ringPrefab, rakeTip.position, Quaternion.identity);
        ring.GetComponent<RakeRing>().InitRing(this.GetValue(ItemData.ItemPropertyType.Speed));
    }


}

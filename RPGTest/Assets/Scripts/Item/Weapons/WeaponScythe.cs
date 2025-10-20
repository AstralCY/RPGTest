using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScythe : Weapon
{
    [SerializeField]private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public override void Attack()
    {
        anim.SetTrigger("IsAttack");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            PlayerManager.Instance.status.DoDamage(enemy.status);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_CapsuleAttack : MonoBehaviour
{
    private Enemy enemy;
    [SerializeField] Collider col;

    public void InitAttack(Enemy parent)
    {
        this.enemy = parent;
        col.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            if (player == null) return;
            enemy.status.DoDamage(player.status);
        }
    }
}

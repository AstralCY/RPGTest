using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HandAttack : MonoBehaviour
{
    [SerializeField] private Transform attackPos;
    [SerializeField] private LineRenderer lineRenderer;
    private Enemy enemy;
    Player player;

    public void InitAttack(Enemy parent)
    {
        this.enemy = parent;
        player = PlayerManager.Instance.player;
    }

    public void AnimTrigger()
    {
        LookatTarget();
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, attackPos.position);
        lineRenderer.SetPosition(1, player.transform.position);
        enemy.status.DoDamage(player.status);
    }

    public void LookatTarget()
    {
        Vector3 targetPos = player.transform.position;
        targetPos.y = enemy.transform.position.y;
        enemy.transform.LookAt(targetPos);
    }

    public void Hide()
    {
        lineRenderer.enabled = false;
    }
}

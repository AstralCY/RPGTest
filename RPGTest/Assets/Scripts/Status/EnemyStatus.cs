using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : CharacterStatus
{
    [SerializeField] private PickableObject dropItemPrefab;
    private Action deathCallback;

    public void ResignDeathCallback(Action callback)
    {
        deathCallback = callback;
    }

    protected override void Die()
    {
        Inventory.Instance.GetRandomItem(dropItemPrefab, transform);
        deathCallback?.Invoke();
        deathCallback = null;
        Destroy(this.gameObject);
    }
}

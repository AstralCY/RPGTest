using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    public Status MaxHP;
    public Status MaxMP;
    public Status MaxEnergy;

    [SerializeField] private Status atk;

    [SerializeField] private int currentHP;
    [SerializeField] private int currentMP;
    [SerializeField] private int currentEnergy;
    [SerializeField] private CanvasBar bar;
    private bool barTure;

    protected virtual void Start()
    {
        currentHP = MaxHP.GetValue();
        currentMP = MaxMP.GetValue();
        currentEnergy = MaxEnergy.GetValue();
        barTure = bar != null;
        if (barTure)
        {
            bar.UpdateBar(BarType.HP, GetHPPct());
            bar.UpdateBar(BarType.MP, GetMPPct());
            bar.UpdateBar(BarType.Energy, GetEnergyPct());
        }
    }

    public virtual void DoDamage(CharacterStatus targetStatus)
    {
        int totalDamage = atk.GetValue();

        targetStatus.TakeDamage(totalDamage);
    }

    protected virtual void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (barTure)
        {
            bar.UpdateBar(BarType.HP, GetHPPct());
        }
        if (currentHP < 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {

    }

    public void AddAtk(int modifier) => atk.AddModifier(modifier);

    public void RemoveAtk(int modifier) => atk.RemoveModifier(modifier);

    public bool CanHeal()
    {
        return currentHP < MaxHP.GetValue() || currentMP < MaxMP.GetValue() || currentEnergy < MaxEnergy.GetValue();
    }

    public virtual void IncreaseHP(int value)
    {
        currentHP = Recovery(currentHP, value, MaxHP);
        if (barTure) bar.UpdateBar(BarType.HP, GetHPPct());
    }

    public virtual void IncreaseMP(int value)
    {
        currentMP = Recovery(currentMP, value, MaxMP);
        if (barTure) bar.UpdateBar(BarType.MP, GetMPPct());
    }

    public virtual void IncreaseEnergy(int value)
    {
        currentEnergy = Recovery(currentEnergy, value, MaxEnergy);
        if (barTure) bar.UpdateBar(BarType.Energy, GetEnergyPct());
    }

    private int Recovery(int currentValue, int recoverValue, Status maxValue)
    {
        return Math.Clamp(currentValue + recoverValue, 0, maxValue.GetValue());
    }

    private float GetPct(int current, Status max)
    {
        return (float)current / max.GetValue();
    }

    public virtual float GetHPPct()
    {
        return GetPct(currentHP, MaxHP);
    }
    public virtual float GetMPPct()
    {
        return GetPct(currentMP, MaxMP);
    }
    public virtual float GetEnergyPct()
    {
        return GetPct(currentEnergy, MaxEnergy);
    }
}
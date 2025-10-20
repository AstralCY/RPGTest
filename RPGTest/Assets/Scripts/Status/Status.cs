using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Status
{
    [SerializeField] private int baseValue = 0;

    public List<int> modifiers;

    public int GetValue()
    {
        int finalValue = baseValue;
        foreach (int modifier in modifiers)
        {
            finalValue += modifier;
        }
        return finalValue;
    }

    public void AddModifier(int modifier)
    {
        modifiers.Add(modifier);
    }

    public void RemoveModifier(int modifier)
    {
        modifiers.Remove(modifier);
    }
}
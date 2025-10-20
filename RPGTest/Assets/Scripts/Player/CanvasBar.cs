using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasBar : MonoBehaviour
{
    [SerializeField] private Slider HPBar;
    [SerializeField] private Slider MPBar;
    [SerializeField] private Slider EnergyBar;

    public void UpdateBar(BarType type, float value)
    {
        switch (type)
        {
            case BarType.HP:
                HPBar.value = value;
                break;
            case BarType.MP:
                MPBar.value = value;
                break;
            case BarType.Energy:
                EnergyBar.value = value;
                break;
        }
    }
}

public enum BarType
{
    HP, MP, Energy
}
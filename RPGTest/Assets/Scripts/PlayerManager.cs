using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public Player player;
    public PlayerStatus status;
    public GameObject currentWeaponInstance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ArmWeapon(Weapon weapon)
    {
        if (weapon.transform.parent != null)
        {
            currentWeaponInstance = GameObject.Instantiate(weapon.transform.parent.gameObject, player.transform);
            Weapon newWeapon = currentWeaponInstance.GetComponentInChildren<Weapon>();
            player.currentWeapon = newWeapon;
            status.AddAtk(weapon.GetValue(ItemData.ItemPropertyType.ATK));
        }
    }

    public void UnArmWeapon(Weapon weapon)
    {
        if (currentWeaponInstance == null) return;
        player.currentWeapon = null;
        status.RemoveAtk(weapon.GetValue(ItemData.ItemPropertyType.ATK));
        Destroy(currentWeaponInstance);

    }
    public void Restore(Healitem healitem)
    {
        if (status.CanHeal() == false) return;
        var itemProperty = healitem.itemData.itemProperty;

        foreach (var property in itemProperty)
        {
            switch (property.propertyType)
            {
                case ItemData.ItemPropertyType.HP:
                    status.IncreaseHP(property.value);
                    break;
                case ItemData.ItemPropertyType.MP:
                    status.IncreaseMP(property.value);
                    break;
                case ItemData.ItemPropertyType.Energy:
                    status.IncreaseEnergy(property.value);
                    break;
            };
        }
    }
}

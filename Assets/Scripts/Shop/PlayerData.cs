using System;
using System.Collections.Generic;
using Given.Manager;
using UnityEditor.Overlays;
using UnityEngine;

public static class PlayerData
{
    private static int gold = 21;
    public static int Gold { get { return gold; } set { gold = value; Save(); OnGoldUpdated?.Invoke();} }
    public static event Action OnGoldUpdated;
    public static event Action OnDiceUpdated;
    public static List<EDiceType> diceInventory = new();
    public static void Save()
    {
        PlayerPrefs.SetInt("Gold", gold);
    }

    public static void Load()
    {
        
    }

    public static void AddDice(ShopDice dice)
    {
        diceInventory.Add(dice.diceType);
        OnDiceUpdated?.Invoke();
    }
}

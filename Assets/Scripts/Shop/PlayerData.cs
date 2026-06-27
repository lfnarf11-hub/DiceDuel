using System;
using System.Collections.Generic;
using Given.Manager;

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
        string s = "";
        for (int i = 0; i < diceInventory.Count; i++)
        {
            s += diceInventory[i];
            if (i < diceInventory.Count - 1) s += ",";
        }
        PlayerPrefs.SetString("Dice", s);
        PlayerPrefs.Save();
    }

    [RuntimeInitializeOnLoadMethod]
    public static void Load()
    {
        gold = PlayerPrefs.GetInt("Gold", 999);
        string s = PlayerPrefs.GetString("Dice", "Four,Four,Six,Eight");
        Debug.Log($"Loading {s}");
        diceInventory.Clear();
        string[] dice = s.Split(',');
        for (int i = 0; i < dice.Length; i++)
        {
            if (EDiceType.TryParse(dice[i], out EDiceType diceType))
            {
                diceInventory.Add(diceType);
            }
            else
            {
                Debug.LogError($"failed to add dice {dice[i]}");
            }
        }
    }

    public static void AddDice(ShopDice dice)
    {
        diceInventory.Add(dice.diceType);
        OnDiceUpdated?.Invoke();
    }
    
    public static void Reset()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Load();
    }
}

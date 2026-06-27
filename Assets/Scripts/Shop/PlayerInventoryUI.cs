using System;
using Given.Manager;
using TMPro;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private DiceUI diceUIPrefab;
    [SerializeField] private Transform diceArea;
    [SerializeField] private TextMeshProUGUI goldText;
    private void Start()
    {
        UpdateGold();
        RebuildInventory();
        PlayerData.OnDiceUpdated += RebuildInventory;
        PlayerData.OnGoldUpdated += UpdateGold;
    }

    private void RebuildInventory()
    {
        for (int i = diceArea.childCount - 1; i >= 0; i--)
        {
            Destroy(diceArea.GetChild(i).gameObject);
        }

        foreach (EDiceType diceType in PlayerData.diceInventory)
        {
            var dice = Instantiate(diceUIPrefab, diceArea);
            dice.diceType = diceType;
        }
    }

    private void OnDestroy()
    {
        PlayerData.OnDiceUpdated -= RebuildInventory;
        PlayerData.OnGoldUpdated -= UpdateGold;
    }

    private void UpdateGold()
    {
        goldText.text = PlayerData.Gold.ToString();
    }

    [ContextMenu("Cheat Gold")]
    public void CheatGold()
    {
        PlayerData.Gold = 1209201;
    }
    [ContextMenu("ClearDice")]
    public void ClearDice()
    {
        PlayerData.diceInventory.Clear();
        PlayerData.Save();
    }
    [ContextMenu("CheatReset")]
    public void CheatReset()
    {
        PlayerData.Reset();
    }
}


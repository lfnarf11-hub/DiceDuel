using System;
using Given.Manager;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private DiceUI diceUIPrefab;
    private void Start()
    {
        RebuildInventory();
        PlayerData.OnDiceUpdated += RebuildInventory;
    }

    private void RebuildInventory()
    {
        while (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
            
        }

        foreach (EDiceType diceType in PlayerData.diceInventory)
        {
            var dice = Instantiate(diceUIPrefab, transform);
            dice.diceType = diceType;
        }
    }

    private void OnDestroy()
    {
        PlayerData.OnDiceUpdated -= RebuildInventory;
    }
}

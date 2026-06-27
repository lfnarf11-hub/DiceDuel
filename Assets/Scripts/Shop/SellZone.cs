using System;
using Given.Manager;
using UnityEngine;

public class SellZone : MonoBehaviour
{
    private void OnTransformChildrenChanged()
    {
        for (int i = transform.childCount - 1; i >= 1; i--)
        {
            if (transform.GetChild(i).TryGetComponent(out DiceUI dice))
            {
                Destroy(dice.gameObject);
                PlayerData.diceInventory.Remove(dice.diceType);
                
                PlayerData.Gold += Mathf.RoundToInt(DataManager.Instance.GetDice(dice.diceType).price * 0.707f);
            }
        }
    }
}

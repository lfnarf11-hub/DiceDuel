using UnityEngine;

public class GameDriver : MonoBehaviour
{
    [SerializeField] BaseCharacter leftCharacter;
    [SerializeField] BaseCharacter rightCharacter;
    BattleManager battleManager;
    void Start()
    {
        battleManager = new (leftCharacter, rightCharacter);
        BeginBattle();
    }
    void BeginBattle()
    {
        battleManager.BeginBattle();
    }
}

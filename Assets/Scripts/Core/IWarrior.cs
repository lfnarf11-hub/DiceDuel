using Cysharp.Threading.Tasks;
using Given.Manager;
using UnityEngine;

public interface IWarrior
{
    void TakeDamage(int amount);

    void RoundStart();
    UniTask DoTurn();
    UniTask RollDice();

    void EndRound();
    bool IsAlive();
    EDiceType[] GetBattleDice();
    void Initialize();
    int Shield { get; set; }
    void Heal(int dataValue);
    IWarrior target { get; set; }
}

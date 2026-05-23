using Cysharp.Threading.Tasks;
using Given.Manager;
using UnityEngine;

public interface IWarrior
{
    void TakeDamage();

    void RoundStart();
    UniTask DoTurn();
    void EndRound();
    bool IsAlive();
    EDiceType[] GetBattleDice();
    void Initialize();
}

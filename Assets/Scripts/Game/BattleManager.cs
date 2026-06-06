using Cysharp.Threading.Tasks;
using Given.Manager;
using UnityEngine;

public class BattleManager
{
    private IWarrior leftWarrior;
    private IWarrior rightWarrior;
    public BattleManager(IWarrior leftWarrior, IWarrior rightWarrior)
    {
        this.leftWarrior = leftWarrior;
        this.rightWarrior = rightWarrior;
    }

    public void BeginBattle()
    {
        PlayBattle();
    }

    public void EndBattle()
    {
        
    }

    private async UniTaskVoid PlayBattle()
    {
        leftWarrior.target = rightWarrior;
        rightWarrior.target = leftWarrior;
        leftWarrior.Initialize();
        rightWarrior.Initialize();
        while (BattleIsRunning())
        {
            leftWarrior.RoundStart();
            rightWarrior.RoundStart();
            await UniTask.WhenAll(leftWarrior.DoTurn(), rightWarrior.DoTurn());
            
            leftWarrior.EndRound();
            rightWarrior.EndRound();
            await UniTask.Delay(5000);
        }
    }

    private bool BattleIsRunning()
    {   
        return leftWarrior.IsAlive() && rightWarrior.IsAlive();
    }
}

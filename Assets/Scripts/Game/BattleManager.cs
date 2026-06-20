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
        
        Debug.Log("Game Start");
        
        while (BattleIsRunning())
        {
            Debug.Log("Round start");

            leftWarrior.RoundStart();
            rightWarrior.RoundStart();
            
            Debug.Log("Do Turn");
            
            await UniTask.WhenAll(leftWarrior.DoTurn(), rightWarrior.DoTurn());
           
            Debug.Log("Roll Dice");
            
            await UniTask.WhenAll(leftWarrior.RollDice(), rightWarrior.RollDice());
            Debug.Log("End Turn");

            leftWarrior.EndRound();
            rightWarrior.EndRound();
            Debug.Log("Round Complete");

        }
        Debug.Log($"Battle has ended. Left is alive? {leftWarrior.IsAlive()}, Right is alive? {rightWarrior.IsAlive()}");

    }

    private bool BattleIsRunning()
    {   
        return leftWarrior.IsAlive() && rightWarrior.IsAlive();
    }
}

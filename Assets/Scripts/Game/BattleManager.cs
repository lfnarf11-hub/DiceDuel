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
            
            Debug.Log("Use abilities");
            
            //To fix abilities, we can use a 'QUEUE' system and instead of doing UseAbilities, we get the next ability.
            //If the ability is null we ignore it other goes first. if both null, then proceed.
            //If the ability is higher priority we go first
            //else enemy goes first
            //If the ability is same, if we had the lower role, we go first.
            //else enemy goes first.
            
            //****After any player goes, check if the battle had ended.****//
            
            await UniTask.WhenAll(leftWarrior.UseAbilities(), rightWarrior.UseAbilities());
            
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

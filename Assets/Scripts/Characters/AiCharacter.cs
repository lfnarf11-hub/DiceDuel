using System;
using Cysharp.Threading.Tasks;
using Given.Manager;
using UnityEngine;
using Random = UnityEngine.Random;

public class AiCharacter : BaseCharacter 
{
    [SerializeField] private EAIType aiType;
    public override async UniTask DoTurn()
    {
        if (target is PlayerCharacter player)
            await UniTask.WaitWhile(player.TurnRunning);
        AssignDice();
        await RollDice();
        await UniTask.Delay(3000);
    }

    AbilityData[] AssignDice()
    {
        switch (aiType)
        {
            case EAIType.Random:
                return ChooseAbilityRandomly();
             
            case EAIType.Offensive:
                return ChooseOffensiveAbilities();
               
            case EAIType.SupeUltraNoobMode:
                //do nothing
                break;
        }
        return Array.Empty<AbilityData>();
    }

    private AbilityData[] ChooseAbilityRandomly()
    {
        activeAbilities.Shuffle();
        diceToRoll.Shuffle();
        AbilityData[] data = new AbilityData[activeAbilities.Length];
        int i = 0;
        for (var index = 0; index < activeAbilities.Length; index++)
        {
            var ability = activeAbilities[index];
            int diceCount = Random.Range(0, base.diceToRoll.Length - i);
            EDiceType[] dice = new EDiceType[diceCount];
            for (int j = i; j < i + diceCount; j++)
            {
                
            }
            data[index] = new(ability, target, dice, 0);

        }
        return data;
    }

    private AbilityData[] ChooseOffensiveAbilities()
    {
        throw new NotImplementedException();
    }

    public enum EAIType
    {
        Random,
        Offensive,
        SupeUltraNoobMode
    }
}

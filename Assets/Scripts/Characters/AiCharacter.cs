using System;
using Cysharp.Threading.Tasks;
using Given.Manager;
using UnityEngine;
using Random = UnityEngine.Random;

public class AiCharacter : BaseCharacter 
{
    [SerializeField] private EAIType aiType;
    [field:SerializeField] protected override EDiceType[] diceToRoll { get; set; }

    
    
    public override async UniTask DoTurn()
    {
        if (target is PlayerCharacter player)
            await UniTask.WaitWhile(player.TurnRunning);

        var assigned = AssignDice();

        string counts = "";
        foreach (var a in assigned)
            counts += a.dice.Length + ",";
        Debug.Log($"AI assigned {assigned.Length} abilities, dice counts: {counts}", gameObject);

        abilities.AddRange(assigned);
        Debug.Log("AI Turn Complete", gameObject);
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
        for (int index = 0; index < activeAbilities.Length; index++)
        {
            var ability = activeAbilities[index];
            int remaining = diceToRoll.Length - i;
            bool isLast = index == activeAbilities.Length - 1;
            int diceCount = isLast ? remaining : Random.Range(0, remaining + 1);
            EDiceType[] dice = new EDiceType[diceCount];
            for (int j = i; j < i + diceCount; j++)
                dice[j - i] = diceToRoll[j];
            i += diceCount;
            data[index] = new(ability, this, dice, 0);
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

using Cysharp.Threading.Tasks;
using Given.Manager;
using UnityEngine;

public abstract class AbilityBase : ScriptableObject
{
    [SerializeField] private Sprite icon;
    public Sprite Icon => icon;
    [SerializeField, ColorUsage(false, true)] private Color color;
    public Color Color => color;
    [SerializeField] private bool costsMaxStamina;
    public bool CostsMaxStamina => costsMaxStamina;
    [SerializeField] private int abilityPriority;
    public int AbilityPriority => abilityPriority;

    public UniTask StartAbility(AbilityData data, IWarrior enemy)
    {
        if(data.value <= 0) return UniTask.CompletedTask;
        return StartAbilityImplementation(data, enemy);
    }
    public abstract UniTask StartAbilityImplementation(AbilityData data, IWarrior enemy);
}

public struct AbilityData
{
    public readonly AbilityBase abilityBase;
    public readonly IWarrior warrior;
    public readonly EDiceType[] dice;
    public readonly int value;

    public AbilityData(AbilityBase abilityBase, IWarrior warrior, EDiceType[] dice, int value)
    {
        this.abilityBase = abilityBase;
        this.warrior = warrior;
        this.dice = dice;
        this.value = value;
    }
}
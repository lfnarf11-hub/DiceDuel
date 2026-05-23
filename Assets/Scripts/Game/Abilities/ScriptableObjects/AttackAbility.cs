using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackAbility", menuName = "Abilities/AttackAbility")]
public class AttackAbility : AbilityBase
{
    public override UniTask StartAbilityImplementation(AbilityData data, IWarrior enemy)
    {
        throw new System.NotImplementedException();
    }
}

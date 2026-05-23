using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "DefendAbility", menuName = "Abilities/DefendAbility")]
public class DefendAbility : AbilityBase
{
    public override UniTask StartAbilityImplementation(AbilityData data, IWarrior enemy)
    {
        throw new System.NotImplementedException();
    }
}

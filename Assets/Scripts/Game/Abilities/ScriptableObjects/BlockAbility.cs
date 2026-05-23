using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockAbility", menuName = "Abilities/BlockAbility")]
public class BlockAbility : AbilityBase
{
    public override UniTask StartAbilityImplementation(AbilityData data, IWarrior enemy)
    {
        throw new System.NotImplementedException();
    }
}

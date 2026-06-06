using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    [SerializeField] AbilityManager _abilityManager;
    public override async UniTask DoTurn()
    {
        await RollDice();
    }

    public override void Initialize()
    {
        base.Initialize();
        _abilityManager.RegenerateAbilities(activeAbilities);
        _abilityManager.GenerateDiceUI(diceToRoll);
    }

    public async Task TurnComplete()
    {
        throw new System.NotImplementedException();
    }
}

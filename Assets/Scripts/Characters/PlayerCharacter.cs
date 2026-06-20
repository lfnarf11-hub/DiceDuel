using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    [SerializeField] AbilityManager _abilityManager;
    bool IsTurnRunning = false;
    [SerializeField] Canvas Button;

    public override async UniTask DoTurn()
    {
        IsTurnRunning = true;
        Button.enabled = true;
        await UniTask.WaitWhile(TurnRunning);
        abilities = _abilityManager.retrieveData(this);
        Debug.Log("Player Turn Complete", gameObject);

    }

    public bool TurnRunning()
    {
        return IsTurnRunning;
    }

    public override void Initialize()
    {
        base.Initialize();
        _abilityManager.RegenerateAbilities(activeAbilities);
        _abilityManager.GenerateDiceUI(diceToRoll);
    }

    public void CompleteTurn()
    {
        Button.enabled = false;
        IsTurnRunning = false;

    }
}

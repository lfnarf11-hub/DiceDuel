using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Given.Manager;
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

    protected override EDiceType[] diceToRoll { get; set; }

    public override void Initialize()
    {
        diceToRoll = PlayerData.diceInventory.ToArray();
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

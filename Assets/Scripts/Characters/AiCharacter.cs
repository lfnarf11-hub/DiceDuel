using Cysharp.Threading.Tasks;
using UnityEngine;

public class AiCharacter : BaseCharacter 
{
    public override async UniTask DoTurn()
    {
        await RollDice();
        await UniTask.Delay(3000);
    }
}

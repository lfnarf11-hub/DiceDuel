using Cysharp.Threading.Tasks;
using Game.Battle.Character;
using Given.Manager;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour, IWarrior
{
    [SerializeField] private int diceNumber;
    [SerializeField] private float maxStamina;
    [SerializeField] private float maxHealth;
    [SerializeField] protected EDiceType[] diceToRoll;
    [SerializeField][ColorUsage(true, true)] private Color glowColor = new (0, 1, 0.85f);
    [SerializeField][ColorUsage(true, true)] private Color textColor = new (1, 0, 0.25f);
    [SerializeField] protected AbilityBase[] activeAbilities;
    private float _currentStamina;
    private float _currentHealth;
    private float _currentMaxStamina;

    
    public virtual void Initialize()
    {
        _currentHealth = maxHealth;
        _currentStamina = maxStamina;
        _currentMaxStamina = maxStamina;
    }

    public int Shield { get; set; }
    public void Heal(int dataValue)
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int amount)
    {
        
    }




    public void EndRound()
    {
   
    }

    public bool IsAlive()
    {
        return true;
    }

    public EDiceType[] GetBattleDice()
    {
        return diceToRoll;
    }

    public void RoundStart()
    {
        
    }

    public abstract UniTask DoTurn();
    
    [ContextMenu("RollDice")]
    protected async UniTask RollDice()
    {
        UniTask<int>[] tasks = new UniTask<int>[diceToRoll.Length];
        for (int i = 0; i < diceToRoll.Length; i++)
        {
           Dice dice = DiceManager.Instance.CreateDice(diceToRoll[i], transform.position.x < 0, glowColor, textColor);
           tasks[i] = dice.Roll(dice.transform.forward);
        }
        int[] nums = await UniTask.WhenAll(tasks);
        int sum = 0;
        for (int i = 0; i < nums.Length; i++)
        {

            sum += nums[i];
        }
        Debug.Log($"{name} Rolled a {sum}", gameObject);
        Debug.Log($"{name} Mean {nums.AverageMean()}");
        Debug.Log($"{name} Median {nums.AverageMedian()}");
        Debug.Log($"{name} Mode {nums.AverageMode()}");
        GraphManager.Instance?.RegisterRoll(GetBattleDice(), sum);
    }

    void RoundEnd()
    {
        
    }
}





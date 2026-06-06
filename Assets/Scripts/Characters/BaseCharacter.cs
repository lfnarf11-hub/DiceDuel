using Cysharp.Threading.Tasks;
using Game.Battle.Character;
using Given.Manager;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.LowLevelPhysics2D;

public abstract class BaseCharacter : MonoBehaviour, IWarrior
{
    [SerializeField] private int diceNumber;
    [SerializeField] private int maxStamina;
    [SerializeField] private int maxHealth;
    public int MaxHealth => maxHealth;
    [SerializeField] private int maxShield;
    public int MaxShield => maxShield;
    [SerializeField] protected EDiceType[] diceToRoll;
    [SerializeField][ColorUsage(true, true)] private Color glowColor = new (0, 1, 0.85f);
    [SerializeField][ColorUsage(true, true)] private Color textColor = new (1, 0, 0.25f);
    [SerializeField] protected AbilityBase[] activeAbilities;
    private int _currentStamina;
    private int _currentHealth;
    public int CurrentHealth => _currentHealth;
    private int _currentMaxStamina;
    
    [Header("Audio")] 
    protected AudioSource audioSource;
    [SerializeField] private AudioResource damageSound;
    [SerializeField] private AudioResource healSound;
    [SerializeField] private AudioResource blockSound;
    
    
    public virtual void Initialize()
    {
        audioSource = GetComponent<AudioSource>();
        _currentHealth = maxHealth;
        _currentStamina = maxStamina;
        _currentMaxStamina = maxStamina;
    }

    public int Shield { get; set; }
    public void Heal(int dataValue)
    {
        audioSource.resource = healSound;
        audioSource.Play();
        
        _currentHealth += dataValue;
        if (_currentHealth > maxHealth) _currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0) return;
        int damage = Shield - amount;
        if (damage >= 0)
        {
            //play sound or particles
            audioSource.resource = blockSound;
            audioSource.Play();
            
            Shield -= amount;
        }
        else
        {
            audioSource.resource = damageSound;
            audioSource.Play();
            
            _currentHealth = _currentHealth + Shield - amount;
            Shield = 0;
            if (!IsAlive())
            {
                Die();
            }
        }
        
        
      
    }

    public bool IsAlive()
    {
        return _currentHealth > 0;
    }

    protected virtual void Die()
    {
        //something
        Debug.Log("WE DIED", gameObject);
    }
    
    

    public void EndRound()
    {
   
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





using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Game.Battle.Character;
using Given.Manager;
using UnityEngine;
using UnityEngine.Audio;

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
    public List<AbilityData> abilities = new List<AbilityData>();

    
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

    public IWarrior target { get; set; }

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
    public async UniTask RollDice ()
    {
        for (int j = 0; j < abilities.Count; ++ j)
        {
            AbilityData Ability = abilities[j];


            UniTask<int>[] tasks = new UniTask<int>[Ability.dice.Length];

            for (int i = 0; i < Ability.dice.Length; i += 1)
            {
                Dice dice = DiceManager.Instance.CreateDice(Ability.dice[i], transform.position.x < 0, Ability.abilityBase.Color, textColor);

                tasks[i] = dice.Roll(dice.transform.forward);

            }

            int[] num = await UniTask.WhenAll(tasks);

            int sum = 0;

            for (int i = 0; i < num.Length; i += 1)
            {
                sum += num[i];

            }
            Debug.Log($"Player Rolled for {Ability.abilityBase.name} with {sum}", gameObject);
            //Debug.Log($"Mean {num.AverageMean()}" );
            //Debug.Log($"Medium {num.AverageMedian()}");
            //Debug.Log($"Mode {num.AverageMode()}");
            GraphManager.Instance?.RegisterRoll(Ability.dice, sum);
            Ability.value = sum;
            abilities[j] = Ability;
           
        }
        abilities =  abilities.OrderBy(a => a.abilityBase.AbilityPriority).ToList();

        for (int j = 0; j < abilities.Count; ++j)
        {
            AbilityData Ability = abilities[j];
            await Ability.abilityBase.StartAbility(Ability, target);
        }
        abilities.Clear();
    }
    
    void RoundEnd()
    {
        
    }
}





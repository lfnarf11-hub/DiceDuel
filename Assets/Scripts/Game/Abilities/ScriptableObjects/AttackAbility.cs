using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackAbility", menuName = "Abilities/AttackAbility")]
public class AttackAbility : AbilityBase
{
    [Header("Animation")]
    [SerializeField] private string animationID = "Attack1";
    [SerializeField] private AnimationClip animation;
    [SerializeField] private int eventIndex = 1;
    [SerializeField] private float attackDist = 1f;
    [SerializeField] private float moveTime = 0.2f;

    [Header("Combos")]
    [SerializeField] int comboThreshold;
    [SerializeField] AttackAbility attackCombo;
    
    public override async UniTask StartAbilityImplementation(AbilityData data, IWarrior enemy)
    {
        if(data.warrior is not BaseCharacter player || enemy is not BaseCharacter enemyCharacter) {
            enemy.TakeDamage(data.value);
            return;
        }

        Animator currentAnimator = player.GetComponentInChildren<Animator>();
        if (currentAnimator is null)
        {
            Debug.LogError($"{nameof(currentAnimator)} is null");
            enemy.TakeDamage(data.value);
            return;
        }
        Vector2 playerPos =  player.transform.position;
        Vector2 enemyPos = enemyCharacter.transform.position;
        await MoveTo(player.transform, enemyCharacter.transform, -(enemyPos-playerPos).normalized*attackDist, moveTime);
        await Combo(currentAnimator, data.value, enemyCharacter, player);
        await UniTask.Delay(300);
        await MoveTo(player.transform, enemyPos - (enemyPos-playerPos).normalized*attackDist, playerPos, moveTime*0.8f);

    }

    private async UniTask Combo(Animator currentAnimator, int data, BaseCharacter enemyCharacter, BaseCharacter player)
    {
        currentAnimator.SetTrigger(animationID);
        int duration = (int)(animation.events[eventIndex].time*1000);
        int totalTime = (int)(animation.length*1000);
        await UniTask.Delay(duration);
        int damage = data;
        if (attackCombo)
        {
            damage = Mathf.Min(damage, comboThreshold);
        }
        enemyCharacter.TakeDamage(damage);
        await UniTask.Delay(totalTime - duration + 300);
        if (attackCombo && damage >= comboThreshold)
        {
            await attackCombo.Combo(currentAnimator, data - comboThreshold, enemyCharacter, player);
        }
    }

    private async UniTask MoveTo(Transform playerTransform, Vector3 playerPos, Vector3 enemyPos, float f)
    {
        if (playerPos == enemyPos) return;
        float currentTime = 0f;
        while (currentTime < f)
        {
            float t = currentTime / f;
            playerTransform.position = Vector3.Lerp(playerPos, enemyPos, t);
            currentTime += Time.deltaTime;
            await UniTask.Yield();
        }
        playerTransform.position = Vector3.Lerp(playerPos, enemyPos, 1f);
        
    }
    
    //OVERLOAD: A secondary version that allows us to move to a moving target
    private async UniTask MoveTo(Transform myTransform, Transform targetTransform, Vector3 offset, float f)
    {
        Vector3 playerPos = myTransform.position;
        if (playerPos == targetTransform.position + offset) return;
        float currentTime = 0f;
        while (currentTime < f)
        {
            float t = currentTime / f;
            // We cannot cache the position and must use targetTransform.position as it is moving every frame
            myTransform.position = Vector3.Lerp(playerPos, targetTransform.position + offset, t); 
            currentTime += Time.deltaTime;
            await UniTask.Yield();
        }
        myTransform.position = Vector3.Lerp(playerPos, targetTransform.position + offset, 1f);
        
    }
}

using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "HealAbility", menuName = "Abilities/HealAbility")]
public class HealAbility : AbilityBase
{
    [SerializeField] private ParticleSystem particles;
    
    public override UniTask StartAbilityImplementation(AbilityData data, IWarrior enemy)
    {
        data.warrior.Heal(data.value);
        if (data.warrior is BaseCharacter player)
        {
            ParticleSystem ps = Instantiate(particles, player.transform.position, Quaternion.identity);
            Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
            return UniTask.Delay((int)(ps.main.duration * 1000));
        }
        return UniTask.CompletedTask;
    }
}

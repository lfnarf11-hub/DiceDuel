using System;
using System.Collections.Generic;
using Given.Manager;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    private AbilityBase[] activeAbilities;
    [SerializeField] private Transform abilityParent;
    [SerializeField] private AbilityUI abilityPrefab;
    List<AbilityUI> activeAbilitiesList = new();
    [SerializeField] private DiceUI dicePrefab;
    [SerializeField] private Transform diceParent;

    public void CreateUI()
    {
        foreach (AbilityBase ability in activeAbilities)
        {
            AbilityUI activeAbility = Instantiate(abilityPrefab, abilityParent);
            activeAbility.SetAbility(ability);
            activeAbilitiesList.Add(activeAbility);
        }
    }

    public void RegenerateAbilities(AbilityBase[] activeAbilities)
    {
        foreach (AbilityUI abilityUI in activeAbilitiesList)
            Destroy(abilityUI.gameObject);
        activeAbilitiesList.Clear();
        this.activeAbilities = activeAbilities;
        CreateUI();
    }

    public void GenerateDiceUI(EDiceType[]  diceTypes)
    {
        foreach (EDiceType diceType in diceTypes)
        {
            DiceUI diceUI = Instantiate(dicePrefab, diceParent);
            diceUI.diceType = diceType;
        }
    }
}

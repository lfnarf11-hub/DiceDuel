using System;
using Given.Manager;
using UnityEngine;
using UnityEngine.UI;

public class DiceUI : MonoBehaviour

{
    DragAndDropObject dadObject; 
    public EDiceType diceType { get => _diceType;
        set { _diceType = value; icon.sprite = DataManager.Instance.DiceSprites[(int) _diceType]; }
    }
    private EDiceType _diceType;
    [SerializeField] private Image icon;
    private AbilityUI current;
    

    private void Awake()
    {
        dadObject = GetComponent<DragAndDropObject>();
        
    }

    private void OnEnable()
    {
        dadObject.OnDropZoneChanged += UpdateAbility;
    }

    private void OnDisable()
    {
        dadObject.OnDropZoneChanged -= UpdateAbility;

    }

    private void UpdateAbility(DropZone obj)
    {
        current?.RemoveDice(diceType);
        current = obj.GetComponentInParent<AbilityUI>();
        
        if (current)
        {
            current.AddDice(diceType);
        }
    }
}

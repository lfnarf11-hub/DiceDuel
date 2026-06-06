using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] private BaseCharacter owner;
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI shieldText;

    private void LateUpdate()
    {
        healthBar.fillAmount = (float) owner.CurrentHealth / owner.MaxHealth;
        shieldText.text = owner.Shield.ToString(); 
    }
}

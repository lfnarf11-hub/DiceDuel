using System;
using System.Collections.Generic;
using Given.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI text;
    [field: SerializeField] public AbilityBase currentAbility  { get; private set; }
    private int low, high;
    public int Low { get => low;
        private set
        {
            low = value;
            text.text  = GenerateFancyText(low, high);
        }
    }
    public int High { get => high;  
        private set {
            high = value;
            text.text  = GenerateFancyText(low, high);
        }
    }
    public readonly List<EDiceType> Dice = new();
    
    public void SetAbility(AbilityBase ability)
    {
        currentAbility = ability;
        icon.sprite = ability.Icon;
    }

    public void AddDice(EDiceType dice)
    {
        Dice.Add(dice);
        Low += DataManager.DiceValues[dice].Low;
        High += DataManager.DiceValues[dice].High;
    }
    public void RemoveDice(EDiceType dice)
    {
        Dice.Remove(dice);
        Low -= DataManager.DiceValues[dice].Low;
        High -= DataManager.DiceValues[dice].High;
    }
    public void ClearDice()
    {
        Dice.Clear();
        Low = 0;
        High = 0;
    }
    private void Awake()
    {
        ClearDice();
    }
    //Generated with chatGPT, optional way to make text fancier
  #region GPTFancyText

  private const int ModerateValue = 30;
    private const int HighValue = 100;
    private const float MinFontSize = 16f;
    private const float MaxFontSize = 24f;

    private static readonly Color HighNegativeValueColor = new Color(0.5f, 0f, 0f); // Dark Red (High Negative)
    private static readonly Color NegativeValueColor = new Color(1f, 0.6f, 0.6f); // Pale Red (Negative)
    private static readonly Color LowValueColor = new Color(1f, 1f, 0.7f); // Pale Yellow (Low Value)
    private static readonly Color ModerateValueColor = new Color(0.6f, 1f, 0.6f); // Pale Green (Moderate Value)
    private static readonly Color HighValueColor = new Color(0f, 0.2f, 0.5f); // Dark Blue (High Value)

    private string GenerateFancyText(int low, int high)
    {
        // If low and high values are the same, return just one formatted value
        if (low == high)
        {
            return Format(low);
        }
        // Otherwise, return a range with both formatted values
        return $"{Format(low)} {Format((low + high) / 2, "~")} {Format(high)}";
    }

    // Helper method to determine the color based on the value
    string GetColor(int value)
    {
        // For values less than -100, we use HighNegativeValueColor
        if (value < -HighValue)
        {
            return ColorToHex(HighNegativeValueColor);
        }

        // For values between -100 and -25, interpolate from HighNegativeValueColor to NegativeValueColor
        if (value < -ModerateValue)
        {
            float t = Mathf.InverseLerp(-HighValue, -ModerateValue, value); // Normalize between -100 and -25
            return ColorToHex(Color.Lerp(HighNegativeValueColor, NegativeValueColor, t));
        }

        // For values between -25 and 25, interpolate from NegativeValueColor to LowValueColor
        if (value <= ModerateValue)
        {
            float t = Mathf.InverseLerp(-ModerateValue, ModerateValue, value); // Normalize between -25 and 25
            return ColorToHex(Color.Lerp(NegativeValueColor, LowValueColor, t));
        }

        // For values between 25 and 100, interpolate from LowValueColor to ModerateValueColor
        if (value <= HighValue)
        {
            float t = Mathf.InverseLerp(ModerateValue, HighValue, value); // Normalize between 25 and 100
            return ColorToHex(Color.Lerp(LowValueColor, ModerateValueColor, t));
        }

        // For values greater than 100, interpolate from ModerateValueColor to HighValueColor
        return ColorToHex(Color.Lerp(ModerateValueColor, HighValueColor, Mathf.InverseLerp(HighValue, float.MaxValue, value)));

    }

    // Helper method to determine the font size based on the value
    float GetFontSize(int value)
    {
        // Scale font size: 6 for value 0, up to 15 for value 100
        float size = Mathf.Lerp(MinFontSize, MaxFontSize, Mathf.Clamp01((float)Mathf.Abs(value) / HighValue));
        return size;
    }

    // Method to format the value with the appropriate color and font size for TMP
    string Format(int value)
    {
        // Calculate font size
        float fontSize = GetFontSize(value);

       

        // Apply size, color, and vertical offset to the value
        return $"<align=center><size={fontSize}><color={GetColor(value)}>{value}</color></size></align>";
    }

    // Method to format the value with the appropriate color and font size for TMP
    string Format(int value, string text)
    {
        // Calculate font size
        float fontSize = GetFontSize(value);

   

        // Apply size, color, and vertical offset to the value
        return $"<align=center><size={fontSize}><color={GetColor(value)}>{text}</color></size></align>";
    }

    private string ColorToHex(Color color)
    {
        // Convert the RGB color values to a hex string for TextMesh Pro
        int r = (int)(color.r * 255);  // Convert red component to range [0, 255]
        int g = (int)(color.g * 255);  // Convert green component to range [0, 255]
        int b = (int)(color.b * 255);  // Convert blue component to range [0, 255]

        // Return the hex string, ensuring the format is #RRGGBB
        return $"#{r:X2}{g:X2}{b:X2}";
    }

  #endregion                                                                                                                                                                                                            
}

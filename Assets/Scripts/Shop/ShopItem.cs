
using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
 public Button purchaseButton;
 private int price;
 public TextMeshProUGUI priceText;
 public Item myItem;
 public Image itemImage;
 private void Start()
 {
  SetPrice(myItem.price);
  PlayerData.OnGoldUpdated += UpdateButton;
  purchaseButton.onClick.AddListener(Purchase);
  itemImage.sprite = myItem.icon;
 }

 public void SetPrice(int newPrice)
 {
   price = newPrice;
   priceText.text = price.ToString();
   purchaseButton.interactable = (PlayerData.Gold >= price);
 }

 private void UpdateButton()
 {
     purchaseButton.interactable = (PlayerData.Gold >= price && PlayerData.diceInventory.Count < 12);
 }

 private void OnDestroy()
 {
     PlayerData.OnGoldUpdated -= UpdateButton;
 }

 private void Purchase()
 {
     
     if (myItem is ShopDice dice)
     {
         PlayerData.AddDice(dice);
     }
     PlayerData.Gold -= price;
 }
}

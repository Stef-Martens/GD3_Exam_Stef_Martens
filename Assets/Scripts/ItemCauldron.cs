using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCauldron : MonoBehaviour
{
    public string[] IngredientsNames;
    public string Info;

    public BaseAbility Ability;

    bool IsBought = false;


    public void BuyPower()
    {
        if (!IsBought)
        {
            bool canBuy = false;
            foreach (var item in IngredientsNames)
            {
                if (FindObjectOfType<Manager>().Ingredients.Contains(item))
                    canBuy = true;
                else
                {
                    canBuy = false;
                    break;
                }

            }

            if (canBuy)
            {
                foreach (var item in IngredientsNames)
                {
                    FindObjectOfType<Manager>().Ingredients.Remove(item);
                }
                FindObjectOfType<Manager>().AddInventory(Ability);
                IsBought = true;
                FindObjectOfType<SoundManager>().PlayBrewingSound();
            }
        }


    }
}

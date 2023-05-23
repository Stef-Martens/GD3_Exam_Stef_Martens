using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCauldron : MonoBehaviour
{
    public string[] IngredientsNames;
    public string Info;

    public BaseAbility Ability;

    bool IsBought = false;

    int counter = 0;


    public void BuyPower()
    {
        counter = 0;
        if (!IsBought)
        {
            foreach (var item in IngredientsNames)
            {
                if (FindObjectOfType<Manager>().Ingredients.Contains(item))
                    counter++;
            }

            if (counter == IngredientsNames.Length)
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

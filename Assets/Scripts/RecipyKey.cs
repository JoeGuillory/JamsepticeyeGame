using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

class RecipeKey : IEquatable<RecipeKey>
{
    private readonly List<PotionType> ingredients;

    public RecipeKey(PotionType a, PotionType b, PotionType c)
    {
        ingredients = new List<PotionType> { a, b, c };
        ingredients.Sort(); // sort to ensure order doesn't matter
    }

    public override bool Equals(object obj) => Equals(obj as RecipeKey);

    public bool Equals(RecipeKey other)
    {
        if (other == null) return false;
        for (int i = 0; i < 3; i++)
        {
            if (this.ingredients[i] != other.ingredients[i])
                return false;
        }

        return true;
    }

    public override int GetHashCode()
    {
        int hash = 17;
        foreach (var ingredient in ingredients)
        {
            hash = hash * 31 + ingredient.GetHashCode();
        }
        return hash;
    }
}
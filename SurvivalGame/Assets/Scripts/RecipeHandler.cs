using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class RecipeHandler : MonoBehaviour {
    private List<Recipe> _craftingRecipes = new List<Recipe>();

    private void Awake() {
        Type stick = new Stick().GetType();
        Type none = new None().GetType();
        Type stone = new Stone().GetType();

        Type[,] Crafted_AxeRecipe;
        Crafted_AxeRecipe = new Type[,] {
        { stone, stone, stone },
        { none, stick, none },
        { none, stick, none }
        };
        _craftingRecipes.Add(new Recipe("Crafted_Axe", Crafted_AxeRecipe));





    }


}


public class Recipe {
    public string RecipeName { get; set; }
    public bool Shaped { get; set; }

    private Type[,] _schematic = new Type[3, 3];
    public Type[,] Schematic {
        get { return _schematic; }
        set { _schematic = Schematic; }
    }

    public Recipe(string name, Type[,] schematic) {
        RecipeName = name;
        Schematic = schematic;
    }
}



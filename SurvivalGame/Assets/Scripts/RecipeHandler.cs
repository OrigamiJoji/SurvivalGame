using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class RecipeHandler : MonoBehaviour {
    static RecipeHandler _instance;
    public static RecipeHandler Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<RecipeHandler>();
            }
            return _instance;
        }
    }


    private List<Recipe> _recipeList = new List<Recipe>();
    public List<Recipe> RecipeList {
        get {
            return _recipeList;
        }
        private set {
            _recipeList = RecipeList;
        }
    }

    private void Awake() {
        Type stick = new Stick().GetType();
        Type none = new None().GetType();
        Type stone = new Stone().GetType();

        Type[,] Crafted_AxeRecipe;
        Crafted_AxeRecipe = new Type[,] {
        { none, stone, stone },
        { none, stick, stone },
        { none, stick, none }
        };
        RecipeList.Add(new Recipe("Crafted_Axe", Crafted_AxeRecipe));
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
    public Item _Product;

    public Recipe(string name, Type[,] schematic) {
        RecipeName = name;
        Schematic = schematic;
    }
}



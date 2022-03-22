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


    static List<Recipe> _recipeList = new List<Recipe>();
    public static List<Recipe> RecipeList {
        get {
            return _recipeList;
        }
        private set {
            _recipeList = RecipeList;
        }
    }

    private void Awake() {
        Type stick = new Stick().ItemType();
        Type none = new None().ItemType();
        Type stone = new Stone().ItemType();

        Type[,] Crafted_AxeRecipe;
        Crafted_AxeRecipe = new Type[,] {
        { none, stone, stone },
        { none, stick, stone },
        { none, stick, none }
        };
        RecipeList.Add(new Recipe("Crafted_Axe", Crafted_AxeRecipe));

        foreach(Recipe recipe in RecipeList) {
            foreach(Type type in recipe.Schematic) {
                Debug.Log(type);
            }
        }
    }
}


public class Recipe {
    public string RecipeName { get; set; }
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



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

    }
    private void Start() {
        Type stick = new Stick().ItemType();
        Type none = new None().ItemType();
        Type stone = new Stone().ItemType();

        RecipeList.Add(new Recipe("Crafted_Axe", none, stick, none, none, stick, stone, none, stone, stone, new Crafted_Axe(), 1));

        foreach (Recipe recipe in RecipeList) {
            Debug.Log(recipe);
            foreach (Type type in recipe.Schematic) {
                Debug.Log(type);
            }
        }
    }
}



public class Recipe {

    public string RecipeName { get; set; }
    private Type[] _schematic = new Type[9];
    public Type[] Schematic {
        get { return _schematic; }
        set { _schematic = Schematic; }
    }
    public Item Product { get; private set; }
    public int Quantity { get; private set; }


    public Recipe(string name, Type T0, Type T1, Type T2, Type T3, Type T4, Type T5, Type T6, Type T7, Type T8, Item product, int quantity) {
        RecipeName = name;
        Schematic[0] = T0;
        Schematic[1] = T1;
        Schematic[2] = T2;
        Schematic[3] = T3;
        Schematic[4] = T4;
        Schematic[5] = T5;
        Schematic[6] = T6;
        Schematic[7] = T7;
        Schematic[8] = T8;
        Product = product;
        Quantity = quantity;
        
    }
}



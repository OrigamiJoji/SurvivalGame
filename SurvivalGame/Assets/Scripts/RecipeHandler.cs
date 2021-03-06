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
    
    private void Start() {
        Type stick = new Stick().ItemType();
        Type none = new None().ItemType();
        Type stone = new Stone().ItemType();
        Type wood = new Oak_Wood().ItemType();
        Type cBar = new Copper_Bar().ItemType();
        Type iBar = new Iron_Bar().ItemType();
        Type bWood = new Birch_Wood().ItemType();

        RecipeList.Add(new Recipe("Crafted_Axe", none, stick, none, none, stick, stone, none, stone, stone, new Crafted_Axe(), 1));
        RecipeList.Add(new Recipe("Copper_Axe", none, wood, none, none, wood, cBar, none, cBar, cBar, new Copper_Axe(), 1));
        RecipeList.Add(new Recipe("Iron_Axe", none, bWood, none, none, bWood, iBar, none, iBar, iBar, new Iron_Axe(), 1));

        RecipeList.Add(new Recipe("Crafted_Pickaxe", none, stick, none, none, stick, none, stone, stone, stone, new Crafted_Pickaxe(), 1));
        RecipeList.Add(new Recipe("Copper_Pickaxe", none, wood, none, none, wood, none, cBar, cBar, cBar, new Copper_Pickaxe(), 1));
        RecipeList.Add(new Recipe("Iron_Pickaxe", none, bWood, none, none, bWood, none, iBar, iBar, iBar, new Iron_Pickaxe(), 1));
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



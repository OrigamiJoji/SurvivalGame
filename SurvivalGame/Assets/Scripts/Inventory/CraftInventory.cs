using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public sealed class CraftInventory : Inventory {

    private RecipeHandler _recipeHandler;
    private Slot[,] _craftingGrid = new Slot[3, 3];
    public Slot[,] CraftingGrid {
        get { return _craftingGrid; }
        set { _craftingGrid = CraftingGrid; }
    }


    private void Awake() {
        _recipeHandler = RecipeHandler.Instance;

    }

    private void Start() {
        GenerateInventory(new Slot[3,3], 9);
    }

    private void Update() {

    }
    private void TestRecipes() {
        foreach (Recipe recipe in _recipeHandler.RecipeList) {
            for (int r = 0; r < 3; r++) {
                for (int c = 0; c < 3; c++) {
                    if (CraftingGrid[r, c].Item.ItemType().Equals(recipe.Schematic[r, c])) {
                        if (r.Equals(3) && c.Equals(3)) {
                            Debug.Log("recipe correct");
                        }
                        continue;
                    }
                    else {
                        break;
                    }
                }
            }
        }
    }

}



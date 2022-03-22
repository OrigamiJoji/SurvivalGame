using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public sealed class CraftInventory : Inventory {

    public static event InventoryEvent UpdateCraftingInventory;
    protected override void OnChange() {
        UpdateCraftingInventory?.Invoke();
        TestRecipes();
    }

    private void Start() {
        GenerateInventory(new Slot[3, 3], 9);
    }

    private void Update() {

    }
    private void TestRecipes() {
        Debug.Log("recipes testing...");
        Type[] craftingGridTypes = new Type[9];
        Type[] recipes = new Type[9];
        int craftIndex = 0;
        foreach (Slot slot in InventoryGrid) {
            craftingGridTypes[craftIndex] = slot.Item.ItemType();
            craftIndex++;

        }
        foreach (Recipe recipe in RecipeHandler.RecipeList) {
            Debug.Log(recipe.RecipeName);
            int recipeIndex = 0;
            foreach (Type type in recipe.Schematic) {
                recipes[recipeIndex] = type;
            }

            int y = 0;
            foreach (Type type in recipes) {
                Debug.Log(type + y.ToString());
            }
        }

        if (craftingGridTypes.Equals(recipes)) {
            Debug.Log("crafted");
        }
    }

    /*

                if (CraftingGrid[r, c].Item.ItemType().Equals(recipe.Schematic[r, c])) {
                    if (r.Equals(3) && c.Equals(3)) {
                        Debug.Log("recipe correct");
                    }
                    continue;
                }
                else {
                    break;

                Debug.Log(recipe.RecipeName);
            for (int r = 0; r < 3; r++) {
                for (int c = 0; c < 3; c++) {
                    Debug.Log(c + " " + r);
                    if(InventoryGrid[r, c].Item.Equals(recipe.Schematic[r, c])) {
                        if (r.Equals(3) && c.Equals(3)) { Debug.Log("Crafted"); }
                        continue;
                    }
                    break;
                }
            }
    */


}



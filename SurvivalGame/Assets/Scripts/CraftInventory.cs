using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public sealed class CraftInventory : MonoBehaviour {

    private RecipeHandler _recipeHandler;
    private int _totalSlots = 9;
    private Slot[,] _craftingGrid = new Slot[3, 3];
    public Slot[,] CraftingGrid {
        get { return _craftingGrid; }
        set { _craftingGrid = CraftingGrid; }
    }

    private PlayerInventory _playerInventory;

    private void Awake() {
        _recipeHandler = RecipeHandler.Instance;
        _playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        // Whew ! Good thing this isn't a multiplayer game.

    }

    private void Start() {
        GenerateSlots();
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

    private void GenerateSlots() {
        for (int i = 0; i < _totalSlots; i++) {
            Slot craftingSlot = new Slot();
            craftingSlot.Item = new None();
            craftingSlot.Quantity = 0;

            _craftingGrid[GetRow(i), GetColumn(i)] = craftingSlot;
        }
        Debug.Log("Crafting table instantiated");
    }
    private int GetRow(int slotPos) {
        var row = Mathf.FloorToInt(slotPos / _craftingGrid.GetLength(1));
        return row;
    }
    private int GetColumn(int slotPos) {
        var column = slotPos % _craftingGrid.GetLength(1);
        return column;
    }

}



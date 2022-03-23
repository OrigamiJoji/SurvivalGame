using UnityEngine;
using System;
using System.Linq;

public sealed class CraftInventory : Inventory {

    public static event InventoryEvent UpdateCraftingInventory;
    private PlayerInventory _playerInventory;
    private void Awake() {
        _playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
    }
    protected override void OnChange() {
        UpdateCraftingInventory?.Invoke();
        TestRecipes();
    }
    private void Start() {
        GenerateInventory(new Slot[3, 3], 9);
        OnChange();
    }

    private void TestRecipes() {
        Debug.Log("recipes testing...");
        Type[] craftingGridTypes = new Type[9];
        int craftIndex = 0;
        foreach (Slot slot in InventoryGrid) {
            craftingGridTypes[craftIndex] = slot.Item.ItemType();
            // Debug.Log($"{craftIndex} holds {slot.Item.ItemType()}");
            craftIndex++;

        }
        foreach (Recipe recipe in RecipeHandler.RecipeList) {
            Debug.Log(recipe.RecipeName);
            if (recipe.Schematic.SequenceEqual(craftingGridTypes)) {
                Debug.Log("Crafted");
                foreach (Slot slot in InventoryGrid) {
                    slot.Item = new None();
                    slot.Quantity = 0;
                }
                _playerInventory.PickupItem(recipe.Product, recipe.Quantity);
                OnChange();
            }
        }


    }
}



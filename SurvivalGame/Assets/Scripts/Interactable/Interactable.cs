using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Interactable : MonoBehaviour {

    public delegate void UpdateInventoryCaller<T>(T caller);

    public bool InUse { get; set; }
    protected GameObject ObjectUI {
        get {
            return InteractableUIManager.Instance.GetUI(GetType().Name);
        }
    }
    private Inventory _thisInventory;
    protected static GameObject InventoryUI { get { return InteractableUIManager.Instance.GetUI("Inventory"); } }

    private void Update() {
        if (InUse) {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E)) {
                Close();
            }
        }
    }

    public void Interact() {
        if (!InUse) { 
            Open();
            UpdateCaller();
            UpdateUI();
        }
    }

    private void UpdateUI() {
        gameObject.GetComponent<Inventory>().OnChange();
    }

    protected void Open() {
        ObjectUI.SetActive(true);
        InUse = true;
    }

    protected abstract void UpdateCaller();

    protected void Close() {
        ObjectUI.SetActive(false);
        InventoryUI.SetActive(false);
        InUse = false;
    }

}




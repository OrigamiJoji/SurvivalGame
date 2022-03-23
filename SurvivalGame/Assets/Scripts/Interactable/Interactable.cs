using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour {
    protected bool InUse { get; set; }
    protected GameObject ObjectUI {
        get {
            return InteractableUIManager.Instance.GetUI(GetType().Name);
        }
    }
    protected static GameObject InventoryUI { get { return InteractableUIManager.Instance.GetUI("Inventory"); } }

    private void Update() {
        if (InUse) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Close();
            }
        }
    }

    public void Interact() {
        if (!InUse) { Open(); }
    }
    protected void Open() {
        ObjectUI.SetActive(true);
        InventoryUI.SetActive(true);
        InUse = true;
    }

    protected void Close() {
        ObjectUI.SetActive(false);
        InventoryUI.SetActive(false);
        InUse = false;
    }

}




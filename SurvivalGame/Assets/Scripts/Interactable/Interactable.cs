using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected GameObject ObjectUI { get; set; }
    protected GameObject TargetUI() {
        return InteractableUIManager.Instance.GetUI(GetType().Name);
    }

    void Update()
    {
        
    }

    public void Interact() {

    }

    public void Open() {
        ObjectUI.SetActive(true);
    }

    public void Close() {
        ObjectUI.SetActive(false);
    }

    public Interactable() {
        ObjectUI = Instantiate(TargetUI());
        ObjectUI.transform.parent = gameObject.transform;
        ObjectUI.SetActive(false);
    }
}




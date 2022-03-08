using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCursorUI : MonoBehaviour
{
    private Text _quantityText;
    private Image _image;
    private PlayerInventory _playerInventory;

    private void Awake() {
        _playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        _quantityText = gameObject.GetComponentInChildren<Text>();
        _image = gameObject.GetComponentInChildren<Image>();
    }

    private void Update() {
        if (_playerInventory.GetImage() != null) {
            _image = _playerInventory.GetImage();
        }
        _quantityText.text = _playerInventory.GetQuantity().ToString();
    }

}

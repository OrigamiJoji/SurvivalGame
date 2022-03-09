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
        //_image = gameObject.GetComponentInChildren<Image>();
        _image = transform.Find("Image").GetComponent<Image>();
    }

    private void Update() {
        gameObject.transform.position = Input.mousePosition;

        if (_playerInventory.GetQuantity() != 0) {
            _quantityText.text = _playerInventory.GetQuantity().ToString();
        }
        else {
            _quantityText.text = string.Empty;
        }

        if (_playerInventory.GetItem() is None) {
            _image.gameObject.SetActive(false);
        }
        else {
            _image.gameObject.SetActive(true);
            _image.sprite = _playerInventory.GetSprite();
        }
    }

}

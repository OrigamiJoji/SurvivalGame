using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class InventoryUI : MonoBehaviour, IPointerClickHandler {

    [SerializeField] private int _buttonPositionX;
    [SerializeField] private int _buttonPositionY;

    private Text _quantityText;
    private Image _image;
    private PlayerInventory _playerInventory;


    private void Awake() {
        _playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        _quantityText = gameObject.GetComponentInChildren<Text>();
        _image = gameObject.GetComponentInChildren<Image>();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            //onLeftClick.Invoke();
            LeftClickFunction();
        }
        else if (eventData.button == PointerEventData.InputButton.Right) {
            //onRightClick.Invoke();
            RightClickFunction();
        }
        else if(eventData.button == PointerEventData.InputButton.Middle) {
            MiddleClickFunction();
        }
    }

    public void LeftClickFunction() {
        _playerInventory.OnLeftClick(_buttonPositionX, _buttonPositionY);
    }

    public void RightClickFunction() {
        _playerInventory.OnRightClick(_buttonPositionX, _buttonPositionY);
    }

    public void MiddleClickFunction() {
        Debug.Log(_playerInventory.GetSlotData(_buttonPositionX, _buttonPositionY));
        Debug.Log(_playerInventory.GetSlotData());
    }

    private void Update() {
       // if (_playerInventory.GetImage(_buttonPositionX, _buttonPositionY) != null) {
       //     _image = _playerInventory.GetImage(_buttonPositionX, _buttonPositionY);
       // }
        _quantityText.text = _playerInventory.GetQuantity(_buttonPositionX, _buttonPositionY).ToString();
    }

}

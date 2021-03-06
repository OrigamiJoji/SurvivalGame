using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public sealed class InventoryUI : MonoBehaviour, IPointerClickHandler {

    [SerializeField] private int _buttonPositionX;
    [SerializeField] private int _buttonPositionY;

    private Text _quantityText;
    private Image _image;
    private PlayerInventory _playerInventory;
    private Image _buttonImage;

    private void Awake() {
        PlayerInventory.UpdatePlayerInventory += UpdateUI;
        _buttonImage = gameObject.GetComponent<Image>();
        _playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        _quantityText = gameObject.GetComponentInChildren<Text>();
        _image = transform.Find("Image").GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            LeftClickFunction();
        }
        else if (eventData.button == PointerEventData.InputButton.Right) {
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
        Debug.Log(_playerInventory.FindSlot(_buttonPositionX, _buttonPositionY).Item);
        Debug.Log(_playerInventory.Debug());
    }

    private void UpdateUI() {
        if (_playerInventory.GetQuantity(_buttonPositionX, _buttonPositionY) > 1) {
            _quantityText.text = _playerInventory.GetQuantity(_buttonPositionX, _buttonPositionY).ToString();
        }
        else {
            _quantityText.text = string.Empty;
        }

        if (_playerInventory.GetItem(_buttonPositionX, _buttonPositionY) is None) {
            _image.gameObject.SetActive(false);
        }
        else {
            _image.gameObject.SetActive(true);
            _image.sprite = _playerInventory.GetSprite(_buttonPositionX, _buttonPositionY);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public sealed class CraftingUI : MonoBehaviour, IPointerClickHandler {

    [SerializeField] private int _buttonPositionX;
    [SerializeField] private int _buttonPositionY;

    private Text _quantityText;
    private Image _image;
    private PlayerInventory _playerInventory;
    private CraftInventory _craftInventory;

    private void Awake() {
        CraftInventory.UpdateCraftingInventory += UpdateUI;
        Workstump.UpdateCraft += UpdateCraft;
        _playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        _quantityText = gameObject.GetComponentInChildren<Text>();
        _image = transform.Find("Image").GetComponent<Image>();
    }

    private void UpdateCraft(CraftInventory craftInventory) {
        _craftInventory = craftInventory;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            LeftClickFunction();
        }
        else if (eventData.button == PointerEventData.InputButton.Right) {
            RightClickFunction();
        }
        else if (eventData.button == PointerEventData.InputButton.Middle) {
            MiddleClickFunction();
        }
    }

    public void LeftClickFunction() {
        _craftInventory.OnLeftClick(_buttonPositionX, _buttonPositionY);
    }

    public void RightClickFunction() {
        _craftInventory.OnRightClick(_buttonPositionX, _buttonPositionY);
    }

    public void MiddleClickFunction() {
        Debug.Log(_craftInventory.GetSlotData(_buttonPositionX, _buttonPositionY));
    }

    private void UpdateUI() {

        if (_craftInventory.GetQuantity(_buttonPositionX, _buttonPositionY) > 1) {
            _quantityText.text = _craftInventory.GetQuantity(_buttonPositionX, _buttonPositionY).ToString();
        }
        else {
            _quantityText.text = string.Empty;
        }

        if (_craftInventory.GetItem(_buttonPositionX, _buttonPositionY) is None) {
            _image.gameObject.SetActive(false);
        }
        else {
            _image.gameObject.SetActive(true);
            _image.sprite = _craftInventory.GetSprite(_buttonPositionX, _buttonPositionY);
        }
    }
}

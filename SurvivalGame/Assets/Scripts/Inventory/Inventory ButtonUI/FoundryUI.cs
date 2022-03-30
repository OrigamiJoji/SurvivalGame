using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FoundryUI : MonoBehaviour, IPointerClickHandler {

    private Image _image;
    private Text _quantityText;
    private FoundryInventory _foundryInventory;

    [SerializeField] private int _row;
    [SerializeField] private int _column;

    private void Awake() {
        FoundryInt.UpdateFoundry += UpdateFoundry;
        FoundryInventory.UpdateFoundryInventory += UpdateUI;
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
        else if (eventData.button == PointerEventData.InputButton.Middle) {
            MiddleClickFunction();
        }
    }

    private void UpdateFoundry(FoundryInventory foundryInventory) {
        _foundryInventory = foundryInventory;
    }

    private void LeftClickFunction() {
        _foundryInventory.OnLeftClick(_row, _column);
    }

    private void RightClickFunction() {
        _foundryInventory.OnRightClick(_row, _column);
    }

    private void MiddleClickFunction() {
    }


    private void UpdateUI() {
        if (_foundryInventory.GetQuantity(_row, _column) > 1) {
            _quantityText.text = _foundryInventory.GetQuantity(_row, _column).ToString();
        }
        else {
            _quantityText.text = string.Empty;
        }

        if (_foundryInventory.GetItem(_row, _column) is None) {
            _image.gameObject.SetActive(false);
        }
        else {
            _image.gameObject.SetActive(true);
            _image.sprite = _foundryInventory.GetSprite(_row, _column);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public sealed class InventoryCursorUI : MonoBehaviour
{
    private Text _quantityText;
    private Image _image;

    private void Awake() {     
        _quantityText = gameObject.GetComponentInChildren<Text>();
        _image = transform.Find("Image").GetComponent<Image>();
    }

    private void Update() {
        gameObject.transform.position = Input.mousePosition;

        if (HeldItem.Instance.Held.Quantity > 1) {
            _quantityText.text = HeldItem.Instance.Held.Quantity.ToString();
        }
        else {
            _quantityText.text = string.Empty;
        }

        if (HeldItem.Instance.Held.Item is None) {
            _image.gameObject.SetActive(false);
        }
        else {
            _image.gameObject.SetActive(true);
            _image.sprite = HeldItem.Instance.Held.Item.Icon();
        }
    }

}

using UnityEngine;
using UnityEngine.UI;

public class FoundryHeatUI : MonoBehaviour {

    private Slider _heatSlider;
    private FoundryInventory _foundryInventory;

    private void Awake() {
        FoundryInt.UpdateFoundry += UpdateFoundry;
        _heatSlider = gameObject.GetComponent<Slider>();
    }

    private void UpdateFoundry(FoundryInventory foundryInventory) {
        _foundryInventory = foundryInventory;
        _heatSlider.maxValue = (float) foundryInventory.MaxSmeltingTime;
    }
    private void Update() {
        _heatSlider.value = (float) _foundryInventory.CurrentSmeltingTime;

    }
}

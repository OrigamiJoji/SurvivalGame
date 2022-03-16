using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] private Slider _actionSlider;

    [SerializeField] private Text _targetText;
    [SerializeField] private Slider _targetHealthSlider;
    [SerializeField] private Text _targetHealthText;
    [SerializeField] private Text _equippedItemText;

    [SerializeField] private GameObject _activeHotbarIcon;

    
    [System.Serializable]
    public class Hotbar {
        [field: SerializeField] public int SlotID { get; set; }
        [field: SerializeField] public GameObject Button {get; set;}
    }
    [SerializeField] private List<Hotbar> _hotbarList;

    private PlayerUse _playerUse;
    private PlayerInventory _playerInventory;
    private Attackable _currentTarget;

    private void Awake() {
        var player = GameObject.Find("Player");
        _playerUse = player.GetComponent<PlayerUse>();
        _playerInventory = player.GetComponent<PlayerInventory>();
    }

    private void UpdateSlot() {
        for(int i = 0; i < 5; i++) {
            if(Input.GetKeyDown(KeyCode.Alpha1 + i)) {
                var hotbarSlot = _hotbarList.Find(e => e.SlotID.Equals(i));
                _activeHotbarIcon.transform.position = hotbarSlot.Button.transform.position + Vector3.up * 1.2f;
            }
        }
    }

    void Update() {

        UpdateSlot();
        if (_playerInventory.EquippedItem is None) {
            _equippedItemText.text = string.Empty;
        }
        else {
            _equippedItemText.text = _playerInventory.EquippedItem.ToString().Replace("_", " ");
        }

        _actionSlider.maxValue = (float)_playerInventory.EquippedItemToolStats.AttackSpeed;
        if (_playerUse.TimeToAttack > 0) {
            _actionSlider.gameObject.SetActive(true);
            _actionSlider.value = (float)_playerUse.TimeToAttack;
        }
        else {
            _actionSlider.gameObject.SetActive(false);
        }
        _currentTarget = _playerUse.CurrentTargetAttackable();

        if (_currentTarget != null) {
            _targetHealthSlider.gameObject.SetActive(true);
            _targetHealthText.gameObject.SetActive(true);
            _targetText.gameObject.SetActive(true);
            _targetHealthSlider.value = _currentTarget.HitPoints;
            _targetHealthSlider.maxValue = _currentTarget.MaxHitPoints;
            _targetText.text = _currentTarget.GetType().Name.ToString();
            _targetHealthText.text = $"{_currentTarget.HitPoints}/{_currentTarget.MaxHitPoints}";
        }
        else {
            _targetText.gameObject.SetActive(false);
            _targetHealthSlider.gameObject.SetActive(false);
            _targetHealthText.gameObject.SetActive(false);
        }
    }
}

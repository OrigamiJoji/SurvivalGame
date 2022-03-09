using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider _actionSlider;

    [SerializeField] private Text _targetText;
    [SerializeField] private Slider _targetHealthSlider;
    [SerializeField] private Text _targetHealthText;

    private PlayerUse _playerUse;
    private PlayerInventory _playerInventory;
    private Attackable _currentTarget;

    private void Awake() {
        var player = GameObject.Find("Player");
        _playerUse = player.GetComponent<PlayerUse>();
        _playerInventory = player.GetComponent<PlayerInventory>();
    }
    void Update()
    {
        _actionSlider.maxValue = (float) _playerInventory.EquippedItemToolStats.AttackSpeed;
        if(_playerUse.TimeToAttack > 0) {
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

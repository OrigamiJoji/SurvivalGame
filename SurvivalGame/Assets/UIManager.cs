using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider _actionSlider;
    private PlayerUse _playerUse;
    private PlayerInventory _playerInventory;

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

    }
}

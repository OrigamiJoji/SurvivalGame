using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTester : MonoBehaviour {
    private PlayerInventory _playerInventory;
    private PlayerMove _playerMove;
    private PlayerLook _playerLook;
    private PlayerUse _playerUse;

    private void Awake() {
        var player = GameObject.Find("Player");
        _playerInventory = player.GetComponent<PlayerInventory>();
        _playerMove = player.GetComponent<PlayerMove>();
        _playerLook = player.GetComponent<PlayerLook>();
        _playerUse = player.GetComponent<PlayerUse>();
    }


    void Update() {
        if (Input.GetKeyDown(KeyCode.F1)) {
            _playerInventory.PickupItem(new Crafted_Axe(), 1);
        }
        if (Input.GetKeyDown(KeyCode.F2)) {
            _playerInventory.PickupItem(new Stick(), 3);
        }
        if (Input.GetKeyDown(KeyCode.F3)) {
        }
        if (Input.GetKeyDown(KeyCode.F4)) {
        }
        if (Input.GetKeyDown(KeyCode.F5)) {
        }
        if (Input.GetKeyDown(KeyCode.F6)) {
        }
        if (Input.GetKeyDown(KeyCode.F7)) {
        }
        if (Input.GetKeyDown(KeyCode.F8)) {
        }
        if (Input.GetKeyDown(KeyCode.F9)) {
        }
        if (Input.GetKeyDown(KeyCode.F10)) {
        }
        if (Input.GetKeyDown(KeyCode.F11)) {
        }
        if (Input.GetKeyDown(KeyCode.F12)) {
        }
    }
}

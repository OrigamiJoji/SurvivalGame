using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerUse : MonoBehaviour
{

    [SerializeField] private Transform _playerCamera;
    [SerializeField] private LayerMask _playerMask;

    private PlayerMove _playerMove;
    private PlayerInventory _playerInventory;
    private PlayerLook _playerLook;
    [SerializeField] private float _useRange;

    private bool IsInventoryOpened { get; set; }

    [SerializeField] private GameObject _fullInventory;

    [field: SerializeField] public double TimeToAttack { get; private set; }
    [field: SerializeField] public bool CanAttack { get; private set; }

    private void Awake() {
        _playerInventory = gameObject.GetComponent<PlayerInventory>();
        _playerMove = gameObject.GetComponent<PlayerMove>();
        _playerLook = gameObject.GetComponent<PlayerLook>();
    }
    private void Start() {
        _playerMask = ~_playerMask;
        _playerInventory.EquippedItem = new Fists();
        IsInventoryOpened = false;
        Cursor.lockState = CursorLockMode.Locked;
        _fullInventory.SetActive(false);
    }


    private void Update() {
        if (TimeToAttack > 0) {
            TimeToAttack -= Time.deltaTime;
        }
        else { CanAttack = true; }


        if (Input.GetMouseButton(0) && CanAttack && !IsInventoryOpened) {
            TimeToAttack = _playerInventory.EquippedItemToolStats.AttackSpeed;
            CanAttack = false;
            if (Physics.Raycast(_playerCamera.position, _playerCamera.forward, out RaycastHit hit, _playerInventory.EquippedItemToolStats.Range, _playerMask)) {
                Debug.DrawRay(_playerCamera.position, _playerCamera.forward, Color.green, 1);
                GameObject objectHit = hit.collider.gameObject;
                if (objectHit.CompareTag("Attackable")) {
                    // if Attackable, do damage.
                    Attackable objectAttackable = objectHit.GetComponent<Attackable>();
                    objectAttackable.TakeDamage(_playerInventory);
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && CanAttack && !IsInventoryOpened) {
            if (Physics.Raycast(_playerCamera.position, _playerCamera.forward, out RaycastHit hit, _useRange, _playerMask)) {
                Debug.DrawRay(_playerCamera.position, _playerCamera.forward, Color.green, 1);
                GameObject objectHit = hit.collider.gameObject;
                if (objectHit.CompareTag("Interactable")) {
                    // if Attackable, do damage.
                    Interactable objectInteractable = objectHit.GetComponent<Interactable>();
                    objectInteractable.Interact();
                    if(objectInteractable.InUse) { OpenInventory(true); }
                    else { OpenInventory(false); }
                    Debug.Log("Interacted");
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.E) && !IsInventoryOpened) {
            //open inv
            OpenInventory(true);

        }
        else if((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)) && IsInventoryOpened) {
            //close inv
            OpenInventory(false);
        }
    }

    public void OpenInventory(bool status) {
        _fullInventory.SetActive(status);
        IsInventoryOpened = status;
        _playerMove.LockMovement(status);
        _playerLook.LockMouseInput(status);

        switch (status) {
            case false:
                Cursor.lockState = CursorLockMode.Locked;
                break;
            case true:
                Cursor.lockState = CursorLockMode.None;
                break;
        }
    }

    public Attackable CurrentTargetAttackable() {
        if (Physics.Raycast(_playerCamera.position, _playerCamera.forward, out RaycastHit hit, _playerInventory.EquippedItemToolStats.Range, _playerMask)) {
            GameObject objectHit = hit.collider.gameObject;
            if (objectHit.CompareTag("Attackable")) {
                Attackable objectAttackable = objectHit.GetComponent<Attackable>();
                return objectAttackable;
            }
        }
        return null;
    }
}

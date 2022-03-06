using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerUse : MonoBehaviour
{
    private PlayerInventory _playerInventory;
    [SerializeField] private Transform _playerCamera;
    [SerializeField] private LayerMask _playerMask;

    [field: SerializeField] public double TimeToAttack { get; private set; }
    [field: SerializeField] public bool CanAttack { get; private set; }

    private void Awake() {
        _playerInventory = gameObject.GetComponent<PlayerInventory>();
    }
    private void Start() {
        _playerMask = ~_playerMask;
        _playerInventory.EquippedItem = new Fists();
    }

    private void Update() {
        if(TimeToAttack > 0) {
            TimeToAttack -= Time.deltaTime;
        }
        else { CanAttack = true; }


        if(Input.GetMouseButton(0) && CanAttack) {
            TimeToAttack = _playerInventory.EquippedItemToolStats.AttackSpeed;
            CanAttack = false;
            if(Physics.Raycast(_playerCamera.position, _playerCamera.forward, out RaycastHit hit, _playerInventory.EquippedItemToolStats.Range, _playerMask)) {
                Debug.DrawRay(_playerCamera.position, _playerCamera.forward, Color.green, 1);
                GameObject objectHit = hit.collider.gameObject;

                if(objectHit.CompareTag("Attackable")) { 
                    // if Attackable, do damage.
                    Attackable objectAttackable = objectHit.GetComponent<Attackable>();
                    objectAttackable.TakeDamage(_playerInventory.EquippedItemToolStats.Damage);
                }
            }
        }
    }

}

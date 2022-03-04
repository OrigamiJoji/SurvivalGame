using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerUse : MonoBehaviour
{
    [HideInInspector] public ITool equippedTool;

    [SerializeField] private Transform playerCamera;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private double timeToAttack;
    [SerializeField] private bool canAttack;

    private void Start() {
        playerMask = ~playerMask;
        equippedTool = new Fists();
    }

    private void Update() {
        if(timeToAttack > 0) {
            timeToAttack -= Time.deltaTime;
        }
        else { canAttack = true; }


        if(Input.GetMouseButton(0) && canAttack) {
            timeToAttack = equippedTool.AttackSpeed;
            canAttack = false;
            if(Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hit, equippedTool.Range, playerMask)) {
                Debug.DrawRay(playerCamera.position, playerCamera.forward, Color.green, 1);
                GameObject objectHit = hit.collider.gameObject;

                if(objectHit.CompareTag("Attackable")) { // If attackable, do damage
                    Attackable objectAttackable = objectHit.GetComponent<Attackable>();
                    objectAttackable.TakeDamage(equippedTool.Damage);
                }
            }
        }
    }

}

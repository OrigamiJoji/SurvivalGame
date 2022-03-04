using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerUse : MonoBehaviour
{
    [SerializeField] private float attackTime;
    [SerializeField] private float attackRange;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float timeToAttack;
    [SerializeField] private bool canAttack;

    private void Start() {
        playerMask = ~playerMask;
    }

    private void Update() {
        if(timeToAttack > 0) {
            timeToAttack -= Time.deltaTime;
            canAttack = false;
        }
        else { canAttack = true; }


        if(Input.GetMouseButtonDown(0) && canAttack) {
            timeToAttack = attackTime;
            if(Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hit, attackRange, playerMask)) {
                Debug.DrawRay(playerCamera.position, playerCamera.forward, Color.green, 1);
            }
        }
    }

}

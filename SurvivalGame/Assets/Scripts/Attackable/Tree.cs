using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Attackable {
    [SerializeField] private float _treeHP;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.R)) {
            TakeDamage(5);
        }
    }
    private void Start() {
        SpawnEntity(_treeHP);
    }
}

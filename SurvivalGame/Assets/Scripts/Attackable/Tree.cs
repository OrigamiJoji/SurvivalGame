using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Attackable {
    [SerializeField] private float treeHP;

    private void Start() {
        hitPoints = treeHP;
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.R)) {
            TakeDamage(5);
        }
    }

}

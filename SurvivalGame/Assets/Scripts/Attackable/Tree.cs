using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tree : Attackable {
    [SerializeField] private float _treeHP;
    [SerializeField] private Type[] _typeReq;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.R)) {
            TakeDamage(5);
        }
    }
    public Tree() {
        SpawnEntity(_treeHP);
        _typeReq = [typeof(Crafted_Axe)];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tree : Attackable {
    [SerializeField] private float _treeHP;
    [SerializeField] private Type typeReq;
    [SerializeField] private int tierReq;

    private void Start() {
        typeReq = typeof(Axe);
        tierReq = 1;
        SpawnEntity(_treeHP);
    }
}

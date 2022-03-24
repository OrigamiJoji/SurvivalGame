using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Copper : Attackable
{
    [SerializeField] private float _HP;
    [SerializeField] private int _minDrop;
    [SerializeField] private int _maxDrop;
    private Drop _drops;

    private void Start() {
        _drops = new Drop(new Copper_Ore(), _minDrop, _maxDrop);
        ItemDrops.Add(_drops);
        TierReq = 1;
        TypeReq = typeof(Pickaxe);
        SpawnEntity(_HP);
    }
}

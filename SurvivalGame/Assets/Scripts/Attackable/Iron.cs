using UnityEngine;

public class Iron : Attackable {
    [SerializeField] private float _HP;
    [SerializeField] private int _minDrop;
    [SerializeField] private int _maxDrop;
    private Drop _drops;

    private void Start() {
        _drops = new Drop(new Iron_Ore(), _minDrop, _maxDrop);
        ItemDrops.Add(_drops);
        TierReq = 2;
        TypeReq = typeof(Pickaxe);
        SpawnEntity(_HP);
    }
}


using UnityEngine;

public class Rock : Attackable {
    [SerializeField] private float _HP;
    [SerializeField] private int _minDrop;
    [SerializeField] private int _maxDrop;
    private Drop _drops;

    private void Start() {
        _drops = new Drop(new Stone(), _minDrop, _maxDrop);
        ItemDrops.Add(_drops);
        TierReq = 0;
        SpawnEntity(_HP);
    }
}

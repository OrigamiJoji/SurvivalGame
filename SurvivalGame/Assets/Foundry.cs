using UnityEngine;

public class Foundry : Attackable
{
    [SerializeField] private float _HP;
    [SerializeField] private int _minDrop;
    [SerializeField] private int _maxDrop;
    private Drop _drops;

    private void Start() {
        _drops = new Drop(new Foundry_(), _minDrop, _maxDrop);
        ItemDrops.Add(_drops);
        TierReq = 0;
        SpawnEntity(_HP);
    }
}

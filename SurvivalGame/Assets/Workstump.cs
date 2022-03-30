using UnityEngine;

public class Workstump : Attackable
{
    [SerializeField] private float _HP;
    [SerializeField] private int _minDrop;
    [SerializeField] private int _maxDrop;
    private Drop _drops;

    private void OnEnable() {
        _drops = new Drop(new Workstump_(), _minDrop, _maxDrop);
        ItemDrops.Add(_drops);
        TierReq = 0;
        SpawnEntity(_HP);
    }
}

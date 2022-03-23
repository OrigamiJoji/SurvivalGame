using UnityEngine;

public class Bush : Attackable {
    [SerializeField] private float _bushHP;
    [SerializeField] private int _minDrop;
    [SerializeField] private int _maxDrop;
    private Drop _drops;

    private void Start() {
        _drops = new Drop(new Stick(), _minDrop, _maxDrop);
        ItemDrops.Add(_drops);
        TierReq = 0;
        SpawnEntity(_bushHP);
    }
}

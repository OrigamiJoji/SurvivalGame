using UnityEngine;

public class Birch : Attackable {
    [SerializeField] private float _treeHP;
[SerializeField] private int _minDrop;
[SerializeField] private int _maxDrop;
private Drop _drops;

private void Start() {
    _drops = new Drop(new Birch_Wood(), _minDrop, _maxDrop);
    ItemDrops.Add(_drops);
    TypeReq = typeof(Axe);
    TierReq = 2;
    SpawnEntity(_treeHP);
}
}

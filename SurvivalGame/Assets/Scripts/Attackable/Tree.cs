using UnityEngine;

public class Tree : Attackable {
    [SerializeField] private float _treeHP;
    [SerializeField] private int _minDrop;
    [SerializeField] private int _maxDrop;
    private Drop _drops;

    private void Start() {
        _drops = new Drop(new Wood(), _minDrop, _maxDrop);
        ItemDrops.Add(_drops);
        TypeReq = typeof(Axe);
        TierReq = 1;
        SpawnEntity(_treeHP);
    }
}

using UnityEngine;

public class Oak : Attackable {
    [SerializeField] private float _treeHP;
    [SerializeField] private int _minDrop;
    [SerializeField] private int _maxDrop;
    private Drop _drops;

    private void Start() {
        _drops = new Drop(new Oak_Wood(), _minDrop, _maxDrop);
        ItemDrops.Add(_drops);
        TypeReq = typeof(Axe);
        TierReq = 1;
        SpawnEntity(_treeHP);
    }
}

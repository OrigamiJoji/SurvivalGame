using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    #region Data Members
    private InventorySlot[,] _inventory = new InventorySlot[3, 5];
    private int _totalSlots;

    private Slot _heldItem = new Slot();
    public Slot HeldItem {
        get {
            return _heldItem;
        }
        set {
            _heldItem = HeldItem;
        }
    }


    public Item EquippedItem { get; set; }
    public ITool EquippedItemToolStats {
        get {
            if (EquippedItem is ITool) {
                return (ITool)EquippedItem;
            }
            else {
                return new Fists();
            }
        }
    }
    #endregion Data Members

    #region Slot Class
    public class Slot {
        public Item Item { get; set; }
        public int Quantity { get; set; }
    }

    [System.Serializable]
    public class InventorySlot : Slot {
        public int Position { get; set; }
        public static int TotalSlots { get; set; }

        public InventorySlot() {
            Item = null;
            Position = TotalSlots;
            TotalSlots++;
        }
    }

    #endregion Slot Class

    private void GenerateSlots() {
        for (int i = _totalSlots; i > 0; i--) {
            InventorySlot inventorySlot = new InventorySlot();
            inventorySlot.Item = new None();
            inventorySlot.Quantity = 0;
            var column = inventorySlot.Position % _inventory.GetLength(1);
            var row = Mathf.FloorToInt(inventorySlot.Position / _inventory.GetLength(1));
            _inventory[row, column] = inventorySlot;
            Debug.Log($"Item #{inventorySlot.Position} is in position #[{row}, {column}] and contains {inventorySlot.Item}. A total of {InventorySlot.TotalSlots} slots exist");
        }
    }

    private void Start() {
        _totalSlots = _inventory.GetLength(1) * _inventory.GetLength(0);
        GenerateSlots();
        FindSlot(0, 1).Item = new Axe();
        FindSlot(0, 1).Quantity = 20;

        FindSlot(1, 3).Item = new Stick();
        FindSlot(1, 3).Quantity = 20;

        HeldItem.Item = new None();
        HeldItem.Quantity = 0;
    }


    private void ClearSlot(Slot slot) {
        slot.Item = new None();
        slot.Quantity = 0;
    }

    private void VerifySlot(Slot inventorySlot) {
        if (inventorySlot.Quantity.Equals(0)) {
            inventorySlot.Item = new None();
        }
        if (HeldItem.Quantity.Equals(0)) {
            HeldItem.Item = new None();
        }
    }

    private int ItemPlaceRemainder(InventorySlot inventorySlot) {
        var itemsInHand = HeldItem.Quantity; // 45
        var limit = inventorySlot.Item.MaxStackSize; //64
        var itemsInSlot = inventorySlot.Quantity; // 25
        if (itemsInHand + itemsInSlot <= limit) {
            // return 0 if items in slot and hand are within slot max
            return 0;
        }
        else {
            // return items needing to be subtracted from hand to equal the max
            return limit - itemsInSlot;
        }
    }

    private void SwapItems(InventorySlot inventorySlot) {
        var tempItem = inventorySlot.Item;
        var tempQuantity = inventorySlot.Quantity;
        inventorySlot.Item = HeldItem.Item;
        inventorySlot.Quantity = HeldItem.Quantity;
        HeldItem.Item = tempItem;
        HeldItem.Quantity = tempQuantity;
    }

    private void SlotAllItems(InventorySlot inventorySlot) {
        var req = ItemPlaceRemainder(inventorySlot);
        if (req > 0) {
            inventorySlot.Quantity += req;
            HeldItem.Quantity -= req;
        }
        else {
            inventorySlot.Quantity += HeldItem.Quantity;
            ClearSlot(HeldItem);
        }
        VerifySlot(inventorySlot);
    }

    private void SlotOneItem(InventorySlot inventorySlot) {
        if (inventorySlot.Item is None) {
            if (HeldItem.Quantity > 0) {
                inventorySlot.Item = HeldItem.Item;
                HeldItem.Quantity -= 1;
                inventorySlot.Quantity += 1;
                VerifySlot(inventorySlot);
            }
        }
        else {
            if (HeldItem.Item.Equals(inventorySlot.Item)) {
                if (HeldItem.Quantity > 0 && inventorySlot.Quantity < inventorySlot.Item.MaxStackSize) {
                    HeldItem.Quantity -= 1;
                    inventorySlot.Quantity += 1;
                    VerifySlot(inventorySlot);
                }
            }
            else {
                SwapItems(inventorySlot);
                VerifySlot(inventorySlot);
            }
        }
    }

    private void TakeHalf(InventorySlot inventorySlot) {
        if (inventorySlot.Quantity >= 2) {
            var half = inventorySlot.Quantity / 2;
            HeldItem.Quantity += half;
            inventorySlot.Quantity -= half;
            HeldItem.Item = inventorySlot.Item;
            VerifySlot(inventorySlot);
        }
    }


    #region Action Methods
    public void OnLeftClick(int x, int y) {
        var currentSlot = FindSlot(x, y);
        if (currentSlot.Item.Equals(HeldItem.Item)) {
            SlotAllItems(currentSlot);
        }
        else {
            SwapItems(currentSlot);
        }
    }

    public void OnRightClick(int x, int y) {
        var currentSlot = FindSlot(x, y);
        if (HeldItem.Item is None) {
            TakeHalf(currentSlot);
        }
        else {
            SlotOneItem(currentSlot);
        }
    }
    #endregion Action Methods

    #region Public Methods
    public string GetSlotData() {
        return $"{HeldItem.Quantity} of Item: {HeldItem.Item} exists in hand";
    }
    public string GetSlotData(int x, int y) {
        InventorySlot slot = FindSlot(x, y);
        return $"{slot.Quantity} of Item: {slot.Item} exists in Position #{slot.Position}";
    }

    public int GetQuantity(int x, int y) {
        return _inventory[x, y].Quantity;
    }
    public int GetQuantity() {
        return HeldItem.Quantity;
    }

    public InventorySlot FindSlot(int x, int y) {
        return _inventory[x, y];
    }
    public Item GetItem(int x, int y) {
        return _inventory[x, y].Item;
    }
    public Item GetItem() {
        return HeldItem.Item;
    }

    public Sprite GetSprite() {
        return HeldItem.Item.Icon;
    }
    public Sprite GetSprite(int x, int y) {
        return _inventory[x, y].Item.Icon;
    }

    #endregion Public Methods


}


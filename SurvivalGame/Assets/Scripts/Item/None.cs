using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class None : Item
{
    public None() {
        ItemType = this.GetType();
        MaxStackSize = 0;
    }
}

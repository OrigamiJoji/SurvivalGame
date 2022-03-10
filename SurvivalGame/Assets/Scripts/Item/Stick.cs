using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Stick : Item {
    public Stick() {
        Icon = ImageHandler.Instance.GetSprite(GetType().Name.ToString());
        ItemType = this.GetType();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class HeldItem : MonoBehaviour
{
    static HeldItem _instance;
    public static HeldItem Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<HeldItem>();
            }
            return _instance;
        }
    }

    private Slot _held = new Slot();
    public Slot Held {
        get { return _held; }
        set { _held = Held; }
    }

}

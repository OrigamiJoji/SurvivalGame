using System.Collections.Generic;
using UnityEngine;

public sealed class ImageHandler : MonoBehaviour {
    static ImageHandler _instance;
    public static ImageHandler Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<ImageHandler>();
            }
            return _instance;
        }
    }

    [System.Serializable]
    public class ItemImage {
        public string _name;
        public Sprite _sprite;
    }
    [SerializeField] private List<ItemImage> ImageList;
    public Sprite GetSprite(string name) {
        return ImageList.Find(e => e._name.Equals(name))._sprite;
    }
}

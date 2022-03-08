using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageHandler : MonoBehaviour
{
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
        public Image _image;
    }
    [SerializeField] private List<ItemImage> ImageList;

    public Image GetIcon(string name) {
        return ImageList.Find(e => e._name.Equals(name))._image;
    }

}

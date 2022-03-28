using System.Collections.Generic;
using UnityEngine;

public sealed class InteractableUIManager : MonoBehaviour {
    static InteractableUIManager _instance;
    public static InteractableUIManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<InteractableUIManager>();
            }
            return _instance;
        }
    }

    [System.Serializable]
    public class InteractableUI {
        public string _name;
        public GameObject _UI;
    }

    [SerializeField] private List<InteractableUI> _UIList;

    public GameObject GetUI(string name) {
        return _UIList.Find(e => e._name.Equals(name))._UI;
    }
}
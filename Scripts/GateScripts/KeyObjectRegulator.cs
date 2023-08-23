using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyNetwork
{
    public class KeyObjectRegulator : MonoBehaviour
    {
        [SerializeField] private bool KEY = false;
        [SerializeField] private bool GATE = false;
        [SerializeField] private KeyList keyList = null;

        private KeyGateRegulator keyGateRegulator; // Gate Object

    
        private void Start()
        {
            if (GATE)
            {
                keyGateRegulator = GetComponent<KeyGateRegulator>();
            }
        }

        public void foundObject()
        {
            if (KEY)
            {
                keyList.hasKey = true;
                gameObject.SetActive(false);
            }
            else if (GATE)
            {
                keyGateRegulator.StartAnimation();
            }
        }



































    }
}

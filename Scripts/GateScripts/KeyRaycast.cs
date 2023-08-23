using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace KeyNetwork
{
    public class KeyRaycast : MonoBehaviour
    {
        [Header("Raycast Radius and Layer")]
        [SerializeField] private int rayRadius = 6;
        [SerializeField] private LayerMask LayerMaskCollective;
        [SerializeField] private string banLayerName = null;

        private KeyObjectRegulator keyObjectRegulator; // raycast Object
        [SerializeField] private KeyCode openGateButton = KeyCode.F;
        [SerializeField] private Image crosshair = null;

        private bool checkCrosshair;
        private bool Onetime;

        private string collectiveTag = "collectiveObject";

        private void Update()
        {
            RaycastHit hitInfo;

            Vector3 forwardDirection = transform.TransformDirection(Vector3.forward);


            
            int mask = 1 << LayerMask.NameToLayer(banLayerName) | LayerMaskCollective.value; //  e�er collective etiketi olan kap�ya bak�yorsak raycast �al���cak

            if(Physics.Raycast(transform.position, forwardDirection, out hitInfo, rayRadius, mask))
            {
                if(hitInfo.collider.CompareTag(collectiveTag))
                {
                    if(!Onetime)
                    {
                        keyObjectRegulator = hitInfo.collider.gameObject.GetComponent<KeyObjectRegulator>();
                        ChangeCrosshair(true);
                    }
                    
                    checkCrosshair = true;
                    Onetime = true;

                    if(Input.GetKeyDown(openGateButton))
                    {
                        keyObjectRegulator.foundObject();
                    }
                }
            }
            else
            {
                if(checkCrosshair)
                {
                    ChangeCrosshair(false);
                    Onetime = false;

                }
            }
        }


        private void ChangeCrosshair(bool changeCH) 
        {
            if(changeCH && !Onetime)
            {
                crosshair.color = Color.blue;
            }
            else
            {
                crosshair.color = Color.white;
                checkCrosshair = false;
            }
        }
    }
}


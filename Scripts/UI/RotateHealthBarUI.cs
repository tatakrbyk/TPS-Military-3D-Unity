using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHealthBarUI : MonoBehaviour
{
    [SerializeField] private Transform mainCam;

    private void LateUpdate()
    {
        transform.LookAt(transform.position + mainCam.forward);
    }
}

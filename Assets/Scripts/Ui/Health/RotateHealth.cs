using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RotateHealth : MonoBehaviour
{
    private Camera _cam;
    private void Start()
    {
        _cam = Camera.main;
    }
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
    }
}

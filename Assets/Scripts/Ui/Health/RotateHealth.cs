using UnityEngine;

public class RotateHealth : MonoBehaviour
{
    private Camera _cam;
    private void Start()
    {
        _cam = Camera.main;
    }
    
    void Update()
    {
        Vector3 direction = transform.position - _cam.transform.position;
        direction.y = 0;  // Giữ cho Health Bar luôn ngang
        transform.rotation = Quaternion.LookRotation(direction);
    }

}

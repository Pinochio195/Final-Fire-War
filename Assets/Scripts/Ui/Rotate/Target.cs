

using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    public enum Direction { Right, Left }

    public float rotationSpeed = 40f; // Tốc độ quay (độ/giây)
    public Direction direction = Direction.Right; // Hướng quay mặc định

    private void Update()
    {
        float rotationStep = rotationSpeed * Time.deltaTime;
        if (direction == Direction.Left)
        {
            rotationStep = -rotationStep; // Đảo chiều quay nếu hướng là Left
        }
        transform.Rotate(0, 0, rotationStep);
    }

   
}

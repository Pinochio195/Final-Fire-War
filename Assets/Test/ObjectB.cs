using UnityEngine;

public class ObjectB : MonoBehaviour
{
    public GameObject objectA;
    public GameObject objectB;
    public float radius;
    public float moveSpeed = 2.0f;

    private void Update()
    {
        MoveObjectB();
    }

    private void MoveObjectB()
    {
        if (objectA != null && objectB != null)
        {
            Vector3 positionA = objectA.transform.position;

            // Tính toán vị trí mới của objectB
            Vector3 direction = objectB.transform.position - positionA;
            float distance = direction.magnitude;
            // Kiểm tra xem objectB có ở ngoài bán kính không
            /*if (distance > radius)
            {
            }*/
            Vector3 targetPosition = positionA + direction.normalized * radius;

            // Lerp giữa vị trí hiện tại và vị trí mới của objectB
            Vector3 newPosition = Vector3.Lerp(objectB.transform.position, targetPosition, Time.deltaTime * moveSpeed);
            objectB.transform.position = newPosition;
        }
        else
        {
            Debug.LogError("Vui lòng thiết lập objectA và objectB trong Inspector.");
        }
    }
}
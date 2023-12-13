using UnityEngine;
using UnityEngine.EventSystems;

public class RotateAroundPlayer : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Vector3 _positionStart;
    private Vector3 _positionEnd;
    private Vector3 directionToTarget;

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _positionStart = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _positionEnd = Input.mousePosition;
        directionToTarget = _positionEnd - _positionStart;
        Debug.Log(directionToTarget.normalized);
        float signedAngle = Vector3.SignedAngle(transform.forward, directionToTarget, Vector3.up);
        if (directionToTarget.x > 0)
        {
            signedAngle = Mathf.Abs(signedAngle);
        }
        else
        {
            signedAngle = -Mathf.Abs(signedAngle);
        }
        PlayerManager.Instance._playerController._rotateCamPlayer.Rotate(Vector3.up, signedAngle * 0.05f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        directionToTarget = Vector3.zero;
    }
}
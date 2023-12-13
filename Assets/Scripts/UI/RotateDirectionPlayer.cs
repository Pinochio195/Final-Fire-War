using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateDirectionPlayer : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
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
        float signatureAngleX = Vector3.SignedAngle(transform.right, directionToTarget, Vector3.up);
        float signatureAngleY = Vector3.SignedAngle(transform.up, directionToTarget, Vector3.right);
        signatureAngleX = Mathf.Clamp(signatureAngleX, -45f, 45f);
        signatureAngleY = Mathf.Clamp(signatureAngleY, -45f, 45f);
        if (directionToTarget.y < 0)
        {
            signatureAngleX = Mathf.Abs(signatureAngleX);
        }
        else
        {
            signatureAngleX = -Mathf.Abs(signatureAngleX);
        }
        if (directionToTarget.x > 0)
        {
            signatureAngleY = Mathf.Abs(signatureAngleY);
        }
        else
        {
            signatureAngleY = -Mathf.Abs(signatureAngleY);
        }
        PlayerManager.Instance._playerController._rotateCamPlayer.Rotate(signatureAngleX * 0.05f, signatureAngleY * 0.05f, 0);
        PlayerManager.Instance._playerController._rotateCamPlayer.eulerAngles = new Vector3(PlayerManager.Instance._playerController._rotateCamPlayer.eulerAngles.x, PlayerManager.Instance._playerController._rotateCamPlayer.eulerAngles.y, 0);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        directionToTarget = Vector3.zero;
    }
}
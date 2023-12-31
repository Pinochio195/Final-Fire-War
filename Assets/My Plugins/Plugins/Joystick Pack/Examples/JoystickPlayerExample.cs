﻿using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickPlayerExample : MonoBehaviour , IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public float speed;
    private VariableJoystick variableJoystick;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _meshPlayer;
    [SerializeField] private Vector3 direction;
    private void Start()
    {
        variableJoystick = UiManager.Instance._uiController._variableJoystick;
    }

    public void FixedUpdate()
    {
        if (variableJoystick == null || rb == null)
        {
            Debug.LogWarning("VariableJoystick or Rigidbody is not assigned.");
            return;
        }
        direction = Camera.main.transform.forward * variableJoystick.Vertical + Camera.main.transform.right * variableJoystick.Horizontal;
        direction.y = 0;  // Giữ nhân vật di chuyển trên mặt phẳng
        if (direction != Vector3.zero)
        {
            PlayerManager.Instance._playerController._directionBullet = direction.normalized;
        }
        rb.velocity = direction.normalized * speed;
        if (_animator.GetBool(Settings.AnimationRun) == false && rb.velocity != Vector3.zero)
        {
            _animator.SetBool(Settings.AnimationRun, true);
            _animator.SetLayerWeight(1,0);
        }
        else if (_animator.GetBool(Settings.AnimationRun) == true && rb.velocity == Vector3.zero)
        {
            _animator.SetBool(Settings.AnimationRun, false);
            _animator.SetLayerWeight(1,1);
        }
        TargetEnemy();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //PlayerManager.Instance._playerController._directionBullet = direction.normalized;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //PlayerManager.Instance._playerController._directionBullet = direction.normalized;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //PlayerManager.Instance._playerController._directionBullet = direction.normalized;
    }

    public void TargetEnemy()
    {
        if (direction != Vector3.zero && !PlayerManager.Instance._playerController.isCheckTarget)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            _meshPlayer.rotation = toRotation;
        }
    }
}
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _meshPlayer;
    [SerializeField] private Vector3 direction;

    public void FixedUpdate()
    {
        if (variableJoystick == null || rb == null)
        {
            Debug.LogWarning("VariableJoystick or Rigidbody is not assigned.");
            return;
        }
        direction = Camera.main.transform.forward * variableJoystick.Vertical + Camera.main.transform.right * variableJoystick.Horizontal;
        //direction = transform.forward * variableJoystick.Vertical + transform.right * variableJoystick.Horizontal;
        direction.y = 0;  // Giữ nhân vật di chuyển trên mặt phẳng
        rb.velocity = direction.normalized * speed;
        if (_animator.GetBool(Settings.AnimationRun) == false && rb.velocity != Vector3.zero)
        {
            _animator.SetBool(Settings.AnimationRun, true);
        }
        else if (_animator.GetBool(Settings.AnimationRun) == true && rb.velocity == Vector3.zero)
        {
            _animator.SetBool(Settings.AnimationRun, false);
        }
        TargetEnemy();
    }

    public void TargetEnemy()
    {
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            _meshPlayer.rotation = toRotation;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchBullet : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Settings.Tag_Bullet))
        {
            _rigidbody.AddForce(PlayerManager.Instance._playerController._directionBullet.normalized * 2.5f,ForceMode.Impulse);
        }
    }
}

using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] ParticleSystem _bomb;
    [SerializeField] Rigidbody _rigidbody;
    private void OnEnable()
    {
        _rigidbody.AddForce(Vector3.up*5*Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(Settings.Tag_Ground))
        {
            MusicManager.Instance.PlayAudio(Ring.MusicController.TypeMusic.Bomb);
            LeanPool.Spawn(_bomb, transform.position, Quaternion.identity);
            //_rigidbody.AddExplosionForce(20, transform.position, 5);
            LeanPool.Despawn(gameObject);
        }
    }
}

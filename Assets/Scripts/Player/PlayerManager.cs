using Ring;
using UnityEngine;

public class PlayerManager : RingSingleton<PlayerManager>
{
    [HeaderTextColor(0.2f, .7f, .8f, headerText = "CheckBox For Player")] public PlayerController _playerController;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ActiveRagdoll();
        }
    }

    public void ActiveRagdoll()
    {
        if (_playerController._animator != null)
        {
            _playerController._animator.enabled = false;
        }

        foreach (Rigidbody rb in _playerController._listRigidbody)
        {
            rb.isKinematic = false;
        }
    }
}
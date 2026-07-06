using UnityEngine;

public class GlowEffect : MonoBehaviour
{
    [SerializeField] Transform _player;

    private void OnEnable()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        transform.position = _player.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _player.position;
    }
}

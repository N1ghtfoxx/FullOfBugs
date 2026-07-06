using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class MapTransition : MonoBehaviour, IInteractable
{
    [SerializeField] Transform _player;
    [SerializeField] Transform _target;
    [SerializeField] PolygonCollider2D _mapBorder;
    CinemachineConfiner2D _confiner;
    CinemachineCamera _cmCam; 

    public bool instantInteract { get; set; } = false;

    private void Awake()
    {
        _confiner = FindFirstObjectByType<CinemachineConfiner2D>();
        _cmCam = _confiner.GetComponent<CinemachineCamera>();
        if(_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public void Interact()
    {
        FadeTransition();
    }

    async void FadeTransition()
    {
        await UiManager.instance.FadeIn();
        _confiner.BoundingShape2D = _mapBorder;
        _player.position = _target.position;
        _cmCam.ForceCameraPosition(_target.position, _cmCam.transform.rotation);
        _cmCam.CancelDamping(true);
        await UiManager.instance.FadeOut();
    }


    public void Selected()
    {
        
    }
}

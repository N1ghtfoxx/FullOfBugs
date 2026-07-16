using System.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;

public class MapTransition : MonoBehaviour, IInteractable
{
    [SerializeField] Transform _player;
    [SerializeField] Transform _target;
    [SerializeField] PolygonCollider2D _mapBorder;
    CinemachineConfiner2D _confiner;
    CinemachineCamera _cmCam;

    [SerializeField] TextAsset _reviveDialog;

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
        StartFade();
    }

    private async void StartFade()
    {
        await FadeTransition();
    }

    async Task FadeTransition()
    {
        await UiManager.instance.FadeIn();
        _confiner.BoundingShape2D = _mapBorder;
        _player.position = _target.position;
        _cmCam.ForceCameraPosition(_target.position, _cmCam.transform.rotation);
        _cmCam.CancelDamping(true);
        await UiManager.instance.FadeOut();
    }

    public void TransitionAfterFight()
    {
        TAF();
    }

    async void TAF()
    {
        await FadeTransition();
        DialogueManager.instance.StartDialogue(_reviveDialog);
    }

    public void Selected()
    {
        
    }
}

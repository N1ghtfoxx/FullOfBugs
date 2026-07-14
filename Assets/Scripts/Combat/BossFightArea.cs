using UnityEngine;
using UnityEngine.Events;

public class BossFightArea : MonoBehaviour, IInteractable
{
    private bool _isBossActive = true;
    [SerializeField] GameObject _bossObj;
    [SerializeField] Transform _player;
    [SerializeField] string _bossQuestName;
    

    public bool instantInteract { get; set; } = true;

    public void Interact()
    {
        UnityEvent conf = new UnityEvent();
        conf.AddListener(StartBossBattle);

        UnityEvent cancel = new UnityEvent();
        cancel.AddListener(CancelBossFight);

        ConfirmManager.instance.AskForConfirmation(conf, cancel);
        PauseManager.instance.SetPause();
    }

    public void StartBossBattle()
    {
        _bossObj.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = false;
        FightManager.instance.StartBossFight(_bossQuestName);
    }

    public void CancelBossFight()
    {
        _player.position -= Vector3.up;
        PauseManager.instance.SetPause();
    }

    public void Selected()
    {
        
    }
}
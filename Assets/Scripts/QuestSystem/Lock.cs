using UnityEngine;

public class Lock : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject _closed;
    [SerializeField] GameObject _open;
    [SerializeField] LockID _lockId;
    [SerializeField] GameObject _nextKey;

    public bool instantInteract { get; set; } = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryOpenLock()
    {
        if (QuestManager.instance.TryOpenLock(_lockId))
        {
            _closed.SetActive(false);
            _open.SetActive(true);
            if(_nextKey != null)
                _nextKey.SetActive(true);
            //QuestManager.instance.UpdAllQuestObjByName("OpenDoors", 1);
            FindAnyObjectByType<HermbertMovement>().MoveHermbert();
        }
    }

    public void Interact()
    {
        if(_closed.activeSelf)
        TryOpenLock();
    }

    public void Selected()
    {
        
    }
}

using UnityEngine;

public class Lock : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject _wall;
    [SerializeField] LockID _lockId;

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
        if (QuestManager.instance.CanOpenLock(_lockId))
        {
            _wall.SetActive(false);
            FindAnyObjectByType<HermbertMovement>().MoveHermbert();
        }
    }

    public void Interact()
    {
        TryOpenLock();
    }

    public void Selected()
    {
        
    }
}

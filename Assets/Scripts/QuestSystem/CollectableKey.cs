using NUnit.Framework.Constraints;
using UnityEngine;

public class CollectableKey : MonoBehaviour, IInteractable
{
    public bool instantInteract { get; set; } = false;

    [SerializeField] Key _key;

    public void Interact()
    {
        QuestManager.instance.CollectKey(_key);
        Destroy(gameObject);
    }

    public void Selected()
    {
        FailFeedbackManager.instance.ShowFailFeedbackInGame(GetComponent<SpriteRenderer>().sprite, gameObject);
    }

}
[System.Serializable]
public class Key
{
    public KeyID keyId;
    public LockID lockId;
}

public enum KeyID
{
    Green,
    Orange,
    Yellow,
    Blue,
    Red
}

public enum LockID
{
    Green,
    Orange,
    Yellow,
    Blue,
    Red
}

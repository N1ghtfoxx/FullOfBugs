using System.Collections;
using UnityEngine;

public class CollectableOverTime : MonoBehaviour, IInteractable
{
    [SerializeField] protected ItemData _data;

    [SerializeField] float _collectionTime = 5f;
    private Coroutine _collectionRoutine;

    private GameObject _progressBar;

    public bool instantInteract { get; set; } = false;

    public void Interact()
    {
        if (_collectionRoutine == null)
            _collectionRoutine = StartCoroutine(CollectionProcess());
        else
        {
            StopCoroutine(_collectionRoutine);
            _collectionRoutine = null;
            _progressBar.SetActive(false);
        }
    }

    public void Selected()
    {
        FailFeedbackManager.instance.ShowFailFeedbackInGame(_data.icon, gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerInteractionController>(out PlayerInteractionController controller))
            _progressBar = controller.progressBar;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(_collectionRoutine != null)
        {
            StopCoroutine(_collectionRoutine);
            _collectionRoutine = null;        

        }
        if (_progressBar != null)
            _progressBar.SetActive(false);
    }

    IEnumerator CollectionProcess()
    {
        _progressBar.SetActive(true);
        Vector3 scale = _progressBar.transform.localScale;
        scale.x = 1;
        _progressBar.transform.localScale = scale;
        float elapsedTime = 1f;
        while (elapsedTime > 0)
        {
            if (PauseManager.instance.isPaused) yield return null;
            elapsedTime -= Time.deltaTime/_collectionTime;
            scale.x = elapsedTime;
            _progressBar.transform.localScale = scale;
            yield return null;
        }

        _progressBar.SetActive(false);
        if (!InventoryManager.instance.AddItemToInventory(_data))
            FailFeedbackManager.instance.ShowFailFeedbackInGame(_data.icon, gameObject);
        else
        {
            LootNotificationManager.instance.ShowNotification(_data);
        }

        _collectionRoutine = null;
    }
}

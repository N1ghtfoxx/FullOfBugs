using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class FailFeedbackManager : MonoBehaviour
{
    public static FailFeedbackManager instance;

    private GameObject _feedbackUiObj;
    private Image _feedbackUiImg;

    private GameObject _feedbackGameObj;
    private SpriteRenderer _feedbackGameRenderer;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        _feedbackUiObj = GameObject.Find("FailFeedbackUI");
        _feedbackUiImg = _feedbackUiObj.GetComponent<Image>();

        _feedbackGameObj = GameObject.Find("FailFeedbackGame");
        _feedbackGameRenderer = _feedbackGameObj.GetComponent<SpriteRenderer>();

        ResetFeedback(_feedbackUiObj);
        ResetFeedback(_feedbackGameObj);
    }

    public void ShowFailFeedbackUI(Sprite original, GameObject parent)
    {
        _feedbackUiImg.sprite = original;
        _feedbackUiObj.transform.SetParent(parent.transform, false);
        RectTransform transform = parent.GetComponent<RectTransform>();
        _feedbackUiImg.rectTransform.sizeDelta = transform.sizeDelta * 1.1f;
        _feedbackUiImg.rectTransform.position = transform.position;
        _feedbackUiImg.rectTransform.rotation = transform.rotation;
        StartCoroutine(FeedbackRoutine(_feedbackUiObj));
    }

    public void ShowFailFeedbackInGame(Sprite original, GameObject parent)
    {
        _feedbackGameRenderer.sprite = original;
        _feedbackGameObj.transform.SetParent(parent.transform, false);
        _feedbackGameObj.transform.position = parent.transform.position;
        _feedbackGameObj.transform.rotation = parent.transform.rotation;
        _feedbackGameObj.transform.localScale = parent.transform.localScale * 1.1f;
        StartCoroutine(FeedbackRoutine(_feedbackGameObj));
    }

    private IEnumerator FeedbackRoutine(GameObject go)
    {
        go.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        go.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        go.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ResetFeedback(go);
    }

    public void ResetFeedback(GameObject go)
    {
        go.transform.SetParent(transform, false);
        go.SetActive(false);
    }
}

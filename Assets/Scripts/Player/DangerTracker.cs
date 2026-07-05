using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DangerTracker : MonoBehaviour
{
    [SerializeField] Image _progressbar;
    [SerializeField] Image _dangerbar;

    [SerializeField] float _duration = 60f;
    private float _progress = 0f;
    private float _danger = 0f;

    private bool _inLight = false;

    [SerializeField] GameObject _enemyGo;

    private void Start()
    {
        _progressbar.fillAmount = _progress;
        _dangerbar.fillAmount = _danger;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseManager.instance.isPaused) return;
        _progress += Time.deltaTime;
        _progressbar.fillAmount = _progress / _duration;
        if (!_inLight)
        {
            _danger += Time.deltaTime;
            _dangerbar.fillAmount = _danger / _duration;
        }
        if(_progress >= _duration)
        {
            float ran = Random.Range(0, _duration);
            if(ran < _danger)
            {
                PauseManager.instance.ForcePause();
                StartCoroutine(StartFightCutscene());
            }
            _danger = 0;
            _progress = 0;
            _dangerbar.fillAmount = 0;
            _progressbar.fillAmount = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Light"))
        {
            _inLight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Light"))
        {
            _inLight = false;
        }
    }

    private IEnumerator StartFightCutscene()
    {
        _enemyGo.SetActive(true);
        _enemyGo.transform.localPosition = new Vector3(Random.Range(-8, 8), 8, 0);
        while(Vector3.Distance(_enemyGo.transform.position, transform.position) > 1)
        {
            _enemyGo.transform.position = Vector3.MoveTowards(_enemyGo.transform.position, transform.position, Time.deltaTime * 4);
            yield return null;
        }
        _enemyGo.SetActive(false);
        FightManager.instance.StartFight();

    }
}
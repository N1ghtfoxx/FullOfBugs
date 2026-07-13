using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HermbertLight : MonoBehaviour
{
    [SerializeField] Light2D _light;

    [SerializeField] float _cycle = 1f;
    [SerializeField] float _switchTime = 0.25f;
    [SerializeField] float _switchTime2 = 0.75f;

    [SerializeField] float _weakFalloff = 0.4f;
    [SerializeField] float _strongFalloff = 0.3f;

    [SerializeField] float _transitionDuration = 0.5f;

    private float _timer = 0f;

    private bool _isWeakPhase;
    private float _transitionProgress = 1f; // 1 = fertig
    private float _startFalloff;
    private float _targetFalloff;

    void Start()
    {
        if (_light == null)
            _light = GetComponent<Light2D>();

        _isWeakPhase = true;
        _light.falloffIntensity = _weakFalloff;
    }

    void Update()
    {
        _timer = (_timer + Time.deltaTime) % _cycle;

        bool newWeakPhase = _timer < _switchTime || _timer > _switchTime2;

        // PHASENWECHSEL ERKANNT  Transition starten
        if (newWeakPhase != _isWeakPhase)
        {
            _isWeakPhase = newWeakPhase;

            _transitionProgress = 0f; // Transition neu starten
            _startFalloff = _light.falloffIntensity;
            _targetFalloff = _isWeakPhase ? _weakFalloff : _strongFalloff;
        }

        // Transition läuft einmalig
        if (_transitionProgress < 1f)
        {
            _transitionProgress += Time.deltaTime / _transitionDuration;
            float t = Mathf.SmoothStep(0f, 1f, _transitionProgress);

            _light.falloffIntensity = Mathf.Lerp(_startFalloff, _targetFalloff, t);
        }
    }
}

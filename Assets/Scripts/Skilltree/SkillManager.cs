using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Skilltree;
using TMPro;
using UnityEngine.InputSystem;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    private GameObject _skilltreePanel;
    private GameObject _skilltreeUI;
    private List<SkilltreeNode> _skillNodes = new List<SkilltreeNode>();
    private List<Skill> _skills;
    private List<SkillID> _unlockedSkills = new List<SkillID>();
    private int[] _shadows;
    private TMP_Text[] _shadowCounterTexts;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        _skilltreePanel = GameObject.Find("SkilltreePanel");
        _skilltreeUI = _skilltreePanel.transform.Find("Skilltree").gameObject;
        foreach (SkilltreeNode node in _skilltreeUI.GetComponentsInChildren<SkilltreeNode>())
            _skillNodes.Add(node);

        _shadows = new int[ShadowType.GetValues(typeof(ShadowType)).Length];
        _shadowCounterTexts = new TMP_Text[_shadows.Length];
        Transform shadowCounterParent = _skilltreePanel.transform.Find("ShadowCounters").transform;
        for (int i = 0; i < _shadows.Length; i++)
        {
            _shadowCounterTexts[i] = shadowCounterParent.GetChild(i).GetComponent<TMP_Text>();
            //Test
            _shadows[i] = 5;
        }
        UpdateShadowUI();
    }

    public bool TryAddSkill(Skill skill)
    {
        if(HasSkill(skill.skillID))
            return false;
        if (SpendShadows(skill.cost))
        {
            _unlockedSkills.Add(skill.skillID);
            return true;
        }
        return false;
    }

    public bool HasSkill(SkillID skillID)
    {
        return _unlockedSkills.Contains(skillID);
    }

    public void UpdateSkilltree()
    {
        foreach (SkilltreeNode node in _skillNodes)
        {
            node.UpdateUI();
        }
    }

    public void AddShadow(ShadowType shadowType)
    {
        _shadows[(int)shadowType]++;
    }

    public bool HasShadows(Cost cost)
    {
        return _shadows[(int)cost.costType] >= cost.amount;
    }

    public bool SpendShadows(Cost cost)
    {
        if (HasShadows(cost))
        {
            _shadows[(int)cost.costType] -= cost.amount;
            UpdateShadowUI();
            return true;
        }
        //TODO: feedback for not enough shadows
        return false;
    }

    private void UpdateShadowUI()
    {
        for (int i = 0; i < _shadows.Length; i++)
        {
            string shadowName = "";
            switch (i)
            {
                case 0: shadowName = "Tiny"; break;
                case 1: shadowName = "Medium"; break;
                case 2: shadowName = "Large"; break;
            }
            _shadowCounterTexts[i].text = shadowName + " Shadows(" + ShadowType.GetName(typeof(ShadowType), i) + "): " + _shadows[i].ToString();
        }

    }

    //TODO: Save and load

    private RectTransform _rectTransform;
    private Vector2 _lastMousePos;
    private bool _skilltreeOpen = false;
    [SerializeField] InputActionReference _toggleInput;
    [SerializeField] InputActionReference _resetInput;
    [SerializeField] InputActionReference _moveInput;
    [SerializeField] float _moveSpeed = 100f;
    [SerializeField] InputActionReference _mouseMoveEnterInput;
    [SerializeField] InputActionReference _mouseMoveInput;

    void Start()
    {
        _skilltreePanel.SetActive(_skilltreeOpen);
    }
    private void Update()
    {
        if (_rectTransform == null)
        {
            _rectTransform = _skilltreeUI.GetComponent<RectTransform>();
        }

        if (_toggleInput.action.WasPerformedThisFrame())
        {
            _skilltreeOpen = !_skilltreeOpen;
            _skilltreePanel.SetActive(_skilltreeOpen);
            //toggle pause
        }

        if (!_skilltreeOpen) return;

        if (_mouseMoveEnterInput.action.WasPressedThisFrame())
        {
            _lastMousePos = _mouseMoveInput.action.ReadValue<Vector2>();
        }

        if (_mouseMoveEnterInput.action.IsPressed())
        {
            Vector2 mouseDelta = _mouseMoveInput.action.ReadValue<Vector2>() - _lastMousePos;
            _rectTransform.anchoredPosition +=mouseDelta;
            _lastMousePos = _mouseMoveInput.action.ReadValue<Vector2>();
        }

        Vector2 move = _moveInput.action.ReadValue<Vector2>();

        if (move != Vector2.zero)
        {
            _rectTransform.anchoredPosition += move * _moveSpeed * Time.deltaTime;
        }

        if (_resetInput.action.WasPressedThisFrame())
        {
            _rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.InputSystem;
using Skilltree;
using UnityEngine.Events;
using Crafting;

public class SkillManager : Singleton<SkillManager>
{
    //UI References
    [SerializeField] GameObject _skilltreePanel; //used to toggle
    [SerializeField] List<SkilltreeNode> _skillNodes = new List<SkilltreeNode>(); //reference to all skill nodes
    [SerializeField] TMP_Text[] _shadowCounterTexts; //references to the shadow counter ui texts, index corresponds to ShadowType enum

    //Data
    private List<SkillID> _unlockedSkills; //storage of unlocked skills
    private int[] _shadows; //storage of shadow amounts, index corresponds to ShadowType enum

    [SerializeField] Recipe _fertilizerRecipe;

    protected override void Awake()
    {
        base.Awake();

        //Get UI references
        //_skilltreePanel = GameObject.Find("SkilltreePanel");
        //GameObject _skilltreeUI = _skilltreePanel.transform.Find("Skilltree").gameObject;
        //_rectTransform = _skilltreeUI.GetComponent<RectTransform>();
        //foreach (SkilltreeNode node in _skilltreeUI.GetComponentsInChildren<SkilltreeNode>())
        //    _skillNodes.Add(node);

        _shadows = new int[ShadowType.GetValues(typeof(ShadowType)).Length];
        _shadowCounterTexts = new TMP_Text[_shadows.Length];
        Transform shadowCounterParent = _skilltreePanel.transform.Find("ShadowCounters").transform;
        for (int i = 0; i < _shadows.Length; i++)
        {
            _shadowCounterTexts[i] = shadowCounterParent.GetChild(i).GetComponent<TMP_Text>();
    //Load Values
            //Test replace with load shadows
            _shadows[i] = 5;
        }
        //load skill list
        _unlockedSkills = new List<SkillID>();
        AddSkill(SkillID.NoSkill);

    //Update UI
        UpdateShadowUI();
    }

    public bool TryAddSkill(Skill skill)
    {
        if(HasSkill(skill.skillID))
            return false;
        if (TrySpendShadows(skill))
        {
            return true;
        }
        return false;
    }

    private void AddSkill(SkillID skill)
    {
        _unlockedSkills.Add(skill);
        UpdateSkilltree();
        switch (skill)
        {
        case SkillID.StrongHealingPotion:
            CraftingManager.instance.TryUpgradeRecipe(Crafting.RecipeName.HealingRecipe, skill);
            break;
        case SkillID.LongGlowPotion:
            CraftingManager.instance.TryUpgradeRecipe(Crafting.RecipeName.GlowingRecipe, skill);
            break;
        case SkillID.Tank:
            PlayerStatsManager.instance.IncreaseMaxHp(1);
            break;

        case SkillID.Giant:
            PlayerStatsManager.instance.IncreaseMaxHp(2);
            break;

        case SkillID.Colossus:
            PlayerStatsManager.instance.IncreaseMaxHp(4);
            break;

        case SkillID.Garden:
                FarmingManager.instance.UnlockFields(4);
            break;

        case SkillID.Farm:
                FarmingManager.instance.UnlockFields(6);
            break;

        case SkillID.Fertilizer:
                CraftingManager.instance.AddRecipe(_fertilizerRecipe);
                break;

        case SkillID.PowerFertilizer:
            break;

        case SkillID.GreenThumb:
                FarmingManager.instance.ReduceGrowtime(30f);
            break;

        case SkillID.PlantMaster:
                FarmingManager.instance.ReduceGrowtime(30f);
            break;

        default:
            break;
        }
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
        UpdateShadowUI();
    }

    public bool HasShadows(Cost cost)
    {
        return _shadows[(int)cost.costType] >= cost.amount;
    }

    public bool TrySpendShadows(Skill skill)
    {
        if (HasShadows(skill.cost))
        {
            if(ConfirmManager.instance.CheckDontAsk(DontAskRegion.Skilltree))
            {
                ApplySkillBuy(skill);
            }
            else
            {
                UnityEvent e = new UnityEvent();
                e.AddListener(() =>
                {
                    ApplySkillBuy(skill);
                });
                ConfirmManager.instance.AskForConfirmation(e, DontAskRegion.Skilltree, $"Do you want to spend {skill.cost.amount} {ShadowType.GetName(typeof(ShadowType),skill.cost.costType)} for {skill.skillName}");
            }
            return true;
        }
        //TODO: feedback for not enough shadows
        return false;
    }

    private void ApplySkillBuy(Skill skill)
    {
        _shadows[(int)skill.cost.costType] -= skill.cost.amount;
        AddSkill(skill.skillID);
        UpdateShadowUI();
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


    #region Interaction
    [SerializeField] RectTransform _rectTransform; //reference to the skilltree rect transform for movement and zooming
    private Vector2 _lastMousePos; //used to calculate mouse movement delta for dragging the skilltree
    private bool _skilltreeOpen = false; //state of the skilltree, used to toggle and to prevent interaction when closed
    //Input references and settings
    [SerializeField] InputActionReference _toggleInput;
    [SerializeField] InputActionReference _resetInput;
    [SerializeField] InputActionReference _moveInput; //keyboard movement
    [SerializeField] float _moveSpeed = 100f; //just for keyboard movement
    [SerializeField] InputActionReference _mouseMoveEnterInput;
    [SerializeField] InputActionReference _mouseMoveInput;
    [SerializeField] InputActionReference _zoomInput;
    [SerializeField] float _zoomSpeed = 0.1f;
    [SerializeField] float _zoomMin = 0.3f;
    [SerializeField] float _zoomMax = 3f;

    void Start()
    {
        _skilltreePanel.SetActive(_skilltreeOpen); //Close skilltree after initialization
    }
    private void Update()
    {
        //Toggle Skilltree by key
        if (_toggleInput.action.WasPerformedThisFrame())
        {
            _skilltreeOpen = !_skilltreeOpen;
            _skilltreePanel.SetActive(_skilltreeOpen);
            //toggle pause
            PauseManager.instance.SetPause();
        }
        //Prevent interaction when skilltree is closed
        if (!_skilltreeOpen) return;
        //Keyboard movement
        if (_moveInput.action.IsPressed()) //check for input
        {
            Vector2 move = _moveInput.action.ReadValue<Vector2>(); //read the input
            _rectTransform.anchoredPosition += move * _moveSpeed * Time.deltaTime; //apply the input
        }
        //Mouse movement
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
        //Zooming
        if (_zoomInput.action.WasPerformedThisFrame())
        {
            float zoomAmount = _zoomInput.action.ReadValue<Vector2>().y;
            _rectTransform.localScale += Vector3.one * zoomAmount * _zoomSpeed;
            _rectTransform.localScale = Vector3.Max(_rectTransform.localScale, Vector3.one * _zoomMin);
            _rectTransform.localScale = Vector3.Min(_rectTransform.localScale, Vector3.one * _zoomMax);
        }
        //Reset position and zoom
        if (_resetInput.action.WasPressedThisFrame())
        {
            _rectTransform.anchoredPosition = Vector2.zero;
            _rectTransform.localScale = Vector3.one;
        }
    }
    #endregion
}
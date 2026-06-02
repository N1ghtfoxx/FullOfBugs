using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Skilltree;

public class SkilltreeNode : MonoBehaviour
{

    private GameObject _expansion;
    private TMP_Text _nameText;
    private TMP_Text _descriptionText;
    private TMP_Text _costText;

    private Image[] _prerequisitesLines;
    private Image _icon;

    [SerializeField] Skill _skill;

    //Shader
    //reached and skilled properties are used for both icon and lines, be sure both shaders use the same as well
    private string _reachedProperty = "_reached";
    private string _skilledProperty = "_skilled";
    [Header("Icon Shader")]
    [SerializeField] float _glowThreshold = 0.2f;
    private string _glowThresholdProperty = "_glowThreshold";

    [SerializeField] Color _glowColor = Color.turquoise;
    private string _glowColorProperty = "_skilledGlowColor";

    [SerializeField] float _reachedMultiplier = 0.4f;
    private string _reachedMultiplierProperty = "_reachedColorMultiplier";

    private void Awake()
    {
        _expansion = transform.Find("Expansion").gameObject;
        _nameText = _expansion.transform.Find("Name").GetComponent<TMP_Text>();
        _descriptionText = _expansion.transform.Find("Description").GetComponent<TMP_Text>();
        _costText = _expansion.transform.Find("Cost").GetComponent<TMP_Text>();
        _prerequisitesLines = transform.Find("Prerequisites").GetComponentsInChildren<Image>();
        _icon = transform.Find("Icon").GetComponent<Image>();

        foreach (Image line in _prerequisitesLines)
        {
            line.material = Instantiate(line.material);
            line.material.SetFloat(_skilledProperty, 0f);
            if (_skill.prerequisites.Count == 0)
                    line.material.SetFloat(_reachedProperty, 1f);
            else
                line.material.SetFloat(_reachedProperty, 0f);
        }

        _icon.material = Instantiate(_icon.material);
        _icon.material.SetFloat(_skilledProperty, 0f);
        _icon.material.SetFloat(_reachedProperty, 0f);
        _icon.material.SetFloat(_glowThresholdProperty, _glowThreshold);
        _icon.material.SetColor(_glowColorProperty, _glowColor);
        _icon.material.SetFloat(_reachedMultiplierProperty, _reachedMultiplier);
    }

    void Start()
    {
        UpdateUI();
        _expansion.SetActive(false);
    }

    public void UpdateUI()
    {

        //TODO: rework indikators
        if (SkillManager.instance.HasSkill(_skill.skillID))
        {
            foreach (Image line in _prerequisitesLines)
                line.material.SetFloat(_skilledProperty, 1f);

            _icon.material.SetFloat(_skilledProperty, 1f);
        }
        else
        {
            for (int i = 0; i < _skill.prerequisites.Count; i++)
            {
                if (SkillManager.instance.HasSkill(_skill.prerequisites[i]))
                    _prerequisitesLines[i].material.SetFloat(_reachedProperty, 1f);
                else
                    _prerequisitesLines[i].material.SetFloat(_reachedProperty, 0f);
            }
            if (CheckRequisites())
                _icon.material.SetFloat(_reachedProperty, 1f);
            else
                _icon.material.SetFloat(_reachedProperty, 0f);
        }

        _nameText.text = _skill.skillName;
        _descriptionText.text = _skill.description;
        _costText.text = _skill.cost.amount.ToString() + " " + _skill.cost.costType.ToString();
    }

    public void OnClick()
    {
        if (CheckRequisites())
        {
            SkillManager.instance.TryAddSkill(_skill);
            SkillManager.instance.UpdateSkilltree();
        }
        else
        {
            //Display message that requisites are not met
            Debug.Log("Cannot unlock skill: requisites not met");
        }

        _expansion.SetActive(false);
    }

    private bool CheckRequisites()
    {
        bool canUnlock = true;
        foreach (SkillID skill in _skill.prerequisites)
        {
            if (!SkillManager.instance.HasSkill(skill))
            {
                canUnlock = false;
                break;
            }
        }
        return canUnlock;
    }

    public void OnHoverEnter()
    {
        _expansion.SetActive(true);
    }

    public void OnHoverExit()
    {
        _expansion.SetActive(false);
    }
}

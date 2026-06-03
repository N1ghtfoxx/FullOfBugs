using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Skilltree;

public class SkilltreeNode : MonoBehaviour
{
    //UI References
    private GameObject _expansion;
    private TMP_Text _nameText;
    private TMP_Text _descriptionText;
    private TMP_Text _costText;

    private Image[] _prerequisitesLines;
    private Image _icon;

    [SerializeField] Skill _skill;

    //Shader properties and values
    //reached and skilled properties are used for both icon and lines, be sure both shaders use the same as well
    private string _reachedProperty = "_reached"; //bool used to indicate that the skill can be unlocked, either for the icon or the lines
    private string _skilledProperty = "_skilled"; //bool used to indicate that the skill is already unlocked, either for the icon or the lines
    [Header("Icon Shader")]
    [SerializeField] float _glowThreshold = 0.2f; //threshold for the needed brighness to be affected by the glow
    private string _glowThresholdProperty = "_glowThreshold";

    [SerializeField] Color _glowColor = Color.turquoise;
    private string _glowColorProperty = "_skilledGlowColor";

    [SerializeField] float _reachedMultiplier = 0.4f; //to darken the icon
    private string _reachedMultiplierProperty = "_reachedColorMultiplier";

    private void Awake()
    {
        //Get UI references
        _expansion = transform.Find("Expansion").gameObject;
        _nameText = _expansion.transform.Find("Name").GetComponent<TMP_Text>();
        _descriptionText = _expansion.transform.Find("Description").GetComponent<TMP_Text>();
        _costText = _expansion.transform.Find("Cost").GetComponent<TMP_Text>();
        _prerequisitesLines = transform.Find("Prerequisites").GetComponentsInChildren<Image>();
        _icon = transform.Find("Icon").GetComponent<Image>();
        //Initialize shader properties
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
        //Set tooltip texts
        _nameText.text = _skill.skillName;
        _descriptionText.text = _skill.description;
        _costText.text = _skill.cost.amount.ToString() + " " + _skill.cost.costType.ToString();
        //set the tooltip to its default inactive state
        _expansion.SetActive(false);
    }

    public void UpdateUI()
    {
        if (SkillManager.instance.HasSkill(_skill.skillID))// if skill is unlocked
        {
            foreach (Image line in _prerequisitesLines)
                line.material.SetFloat(_skilledProperty, 1f);

            _icon.material.SetFloat(_skilledProperty, 1f);
        }
        else //if not unlocked
        {
            for (int i = 0; i < _skill.prerequisites.Count; i++) //Chck each prerequisite
            {
                if (SkillManager.instance.HasSkill(_skill.prerequisites[i])) //if prerequisite is unlocked
                    _prerequisitesLines[i].material.SetFloat(_reachedProperty, 1f);
                else //if prerequisite is not unlocked
                    _prerequisitesLines[i].material.SetFloat(_reachedProperty, 0f);
            }
            if (CheckRequisites()) //if all prerequisites are unlocked
                _icon.material.SetFloat(_reachedProperty, 1f);
            else //if not all prerequisites are unlocked
                _icon.material.SetFloat(_reachedProperty, 0f);
        }
    }

    public void OnClick() //Try to unlock the skill when the node is clicked
    {
        if (CheckRequisites())
        {
            SkillManager.instance.TryAddSkill(_skill);//Try to add the skill to the skill manager
        }
        else
        {
            //TODO: Display message that requisites are not met
            Debug.Log("Cannot unlock skill: requisites not met");
        }
        //close the tooltip after clicking
        _expansion.SetActive(false);
    }

    private bool CheckRequisites()//Check if all prerequisites are unlocked and return true if they are, false if not
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

    public void OnHoverEnter() //Show the tooltip when hovering over the node
    {
        _expansion.SetActive(true);
    }

    public void OnHoverExit() //Hide the tooltip when no longer hovering over the node
    {
        _expansion.SetActive(false);
    }
}

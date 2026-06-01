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

    private void Awake()
    {
        _expansion = transform.Find("Expansion").gameObject;
        _nameText = _expansion.transform.Find("Name").GetComponent<TMP_Text>();
        _descriptionText = _expansion.transform.Find("Description").GetComponent<TMP_Text>();
        _costText = _expansion.transform.Find("Cost").GetComponent<TMP_Text>();
        _prerequisitesLines = transform.Find("Prerequisites").GetComponentsInChildren<Image>();
        _icon = transform.Find("Icon").GetComponent<Image>();
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
                line.color = Color.white;

            _icon.color = Color.white;
        }
        else
        {
            for (int i = 0; i < _skill.prerequisites.Count; i++)
            {
                if (SkillManager.instance.HasSkill(_skill.prerequisites[i]))
                    _prerequisitesLines[i].color = new Color(0.7f, 0.7f, 0.7f);
                else
                    _prerequisitesLines[i].color = new Color(0.4f, 0.4f, 0.4f);
            }
            if (CheckRequisites())
                _icon.color = Color.gray;
            else
                _icon.color = Color.black;
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

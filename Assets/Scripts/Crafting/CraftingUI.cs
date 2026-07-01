using Crafting;
using Skilltree;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class CraftingUI : MonoBehaviour
{
    private TMP_Dropdown _recipeDropdown;
    private Image _dropdownImg;
    [SerializeField] CraftingSlot[] _slots;
    private GameObject _thirdIngrediant;
    [SerializeField] TMP_Text _timeText;
    [SerializeField] Image _progressbar;
    private Image _background;

    private Button _startButton;
    private Image _startButtonImg;

    void Awake()
    {
        _recipeDropdown = GetComponentInChildren<TMP_Dropdown>();
        _dropdownImg = _recipeDropdown.GetComponent<Image>();
        _startButton = GetComponentInChildren<Button>();
        _startButton.onClick.AddListener(StartCraft);
        _startButtonImg = _startButton.GetComponent<Image>();
        _background = transform.Find("CraftingEquation").GetComponent<Image>();
        _thirdIngrediant = _background.transform.Find("ThirdIngrediant").gameObject;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwitchShowCrafting(CraftingManager.instance.CraftingAvailable());
    }

    void OnEnable()
    {
        if (CraftingManager.instance?.currentRecipe != null)
        {
            SetRecipeUi();
        }
    }

    public void SwitchShowCrafting(bool show)
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(show);

        transform.GetChild(0).gameObject.SetActive(!show);
    }

    public void UpdateDropdown(List<string> options, int value)
    {
        _recipeDropdown.ClearOptions();
        _recipeDropdown.AddOptions(options);
        _recipeDropdown.value = value;
        _recipeDropdown.RefreshShownValue();
    }

    public void SetRecipe(int index)
    {
        if (CraftingManager.instance.isCrafting)
        {
            _recipeDropdown.value = CraftingManager.instance.GetCurrentRecipeIndex();
            _recipeDropdown.RefreshShownValue();
            FailFeedbackManager.instance.ShowFailFeedbackUI(_dropdownImg.sprite, _recipeDropdown.gameObject);
            return;
        }
        CraftingManager.instance.SetCurrentRecipe(index);
        SetRecipeUi();
    }

    private void SetRecipeUi()
    {
        if (CraftingManager.instance.isCrafting) return;
        Recipe recipe = CraftingManager.instance.currentRecipe;
        int slotAmount = recipe.slotSprites.Length;
        _thirdIngrediant.SetActive(slotAmount > 3);
        for(int i = 0; i < slotAmount; i++)
        {
            _slots[i].itemShape.sprite = recipe.slotSprites[i];
            _slots[i].UpdateItemSlot(null);
        }
        _timeText.text = recipe.craftTime.ToString() + " sec";
        Color color;
        ColorUtility.TryParseHtmlString("#837EA3", out color);
        _background.color = SkillManager.instance.HasSkill(recipe.requiredSkill) ? color : Color.grey;
        _startButton.interactable = SkillManager.instance.HasSkill(recipe.requiredSkill);
        UpdateProgressbar(0);
    }

    public void UpdateResultShape()
    {
        _slots[0].itemShape.sprite = CraftingManager.instance.currentRecipe.slotSprites[0];
    }

    public void UpdateProgressbar(float progress)
    {
        _progressbar.fillAmount = progress;
    }

    public void StartCraft()
    {
        if (CraftingManager.instance.isCrafting || !CraftingManager.instance.CheckIngrediants()) 
        {
            FailFeedbackManager.instance.ShowFailFeedbackUI(_startButtonImg.sprite, _startButton.gameObject);
            return; 
        }
        CraftingManager.instance.StartCraft();
    }

    public void FillResultSlot(ItemData result)
    {
        int slotAmount = CraftingManager.instance.currentRecipe.slotSprites.Length;
        for (int i = 1; i < slotAmount; i++)
        {
            _slots[i].RemoveItem();
        }

        _slots[0].UpdateItemSlot(result);
    }
}

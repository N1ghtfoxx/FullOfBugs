using System.Collections;
using UnityEngine;

public class Consumables : Singleton<Consumables>
{
    [SerializeField] GameObject _glowEffect;
    [SerializeField] int _strawberryHealAmount = 1;
    [SerializeField] int _healingPotionHealAmount = 3;
    [SerializeField] float _glowPotionDuration = 45f;
    [SerializeField] float _glowPotionPlusDuration = 90f;

    public void UseConsumable(string consumableName)
    {
        switch (consumableName)
        {
            case "Strawberry":
                UseHealing(_strawberryHealAmount);
                break;
            case "HealingPotion":
                UseHealing(_healingPotionHealAmount);
                break;
            case "HealingPotionPlus":
                UseHealing(PlayerStatsManager.instance.maxHp);
                break;
            case "GlowPotion":
                ActivateGlow(_glowPotionDuration);
                break;
            case "GlowPotionPlus":
                ActivateGlow(_glowPotionPlusDuration);
                break;
            default:
                Debug.Log("Consumable not recognized.");
                break;
        }
    }

    [ContextMenu("Use GLow Potion")]
    public void UseGlowPotion()
    {
        UseConsumable("GlowPotion");
    }

    private void UseHealing(int stregnth)
    {
        PlayerStatsManager.instance.modifyHp(stregnth);
    }

    private void ActivateGlow(float time)
    {
        StartCoroutine(GlowRoutine(time));
    }

    private IEnumerator GlowRoutine(float time)
    {
        _glowEffect.SetActive(true);
        while (time > 0)
        {
            if (!PauseManager.instance.isPaused)
                time -= Time.deltaTime;

            yield return null;
        }
        _glowEffect.SetActive(false);
    }
}

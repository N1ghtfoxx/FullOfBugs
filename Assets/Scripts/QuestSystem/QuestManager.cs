using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    [Header("Quest System Settings")]
    [SerializeField] private List<Quest> quests = new List<Quest>();
    [SerializeField] private GameObject questUI;
    [SerializeField] private GameObject newQuestIndicator;
    [SerializeField] private float indicatorDuration = 3f;

    public void StartQuest(QuestID questID)
    {
        Quest quest = quests.Find(q => q.QuestId == questID);
        if (quest != null)
        {
            Debug.Log($"Quest '{quest.QuestId}' started!");
            // Show the new quest indicator
            newQuestIndicator.SetActive(true);

            // Hide the indicator after a delay
            StartCoroutine(HideQuestIndicator());
        }
        else
        {
            Debug.LogWarning($"Quest with ID '{questID}' not found!");
        }
    }

    private IEnumerator HideQuestIndicator()
    {
        yield return new WaitForSeconds(indicatorDuration);
        questUI.SetActive(false);
    }
}

public enum QuestID
{
    None,
    HermbertHelp,
    Combat,
    Farming,
    Crafting
}

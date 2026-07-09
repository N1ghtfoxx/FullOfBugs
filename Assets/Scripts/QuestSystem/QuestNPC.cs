using System.Collections.Generic;
using UnityEngine;

public class QuestNPC : DialogueNpc
{
    [Header("Quest System Settings")]
    // use an questnpc id if you have more than 1 questnpc
    [SerializeField] List<QuestID> questsToGive = new();
    private List<QuestID> _activeQuests = new();
    private List<QuestID> _completedQuests = new();
    private List<QuestID> _waitingDialogs = new();

    public override void Interact()
    {
        if (DialogueManager.instance.isDialogueActive)
            return;

        if (questsToGive.Count > 0)
        {
            QuestID questToGive = questsToGive[0];
            _activeQuests.Add(questToGive);
            questsToGive.Remove(questToGive);
            _waitingDialogs.Add(questToGive);
            if (QuestManager.instance.StartQuest(questToGive))
            {
                PauseManager.instance.SetPause();
                return;
            }
        }

        foreach (QuestID q in _completedQuests)
        {
            if (QuestManager.instance.CollectQuest(q))
            {
                _completedQuests.Remove(q);
                PauseManager.instance.SetPause();
                return;
            }
        }

        if(Random.value < 0.5f) 
        {
            foreach (QuestID q in _waitingDialogs)
            {
                _waitingDialogs.Remove(q);
                if (QuestManager.instance.WaitForQuestCompletion(q))
                {
                    _waitingDialogs.Add(q);
                    PauseManager.instance.SetPause();
                    return;
                }
            }
        }


        base.Interact();
    }

    public void AddQuest(QuestID questID)
    {
        if (!questsToGive.Contains(questID))
        {
            questsToGive.Add(questID);
        }
    }

    public void QuestCompleted(QuestID quest)
    {
        if (_activeQuests.Contains(quest))
        {
            _activeQuests.Remove(quest);
            _completedQuests.Add(quest);
        }
    }
}

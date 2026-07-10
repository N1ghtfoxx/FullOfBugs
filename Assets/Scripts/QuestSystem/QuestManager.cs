using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    [Header("Quest System Settings")]
    [SerializeField] private List<Quest> _quests = new List<Quest>();
    [SerializeField] private GameObject questUI;
    [SerializeField] private GameObject newQuestIndicator;
    [SerializeField] private float indicatorDuration = 3f;
    //make it a list if you have more than one and take the right one by his id
    [SerializeField] QuestNPC _questNPC;

    private List<Key> _collectedKeys = new List<Key>();

    public void DiscoverQuest(QuestID questID)
    {
        Quest quest = _quests.Find(q => q.QuestId == questID);
        if (quest != null)
        {
            if(quest.questState == QuestState.Unknown)
            {
                quest.questState = QuestState.Known;
                _questNPC.AddQuest(questID);
            }
        }
    }

    public bool StartQuest(QuestID questID)
    {
        bool hasDialog = false;
        Quest quest = _quests.Find(q => q.QuestId == questID);
        if (quest != null)
        {
            if (quest.questState == QuestState.Known)
            {
                hasDialog = quest.StartQuest();
            }
            else return false;

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
        return hasDialog;
    }

    public bool WaitForQuestCompletion(QuestID questID)
    {
        Quest quest = _quests.Find(q => q.QuestId == questID);
        if (quest != null)
        {
            return quest.WaitForQuestCompletion();
        }
        else
        {
            Debug.LogWarning($"Quest with ID '{questID}' not found!");
            return false;
        }
    }

    public bool CollectQuest(QuestID questID)
    {
        Quest quest = _quests.Find(q => q.QuestId == questID);
        if (quest != null)
        {
            if (quest.IsCompleted)
            {
                return quest.CollectQuest();
            }
            else
            {
                Debug.LogWarning($"Quest '{questID}' is not completed yet!");
                return false;
            }
        }
        else
        {
            Debug.LogWarning($"Quest with ID '{questID}' not found!");
            return false;
        }
    }

    public void CompleteQuest(QuestID quest)
    {
        _questNPC.QuestCompleted(quest);
    }

    public void UpdAllQuestObjByType(ObjectiveType type, int count)
    {
        foreach (Quest quest in _quests)
        {
            quest.UpdObjByType(type, count);
        }
    }

    public void UpdAllQuestObjByName(string name, int count)
    {
        foreach (Quest quest in _quests)
        {
            quest.UpdObjByName(name, count);
        }
    }

    private IEnumerator HideQuestIndicator()
    {
        yield return new WaitForSeconds(indicatorDuration);
        questUI.SetActive(false);
    }

    public void CollectKey(Key k)
    {
        if (!_collectedKeys.Contains(k))
        {
            _collectedKeys.Add(k);
            Debug.Log($"Collected key: {k.keyId}");
        }
    }

    public bool TryOpenLock(LockID lockId)
    {
        if ((int)lockId > GetQuest(QuestID.HermbertHelp).Progress) return false;
        if( _collectedKeys.Exists(k => k.lockId == lockId))
        {
            _collectedKeys.Remove(_collectedKeys.Find(k => k.lockId == lockId));
            return true;
        }
        return false;
    }

    private Quest GetQuest(QuestID id)
    {
        return _quests.Find(k => k.QuestId == id);
    }

    public bool HasKey(KeyID keyId)
    {
        return _collectedKeys.Exists(k => k.keyId == keyId);
    }

    #region Reward functions

    public void HelpHermbertReward()
    {
        Debug.Log("Your QuestReward Plays Now");
    }

    #endregion
}

public enum QuestID
{
    None,
    HermbertHelp,
    Combat,
    Farming,
    Crafting
}

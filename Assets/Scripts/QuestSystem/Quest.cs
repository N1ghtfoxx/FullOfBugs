using Ink.Parsed;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Quest
{
    [Header("Quest Settings")]
    [SerializeField] private QuestID questID;
    [SerializeField] List<Objective> _objectives = new();
    private bool isCompleted => _objectives.TrueForAll( o => o.isCompleted );
    [SerializeField] private TextAsset startDialogInk;
    [SerializeField] TextAsset endDialogInk;

    [SerializeField] UnityEvent _rewardEvent;

    public QuestState questState;

    public QuestID QuestId => questID;
    public bool IsCompleted => isCompleted;

    public bool StartQuest()
    {
        if (startDialogInk != null)
        {
            DialogueManager.instance.StartDialogue(startDialogInk);
        }
        questState = QuestState.Started;
        return startDialogInk != null;
    }

    public bool WaitForQuestCompletion()
    {
        TextAsset dialogInk;
        dialogInk = _objectives.Find(o => !o.isCompleted && o.waitingForDialogInk != null)?.waitingForDialogInk;
        if (dialogInk != null)
            DialogueManager.instance.StartDialogue(dialogInk);
        return dialogInk != null;
    }

    public bool CollectQuest()
    {
        if (endDialogInk != null)
        {
            DialogueManager.instance.StartDialogue(endDialogInk, false ,_rewardEvent);
        }
        //_rewardEvent?.Invoke();
        return endDialogInk != null;
    }

    public void UpdObjByType(ObjectiveType type, int count)
    {
        Objective objective = _objectives.Find(o => o.type == type);
        if (objective != null)
        {
            objective.currentCount += count;
            Debug.Log($"Objective '{objective.name}' updated: {objective.currentCount}/{objective.requiredCount}");

            CheckCompletion();
        }
    }

    public void UpdObjByName(string name, int count)
    {
        Objective objective = _objectives.Find(o => o.name == name);
        if (objective != null)
        {
            objective.currentCount += count;
            Debug.Log($"Objective '{objective.name}' updated: {objective.currentCount}/{objective.requiredCount}");

            CheckCompletion();
        }
    }

    private void CheckCompletion()
    {
        if (isCompleted)
        {
            QuestManager.instance.CompleteQuest(questID);
            questState = QuestState.Completed;
        }
    }
}

[System.Serializable]
public class Objective
{
    public string name;
    public bool isCompleted => currentCount >= requiredCount;
    public ObjectiveType type;
    public int requiredCount;
    public int currentCount;

    public TextAsset waitingForDialogInk;
}

public enum ObjectiveType
{

}

public enum QuestState
{
    Unknown,
    Known,
    Started,
    Completed
}
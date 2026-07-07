using Ink.Parsed;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    [Header("Quest Settings")]
    [SerializeField] private QuestID questID;
    private bool isCompleted;
    private Dictionary<string, bool> _objectives;
    [SerializeField] private TextAsset startDialogInk;
    public QuestID QuestId => questID;
    public bool IsCompleted => isCompleted;
    public void CompleteQuest()
    {
        isCompleted = true;
        Debug.Log($"Quest '{questID}' completed!");
    }
}

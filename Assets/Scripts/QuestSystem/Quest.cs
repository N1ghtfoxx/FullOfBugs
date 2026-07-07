using Ink.Parsed;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    [Header("Quest Settings")]
    [SerializeField] private string questName;
    private bool isCompleted;
    private Dictionary<string, bool> _objectives;
    [SerializeField] private TextAsset startDialogInk;
    public string QuestName => questName;
    public bool IsCompleted => isCompleted;
    public void CompleteQuest()
    {
        isCompleted = true;
        Debug.Log($"Quest '{questName}' completed!");
    }
}

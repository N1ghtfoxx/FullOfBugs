using System.Collections;
using UnityEngine;

public class HermbertMovement : MonoBehaviour
{
    [SerializeField] Questpoint[] questpoints;
    private int currentQuestpointIndex = 0;

    [SerializeField] float _speed = 1;

    public void MoveHermbert()
    {
        StopAllCoroutines();
        StartCoroutine(MoveThroughWaypoints());
    }

    private IEnumerator MoveThroughWaypoints()
    {
        foreach (Transform wp in questpoints[currentQuestpointIndex].waypoints)
        {
            while (Vector3.Distance(transform.position, wp.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, wp.position, Time.deltaTime * _speed);
                yield return null;
            }
            transform.position = wp.position;
        }
        currentQuestpointIndex++;
        string objName = "";
        switch (currentQuestpointIndex)
        {
            case 1:
                objName = "GreenDoor";
                break;
            case 2:
                objName = "OrangeDoor";
                break;
            case 3:
                objName = "YellowDoor";
                break;
            case 4:
                objName = "BlueDoor";
                break;
            case 5:
                objName = "RedDoor";
                break;
        }
        QuestManager.instance.UpdAllQuestObjByName(objName, 1);
        Debug.Log(currentQuestpointIndex);
    }
}

[System.Serializable]
public class Questpoint
{
    public Transform[] waypoints;
}
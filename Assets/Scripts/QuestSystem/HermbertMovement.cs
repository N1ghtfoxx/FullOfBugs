using System.Collections;
using UnityEngine;

public class HermbertMovement : MonoBehaviour
{
    [SerializeField] Questpoint[] questpoints;
    private int currentQuestpointIndex = 0;

    [SerializeField] float _speed = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        Debug.Log(currentQuestpointIndex);
    }
}

[System.Serializable]
public class Questpoint
{
    public Transform[] waypoints;
}
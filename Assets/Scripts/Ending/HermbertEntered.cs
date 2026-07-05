using UnityEngine;

public class HermbertEntered : MonoBehaviour
{
    //Dialog

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Hermbert")
        {
            Interaction();
        }
    }


    protected virtual void Interaction()
    {
        Debug.Log("Hermbert Says something");

    }
}

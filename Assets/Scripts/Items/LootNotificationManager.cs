using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootNotificationManager : Singleton<LootNotificationManager>
{
    [SerializeField] private ShowItemUI notificationPrefab;
    [SerializeField] private Transform parent;
    [SerializeField] private float visibleDuration = 2f;

    private Dictionary<string, ShowItemUI> activeNotifications = new();
    private Dictionary<string, Coroutine> activeTimers = new();

    public void ShowNotification(ItemData itemData)
    {
        /*ShowItemUI notification = Instantiate(notificationPrefab, parent);
        notification.Setup(itemData);
        Destroy(notification.gameObject, visibleDuration);*/

        
        if(activeNotifications.TryGetValue(itemData.name, out ShowItemUI notification))
        {
            // if item is already visible increase quantity and reset the timer 
            notification.AddQuantity(itemData.quantity);
            StopCoroutine(activeTimers[itemData.name]);
            activeTimers[itemData.name] = StartCoroutine(RemoveAfterTime(itemData.name));
        }
        else
        {
            // if item is not visible already, instantiate new notification
            notification = Instantiate(notificationPrefab, parent);
            notification.Setup(itemData);

            // Add the item to the dictionary and start the timer
            activeNotifications.Add(itemData.name, notification);
            activeTimers.Add(itemData.name, StartCoroutine(RemoveAfterTime(itemData.name)));
        }

    }

    private IEnumerator RemoveAfterTime(string itemName)
    {
        // wait for the specified duration before removing the notification
        yield return new WaitForSeconds(visibleDuration);

        // remove the notification from the activeNotifications dictionary and destroy the GameObject
        Destroy(activeNotifications[itemName].gameObject);
        activeNotifications.Remove(itemName);
        activeTimers.Remove(itemName);
    }


}

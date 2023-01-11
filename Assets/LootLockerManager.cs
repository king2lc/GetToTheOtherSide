using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LootLocker.Requests;

public class LootLockerManager : MonoBehaviour
{
    public void StartSession()
    {
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                SceneManager.LoadScene(2);
            }
            else
            {
                Debug.Log("SAD");
            }
        });

    }
}

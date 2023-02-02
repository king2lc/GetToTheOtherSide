using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenerScipt : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var Pause = UnityEngine.Input.GetKeyDown(KeyCode.Escape);

        if (!Pause)
        {
            Pause = true;
        }
        else
            Pause = false;

    }
}

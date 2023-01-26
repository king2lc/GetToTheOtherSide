using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector2 screenPosistion = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 relativeMousePosistion = Camera.main.ScreenToWorldPoint(screenPosistion);
        transform.position = relativeMousePosistion;
    }
}

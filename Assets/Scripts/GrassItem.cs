using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassItem : MonoBehaviour
{
    public int ItemID;
    private LevelEditManager levelEditManager;

    // Start is called before the first frame update
    void Start()
    {
        //levelEditManager = GameObject.FindGameObjectWithTag("LevelEditManager").GetComponent<LevelEditManager>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(this.gameObject);
            levelEditManager.ItemButtons[ItemID].currentQuantity++;
            levelEditManager.ItemButtons[ItemID].currentQuantityText.text = levelEditManager.ItemButtons[ItemID].currentQuantity.ToString();
        }
    }
}

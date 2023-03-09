using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public GameObject[] playerPrefabs;
    public void Awake(){
        PlayerPrefs.GetInt("SelectedCharacter",0)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

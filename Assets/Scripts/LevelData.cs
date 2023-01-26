using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;

public class LevelData : MonoBehaviour
{
    public int levelID = 0;
    public string levelName = "";
    public Text levelNameText;
    public Image levelIcon;
    public string textFileURL = "";
    public GameObject level;

    private void Start()
    {
        transform.position = Vector3.zero;
        transform.localPosition = Vector3.one;

        levelNameText.text = levelName;
        levelIcon.sprite = levelIcon.sprite;
    }

    public void LoadLevel()
    {
        StartCoroutine(DownloadText(textFileURL));
    }

    private IEnumerator DownloadText(string textFileURL)
    {
        var webRequest = UnityWebRequest.Get(textFileURL);
        yield return webRequest.SendWebRequest();

        var path = "/Assets/CreatedLevels/Level-Data.txt";
        File.WriteAllText(path, webRequest.downloadHandler.text);

        AssetDatabase.Refresh();
        yield return new WaitForSeconds(1f);
        GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelSaver>().LoadLevel();
        yield return new WaitForSeconds(2f);
        GameObject.FindGameObjectWithTag("DownloadMenu").SetActive(false);

    }
}

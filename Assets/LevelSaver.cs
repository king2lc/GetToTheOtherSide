using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelSaver : MonoBehaviour
{
    public string savableTag = "Save";
    private GameObject[] assets;
    [SerializeField] private string[] assetNames;
    [SerializeField] private Vector3[] assetPosistions;
    private string levelPath = Directory.GetCurrentDirectory() + "/Assets/CreatedLevels/Level-Data.txt";
    public void FindAssets()
    {
        if(assets != null)
        {
            foreach(var asset in assets)
            {
                Destroy(asset);
            }
        }
        assets = GameObject.FindGameObjectsWithTag(savableTag);

        assetNames = new string[assets.Length];
        assetPosistions = new Vector3[assets.Length];

        for(int i = 0; i < assets.Length; i++)
        {
            assetNames[i] = assets[i].name;
            assetPosistions[i] = assets[i].transform.position;
        }
        SaveToTextFile();
    }

    public void SaveToTextFile()
    {
        var writer = new StreamWriter(levelPath, false);
        var fileInfo = new FileInfo(levelPath);
        if (!fileInfo.Exists)
        {
            System.IO.Directory.CreateDirectory(fileInfo.DirectoryName);
        }
        for (int i = 0; i < assets.Length; i++)
        {
            string info = assetNames[i] + ',' + assetPosistions[i].x.ToString() + ',' + 
                assetPosistions[i].y.ToString() + ',' + assetPosistions[i].z.ToString();
            writer.WriteLine(info);
        }
        writer.Close();
    }
}

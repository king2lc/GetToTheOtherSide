using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class LevelSaver : MonoBehaviour
{
    public string savableTag = "Savable";
    public string chestTag = "EndGameChest";
    public List<string> tags = new List<string>() { "Savable", "EndGameChest"};
    private GameObject[] assets;
    private GameObject[] chestAsset;
    private GameObject[] finalAsset;
    public GameObject[] possibleAssets;
    [SerializeField] private string[] assetNames;
    [SerializeField] private Vector3[] assetPosistions;
    private string levelPath = Directory.GetCurrentDirectory() + "/Assets/CreatedLevels/Level-Data.txt";
    public void FindAssets()
    {
        if(finalAsset != null)
        {
            foreach(var asset in assets)
            {
                Destroy(asset);
            }
        }
        assets = GameObject.FindGameObjectsWithTag(savableTag);
        chestAsset = GameObject.FindGameObjectsWithTag(chestTag);
        finalAsset = assets.Concat(chestAsset).ToArray();
        assetNames = new string[finalAsset.Length];
        assetPosistions = new Vector3[finalAsset.Length];

        for(int i = 0; i < finalAsset.Length; i++)
        {
            if(finalAsset[i].name.Contains("Clone") && finalAsset[i].name.Contains("Chest"))
            {
                finalAsset[i].name = "EndLevelChest";
            }

            assetNames[i] = finalAsset[i].name;
            assetPosistions[i] = finalAsset[i].transform.position;
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
        for (int i = 0; i < finalAsset.Length; i++)
        {
            string info = assetNames[i] + ',' + assetPosistions[i].x.ToString() + ',' + 
                assetPosistions[i].y.ToString() + ',' + assetPosistions[i].z.ToString();
            writer.WriteLine(info);
        }
        writer.Close();
    }

    public void LoadLevel()
    {
        //foreach(var savableObject in GameObject.FindGameObjectsWithTag("Savable"))
        //{
        //    Destroy(savableObject);
        //}
        var count = 0;
        using (var reader = new StreamReader(levelPath))
        {
            var s = string.Empty;

            while((s = reader.ReadLine()) != null)
            {
                count++;
            }
            assetNames = new string[0];
            assetPosistions = new Vector3[0];

            assetNames = new string[count];
            assetPosistions = new Vector3[count];
        }

        using (var reader = new StreamReader (levelPath))
        {
            while(!reader.EndOfStream)
            {
                for(int i = 0; i < count; i++)
                {
                    var data = reader.ReadLine().Split(',');

                    assetNames[i] = data[0];
                    assetPosistions[i].x = float.Parse(data[1]);
                    assetPosistions[i].y = float.Parse(data[2]);
                    assetPosistions[i].z = float.Parse(data[3]);
                }
            }
        }
        //File.Delete(levelPath);
        CreateAssets();
    }
    public void CreateAssets()
    {
        for (int i = 0; i < assetNames.Length; i++)
        {
            for (int j = 0; j < possibleAssets.Length; j++)
            {
                if (possibleAssets[j].name == assetNames[i])
                {
                    Instantiate(possibleAssets[j], assetPosistions[i], Quaternion.identity);
                }
            }
        }
    }
}

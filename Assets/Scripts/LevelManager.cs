using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LootLocker.Requests;
using UnityEngine.Networking;

public class LevelManager : MonoBehaviour
{
    public TMPro.TMP_InputField levelNameInput;
    public GameObject levelUploadUI;

    public GameObject levelDownload;
    public Transform levelContent;

    private string screenShotPath = Directory.GetCurrentDirectory() + "/Assets/Screenshot/Screenshot.png";
    private string levelPath = Directory.GetCurrentDirectory() + "/Assets/CreatedLevels/Level-Data.txt";
    public void LevelCreation()
    {
        var levelName = levelNameInput.text;
        LootLockerSDKManager.CreatingAnAssetCandidate(levelName, (response ) =>
        {
            if (response.success)
            {
                UploadLevelUI();
                UploadData(response.asset_candidate_id);
            }
            else
            {
                Debug.Log("There was an error creating the level.");
            }
        });
    }

    IEnumerator WaitScreenshot()
    {
        var fileInfo = new FileInfo(screenShotPath);
        if (!fileInfo.Exists)
        {
            System.IO.Directory.CreateDirectory(fileInfo.DirectoryName);
        }    
        ScreenCapture.CaptureScreenshot(screenShotPath);
        GetComponent<LevelSaver>().FindAssets();
        yield return new WaitForSeconds(1f);
        levelUploadUI.SetActive(true);
    }

    public void UploadLevelUI()
    {
        StartCoroutine(WaitScreenshot());
    }

    public void UploadData(int levelID)
    {
        LootLocker.LootLockerEnums.FilePurpose thumbnail = LootLocker.LootLockerEnums.FilePurpose.primary_thumbnail;

        LootLockerSDKManager.AddingFilesToAssetCandidates(levelID, screenShotPath, "Screenshot", thumbnail, (response) =>
        {
            if (response.success)
            {
                LootLocker.LootLockerEnums.FilePurpose textFileType = LootLocker.LootLockerEnums.FilePurpose.file;

                LootLockerSDKManager.AddingFilesToAssetCandidates(levelID, levelPath, "LevelData.txt", textFileType, (textResponse) =>
                {
                    if(textResponse.success)
                    {
                        LootLockerSDKManager.UpdatingAnAssetCandidate(levelID, true, (updatedResponse) => { });
                    }
                    else
                    {
                        Debug.Log("There was an error creating the level.");
                    }
                });
            }
            else
            {
                Debug.Log("There was an error uploading screenshot.");
            }
        });
        File.Delete(screenShotPath);
        //File.Delete(levelPath);
    }

    public void DownloadData()
    {
        LootLockerSDKManager.GetAssetListWithCount(20, (response) =>
        {
            for(int i = 0; i < response.assets.Length; i++)
            {
                var item = Instantiate(levelDownload, transform.position, Quaternion.identity);
                item.transform.SetParent(levelContent);

                item.GetComponent<LevelData>().levelID = i;
                item.GetComponent<LevelData>().levelName = response.assets[i].name;

                var levelData = response.assets[i].files;
                StartCoroutine(LoadIcon(levelData[0].url.ToString(), item.GetComponent<LevelData>().levelIcon));
                item.GetComponent<LevelData>().textFileURL = levelData[1].url.ToString();
            }
        }, null, true);
        
    }
    IEnumerator LoadIcon(string imageURL, Image image)
    {
        var request = UnityWebRequestTexture.GetTexture(imageURL);
        yield return request.SendWebRequest();

        var imageDownload = DownloadHandlerTexture.GetContent(request);
        image.sprite = Sprite.Create(imageDownload, new Rect(0f, 0f, imageDownload.width, imageDownload.height), Vector2.zero);
    }
}

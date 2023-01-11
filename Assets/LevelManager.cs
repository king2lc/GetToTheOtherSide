using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LootLocker.Requests;

public class LevelManager : MonoBehaviour
{
    public InputField levelNameInputField;
    public string levelName;
    public GameObject levelUploadUI;

    private string screenShotPath = Directory.GetCurrentDirectory() + "/Assets/Screenshot/Screenshot.png";
    public void LevelCreation()
    {
        levelName = "test";
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
        yield return new WaitForSeconds(.1f);
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
                string levelPath = Directory.GetCurrentDirectory() + "/Assets/CreatedLevels/Level-Data.txt";
                LootLocker.LootLockerEnums.FilePurpose textFileType = LootLocker.LootLockerEnums.FilePurpose.file;

                LootLockerSDKManager.AddingFilesToAssetCandidates(levelID, screenShotPath, "Level-Data.txt", textFileType, (textResponse) =>
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
    }
}

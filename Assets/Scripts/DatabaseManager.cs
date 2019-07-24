using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;
using Newtonsoft.Json;
using System.IO;

public class DatabaseManager : MonoBehaviour
{
    private string appKey = "Bo06WUzU1lIKhlwN4XQ6K3X2PwQqw5ldBYaRKENS";
    private string restAPIKey = "nYG3RwwlyQ5fd5KqbZ8uFEK7qrqAq0eZ3OXfh5Jg";
    private string appURL = "https://parseapi.back4app.com/classes/Dragon";

    [SerializeField] private TextMeshProUGUI dragonName;

    public void GetDragon(string dragonID)
    {
        StartCoroutine(GET_Dragon(dragonID, SetDragonName));
    }

    private void SetDragonName(string JSONData)
    {
        dragonName.text = JSONData; //TODO : do not show all the JSON data, just the name

        //Create a new dragon
        SDragon newDragon = new SDragon();
        newDragon.objectID = "h702IhcZEd";
        newDragon.element = EElement.fire;
        newDragon.sex = ESex.male;
        newDragon.spikesNumber = UnityEngine.Random.Range(1, 255);

        //transform the dragon data structure to a JSON string
        string JSON = JsonConvert.SerializeObject(newDragon);

        //write all the JSON string into a file in /Assets/StreamingAssets
        File.WriteAllText(Application.streamingAssetsPath + "/" + newDragon.objectID+".json", JSON);
    }

    /// <summary>
    /// Get a specific dragon from the database
    /// </summary>
    /// <param name="id">the id of the dragon want.</param>
    /// <param name="callback_OnDownloaded">the function called after the dragon's data has been downloaded.</param>
    private IEnumerator GET_Dragon(string _id, Action<string> callback_OnDownloaded)
    {
        //request at https://parseapi.back4app.com/classes/Dragon/_id/name
        UnityWebRequest request = new UnityWebRequest(appURL+"/"+_id+"/name", "GET");
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("X-Parse-Application-Id", appKey);
        request.SetRequestHeader("X-Parse-REST-API-Key", restAPIKey);
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.LogError($"Error while fetching the database : {request.error}");
        }
        else
        {
            callback_OnDownloaded.Invoke(request.downloadHandler.text);
        }
    }
}

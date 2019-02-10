using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    private AssetBundle myLoadedAssetBundle;
    public void Start()
    {
        myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/CustomAssets/Scenes");
    }
    public void LoadByName(string sceneName)
    {
        myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/CustomAssets/Scenes");
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
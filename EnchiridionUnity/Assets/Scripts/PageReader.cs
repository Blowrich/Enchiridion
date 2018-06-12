using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[Serializable]
public class Page
{
    public int Type;
    public string Name;
    public string Picture;
    public string Prefab;
    public string Info;
}


public class PageReader : MonoBehaviour {

    public Text Name;
    public Text Info;
    public Image img;
    public RectTransform content;
    public GameObject PagePanel;
    public Transform pivot;
    public string CurrentArPrefab;
    public GameObject MainPanel;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        content.sizeDelta = new Vector2(content.sizeDelta.x, 215 + Info.rectTransform.sizeDelta.y);
	}

    public static string LoadResourceTextfile(string path)
    {
        string filePath = path.Replace(".json", "");
        TextAsset targetFile = Resources.Load<TextAsset>(filePath);
        return targetFile.text;
    }

    public void OpenPage(string jsonFile)
    {
        PagePanel.SetActive(true);
        string j = LoadResourceTextfile(jsonFile);
        Debug.Log(j);
        Page newPage = new Page();
        newPage = JsonUtility.FromJson<Page>(j);
        Name.text = newPage.Name;
        Info.text = newPage.Info;
        img.sprite = Resources.Load<Sprite>(newPage.Picture);
        //StartCoroutine(LoadImage(newPage.Picture));
        if (CurrentArPrefab != newPage.Prefab)
            CurrentArPrefab = newPage.Prefab;

    }

    IEnumerator LoadImage(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        // assign texture
        float k = www.texture.width / www.texture.height;
        img.sprite = Sprite.Create(www.texture, new Rect(k*200, 200,k*200, 200), new Vector2(0, 0));
    }

    public void OpenAr()
    {
        if (pivot.childCount > 0)
            Destroy(pivot.GetChild(0).gameObject);
        GameObject clone = Instantiate<GameObject>(Resources.Load<GameObject>(CurrentArPrefab), pivot);
        PagePanel.SetActive(false);
        MainPanel.SetActive(false);
    }

    public void CloseAr()
    {
        PagePanel.SetActive(true);
        MainPanel.SetActive(true);
    }

    public void Back()
    {
        PagePanel.SetActive(false);
    }

    public void BackToClasses(int n)
    {
        SceneManager.LoadScene(n);
    }
}

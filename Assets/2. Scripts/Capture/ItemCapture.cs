using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public enum Grade
{
    Normal,
    Uncommon,
    Epic,
    Legend,
}

public enum Size
{
    POT64,
    POT128,
    POT256,
    POT512,
    POT1024,
}

public class ItemCapture : MonoBehaviour
{
    public Camera cam;
    public RenderTexture rt;
    public Image bg;

    public Grade grade;
    public Size size;

    public GameObject[] objects;
    int nowCnt = 0;

    private void Start()
    {
        cam = Camera.main;
        SettingColor();
        SettingSize();
    }

    public void CreateImage()
    {
        StartCoroutine(CaptureImage());
    }

    public void CreateAll()
    {
        StartCoroutine(CaptureAll());
    }

    IEnumerator CaptureImage()
    {
        yield return null;

        Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false, true);
        RenderTexture.active = rt;
        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);

        yield return null;

        var data = tex.EncodeToPNG();
        string name = "Thumbnail";
        string extention = ".png";
        string path = Application.dataPath + "/3. Assets" + "/ItemImages";

        Debug.Log(path);

        if(!Directory.Exists(path)) Directory.CreateDirectory(path);

        File.WriteAllBytes(path + name + extention, data);

        yield return null;
    }

    IEnumerator CaptureAll()
    {
        for(int i=0; i<objects.Length; i++)
        {
            var nowObj = Instantiate(objects[i]);

            yield return null;

            Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false, true);
            RenderTexture.active = rt;
            tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);

            yield return null;

            var data = tex.EncodeToPNG();
            string name = $"{nowObj.name}_Thumbnail";
            string extention = ".png";
            string path = Application.dataPath + "/3. Assets" + "/ItemImages/";

            Debug.Log(path);

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            File.WriteAllBytes(path + name + extention, data);

            yield return null;

            DestroyImmediate(nowObj);

            yield return null;
        }
    }

    void SettingColor()
    {
        switch(grade)
        {
            case Grade.Normal:
                cam.backgroundColor = Color.gray;
                break;
            case Grade.Uncommon:
                cam.backgroundColor = Color.blue;
                break;
            case Grade.Epic:
                cam.backgroundColor = Color.green;
                break;
            case Grade.Legend:
                cam.backgroundColor = Color.yellow;
                break;
            default:
                break;
        }
    }

    void SettingSize()
    {
        switch (size)
        {
            case Size.POT64:
                rt.width = 64;
                rt.height = 64;
                break;
            case Size.POT128:
                rt.width = 128;
                rt.height = 128;
                break;
            case Size.POT256:
                rt.width = 256;
                rt.height = 256;
                break;
            case Size.POT512:
                rt.width = 512;
                rt.height = 512;
                break;
            case Size.POT1024:
                rt.width = 1024;
                rt.height = 1024;
                break;
            default:
                break;
        }

    }
}

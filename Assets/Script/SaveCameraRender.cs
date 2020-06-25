using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//挂载在相机上,按F4截屏，图像存在Image目录下
public class SaveCameraRender : MonoBehaviour
{
    Camera cam;
    RenderTexture renderTexture;
    public void Start()
    {
        if (cam == null)
        {
            cam = this.GetComponent<Camera>();
        }
    }
    private void Update()
    {
        if (cam == null)
        { return; }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            _SaveCamTexture();
        }
    }
    private void _SaveCamTexture()
    {
        renderTexture = cam.targetTexture;
        if (renderTexture != null)
        {
            _SaveRenderTexture(renderTexture);
            renderTexture = null;
        }
        else
        {
            GameObject camGo = new GameObject("camGO");
            Camera tmpCam = camGo.AddComponent<Camera>();
            tmpCam.CopyFrom(cam);
            // rt = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.ARGB32);
            renderTexture = RenderTexture.GetTemporary(Screen.width, Screen.height, 16, RenderTextureFormat.ARGB32);

            tmpCam.targetTexture = renderTexture;
            tmpCam.Render();
            _SaveRenderTexture(renderTexture);
            Destroy(camGo);
            //rt.Release();
            RenderTexture.ReleaseTemporary(renderTexture);
            //Destroy(rt);
            renderTexture = null;
        }

    }
    private void _SaveRenderTexture(RenderTexture rt)
    {
        RenderTexture active = RenderTexture.active;
        RenderTexture.active = rt;
        Texture2D png = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false);
        png.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        png.Apply();
        RenderTexture.active = active;
        byte[] bytes = png.EncodeToPNG();
        string path = string.Format("Assets/Image/rt_{0}_{1}_{2}.png", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Millisecond);
        FileStream fs = File.Open(path, FileMode.Create);
        BinaryWriter writer = new BinaryWriter(fs);
        writer.Write(bytes);
        writer.Flush();
        writer.Close();
        fs.Close();
        Destroy(png);
        png = null;
        Debug.Log("保存成功！" + path);
    }
}

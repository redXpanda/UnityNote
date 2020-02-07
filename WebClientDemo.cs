using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;
using System.IO;
using UnityEngine;

public class WebClientDemo : MonoBehaviour
{

    string url = "http://e.hiphotos.baidu.com/zhidao/pic/item/a2cc7cd98d1001e98debe2acbc0e7bec55e797ba.jpg";


    // Start is called before the first frame update
    void Start()
    {
        WebClient client = new WebClient();
        client.DownloadDataCompleted += DownloadDataCallback;
        client.DownloadProgressChanged += Client_DownloadProgressChanged;


        Uri uri = new Uri(url);
        client.DownloadDataAsync(uri);
    }


    //下载进度
    private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        Debug.Log(e.BytesReceived * 100 / e.TotalBytesToReceive + "%");
    }
    //下载完成
    void DownloadDataCallback(object sender, DownloadDataCompletedEventArgs data)
    {
        try
        {
            if (data.Result.Length > 0 && data.Error == null && data.Cancelled == false)
            {
                using (MemoryStream ms = new MemoryStream(data.Result))
                {
                    byte[] buffer = ms.ToArray();
                    string path = @"D:\Unity-Teacher\Teach_TCP\Assets/iamge.png";
                    File.WriteAllBytes(path, buffer);
                    Debug.Log("下载完成");
                }
            }
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

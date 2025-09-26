using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cResolution : MonoBehaviour
{
    void Start()
    {
        Screen.fullScreen = true;
        //SetResolution();
    }

    void SetResolution()
    {
        //float targetAspect = 9f / 16f; // 기준 비율 (1080x1920)
        //float windowAspect = (float)Screen.width / (float)Screen.height;
        //float scaleHeight = windowAspect / targetAspect;

        //Camera cam = Camera.main;

        //if (scaleHeight < 1.0f)
        //{
        //    Rect rect = cam.rect;

        //    rect.width = 1.0f;
        //    rect.height = scaleHeight;
        //    rect.x = 0;
        //    rect.y = (1.0f - scaleHeight) / 2.0f;

        //    cam.rect = rect;
        //}
        //else
        //{
        //    float scaleWidth = 1.0f / scaleHeight;

        //    Rect rect = cam.rect;

        //    rect.width = scaleWidth;
        //    rect.height = 1.0f;
        //    rect.x = (1.0f - scaleWidth) / 2.0f;
        //    rect.y = 0;

        //    cam.rect = rect;
        //}




        //int setWidth = 1080;
        //int setHeight = 1920;

        //int deviceeWidth = Screen.width;
        //int deviceHeight = Screen.height;

        //Screen.SetResolution(setWidth, (int)((float)deviceHeight / deviceeWidth) * setWidth, true);

        //if ((float)setWidth / setHeight < (float)deviceeWidth / deviceHeight)
        //{
        //    float newWidth = ((float)setWidth / setHeight) / ((float)deviceeWidth / deviceHeight);
        //    Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f);
        //}
        //else
        //{
        //    float newHeight = ((float)deviceeWidth / deviceHeight) / ((float)setWidth / setHeight);
        //    Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight);
        //}




        //Camera cam = GetComponent<Camera>();
        //Rect rect = cam.rect;

        //float scaleHeight = ((float)Screen.width / Screen.height) / ((float)9 / 16);
        //float scaleWidth = 1f / scaleHeight;
        //if (scaleHeight < 1)
        //{
        //    rect.height = scaleHeight;
        //    rect.y = (1f - scaleHeight) / 2f;
        //}
        //else
        //{
        //    rect.width = scaleWidth;
        //    rect.x = (1f - scaleWidth) / 2f;
        //}
        //cam.rect = rect;
    }
}

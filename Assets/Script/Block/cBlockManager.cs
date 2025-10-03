using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class cBlockManager : MonoBehaviour
{
    [SerializeField] GameObject blockParent;
    [SerializeField] GameObject mantle;
    [SerializeField] GameObject[] grounds;

    Queue<GameObject> allBlocks = new Queue<GameObject>();
    List<GameObject> surfaceBlocks = new List<GameObject>();

    const int blockLine = 50;
    const int blockColum = 9;
    const int blockRow = 5;
    const int halfLine = 182;

    int existBlock = 0;
    int maxLine = 0;
    int curLine = 0;

    public int GetSurfaceBlockCount => surfaceBlocks.Count;
    public int GetallBlocksCount => allBlocks.Count;
    public int GetCurLine => curLine;

    void Start()
    {
        //bool isSave = Convert.ToBoolean(PlayerPrefs.GetInt("IsSave"));
        cBlockJson blockJson = new cBlockJson();
        LineData data = blockJson.LoadLineData();
        if (data != null)
        {
            //curLine = PlayerPrefs.GetInt("CurLine");
            //maxLine = PlayerPrefs.GetInt("MaxLine");
            curLine = data.curLine;
            maxLine = data.maxLine;

            LoadSurfaceBlock();
            for (int line = curLine + 1; line < maxLine; line++)
                LoadOtherBlock(line);

            SetSurfaceBlock(existBlock);

            StartCoroutine(ReloadBlocks());
        }
        else
        {
            for (int line = 0; line < blockLine; line++)
                InstanceBlock(line);

            maxLine = blockLine;

            SetSurfaceBlock();

            StartCoroutine(ReloadBlocks());
        }
    }

    void InstanceBlock(int line)
    {
        for (int row = 0; row < blockRow; row++)
        {
            for (int colum = 0; colum < blockColum; colum++)
            {
                GameObject instBlock = BlockPoolManager.instance.Get();
                instBlock.transform.SetParent(blockParent.transform);
                instBlock.transform.position = new Vector3(-16.49f - (colum * 3f), 0f - (line * 0.2f), 1.48f + (row * 3f));
                allBlocks.Enqueue(instBlock);
            }
        }
    }

    void LoadSurfaceBlock()
    {
        cBlockJson json = new cBlockJson();
        List<BlockData> data = new List<BlockData>();
        data = json.LoadBlockData();
        if (data == null)
        { 
            Debug.LogError("json data is null");
            return;
        }

        foreach (var obj in data)
        {
            GameObject instBlock = BlockPoolManager.instance.Get();
            instBlock.transform.SetParent(blockParent.transform);
            instBlock.transform.position = new Vector3(obj.surfacePosX, obj.surfacePosY, obj.surfacePosZ);
            allBlocks.Enqueue(instBlock);
            existBlock++;
        }
    }

    void LoadOtherBlock(int line)
    {
        for (int row = 0; row < blockRow; row++)
        {
            for (int colum = 0; colum < blockColum; colum++)
            {
                GameObject instBlock = BlockPoolManager.instance.Get();
                instBlock.transform.SetParent(blockParent.transform);
                instBlock.transform.position = new Vector3(-16.49f - (colum * 3f), 0f - (line * 0.2f), 1.48f + (row * 3f));
                allBlocks.Enqueue(instBlock);
            }
        }

        cBlockJson json = new cBlockJson();
        json.CleanData();
    }

    void SetSurfaceBlock(int count = 45)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject surfaceBlock = allBlocks.Dequeue();
            cBlock block = surfaceBlock.GetComponent<cBlock>();
            block.OnDestroyed += HandleBlockDestory;
            block.state = BlockState.SURFACE;
            block.ChangeMat();
            surfaceBlocks.Add(surfaceBlock);
        }
    }

    IEnumerator ReloadBlocks()
    {
        while (true)
        {
            if (surfaceBlocks.Count == 0)
            {
                SetSurfaceBlock();
                maxLine++;
                curLine++;
                InstanceBlock(maxLine);

                mantle.transform.position = new Vector3(mantle.transform.position.x, mantle.transform.position.y - 0.2f, mantle.transform.position.z);

                if (curLine >= halfLine)
                {
                    foreach (var ground in grounds)
                        ground.transform.position = new Vector3(ground.transform.position.x, ground.transform.position.y - 0.2f, ground.transform.position.z);
                }
            }
            yield return null;
        }
    }

    void HandleBlockDestory(cBlock block)
    {
        surfaceBlocks.Remove(block.gameObject);
        block.OnDestroyed -= HandleBlockDestory;
    }

    public void DestroyBlock()
    {
        if (surfaceBlocks.Count < 1)
            return;

        foreach (var block in surfaceBlocks)
        {
            cBlock blockCom = block.GetComponent<cBlock>();
            blockCom.InstanteFood();
            blockCom.InstanteParticle();
            blockCom.OnDestroyed -= HandleBlockDestory;
            Destroy(block);
        }
        surfaceBlocks.Clear();
    }

    public void DestroyBlockX5()
    {
        StartCoroutine(FiveLineDestory());
    }

    IEnumerator FiveLineDestory()
    {
        yield return new WaitUntil(() => surfaceBlocks.Count > 0);
        DestroyBlock();
        AudioManager.instance.PlayerSfx(AudioManager.Sfx.DIGGY);
        yield return new WaitUntil(() => surfaceBlocks.Count > 0);
        DestroyBlock();
        AudioManager.instance.PlayerSfx(AudioManager.Sfx.DIGGY);
        yield return new WaitUntil(() => surfaceBlocks.Count > 0);
        DestroyBlock();
        AudioManager.instance.PlayerSfx(AudioManager.Sfx.DIGGY);
        yield return new WaitUntil(() => surfaceBlocks.Count > 0);
        DestroyBlock();
        AudioManager.instance.PlayerSfx(AudioManager.Sfx.DIGGY);
        yield return new WaitUntil(() => surfaceBlocks.Count > 0);
        DestroyBlock();
        AudioManager.instance.PlayerSfx(AudioManager.Sfx.DIGGY);
        AudioManager.instance.PlayerSfx(AudioManager.Sfx.DIGGY);
    }

    void OnApplicationQuit()
    {
        //PlayerPrefs.SetInt("IsSave", 1);
        //PlayerPrefs.SetInt("MaxLine", maxLine);
        //PlayerPrefs.SetInt("CurLine", curLine);
        //cBlockJson json = new cBlockJson();
        //json.SaveBlockData(surfaceBlocks);

        cBlockJson json = new cBlockJson();
        json.SaveLineData(curLine, maxLine);
        json.SaveBlockData(surfaceBlocks);
    }

    void OnApplicationPause(bool pause)
    {
        if (pause)
        {

            //PlayerPrefs.SetInt("IsSave", 1);
            //PlayerPrefs.SetInt("MaxLine", maxLine);
            //PlayerPrefs.SetInt("CurLine", curLine);
            cBlockJson json = new cBlockJson();
            json.SaveLineData(curLine, maxLine);
            json.SaveBlockData(surfaceBlocks);
        }
    }
}

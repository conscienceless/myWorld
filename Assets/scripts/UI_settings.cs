using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Uniblocks;

public class UI_settings : MonoBehaviour
{

    public GameObject panel, newPanel, loadPanel, settingPanel, input, list, inputField;

    private void Awake()
    {
        Screen.SetResolution(1024, 576, false);
    }

    // Use this for initialization
    void Start()
    {    
        newPanel.SetActive(false);
        loadPanel.SetActive(false);
        settingPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void NewBtnClick()
    {
        newPanel.SetActive(true);
        panel.SetActive(false);
    }

    public void ConfirmBtnClick_new()
    {
        string newWorldName = input.GetComponent<InputField>().text;
        StreamWriter sw = new StreamWriter(Application.dataPath + "/Resources/settings.txt", false);
        sw.WriteLine(newWorldName);
        sw.Close();
        SceneManager.LoadScene(1);
    }

    public void CancelBtnClick_new()
    {
        newPanel.SetActive(false);
        panel.SetActive(true);
    }

    public void LoadBtnClick()
    {
        panel.SetActive(false);
        loadPanel.SetActive(true);
        Dropdown dropdown = list.GetComponent<Dropdown>();
        dropdown.options.Clear();
        string path = Application.dataPath + "/Resources/Worlds/";
        string[] worldName = Directory.GetDirectories(path);
        for (int i = 0; i < worldName.Length; i++)
        {
            string[] name = worldName[i].Split('/');
            //Debug.Log(name[name.Length - 1]);
            Dropdown.OptionData newOption = new Dropdown.OptionData(name[name.Length - 1]);
            dropdown.options.Add(newOption);
        }
    }
    public void ConfirmBtnClick_load()
    {
        List<Dropdown.OptionData> dataList = list.GetComponent<Dropdown>().options;
        int index = list.GetComponent<Dropdown>().value;
        string loadWorldName = dataList[index].text;
        StreamWriter sw = new StreamWriter(Application.dataPath + "/Resources/settings.txt", false);
        sw.WriteLine(loadWorldName);
        sw.Close();
        SceneManager.LoadScene(1);
    }

    public void CancelBtnClick_load()
    {
        loadPanel.SetActive(false);
        panel.SetActive(true);
    }

    public void SettingBtnClick()
    {
        panel.SetActive(false);
        settingPanel.SetActive(true);
    }
    /*public void CreateBtnClick()
    {
        GameObject newBlock = (GameObject)Resources.Load("block_1");
        Blocks.Clear();
        GetBlocks();
        //UnityEditor.PrefabUtility.CreatePrefab(GetPath()+GetBlockPath((ushort)Blocks.Count)+".prefab", newBlock);
        //UnityEditor.AssetDatabase.CreateAsset(newBlock, GetPath() + GetBlockPath((ushort)Blocks.Count) + ".prefab");
        Voxel newVoxel = newBlock.GetComponent<Voxel>();
        newVoxel.name = GetBlockPath((ushort)Blocks.Count);
        inputField.GetComponent<Text>().text = newVoxel.name;
    }*/

    public void QuitBtnClick()
    {
        Application.Quit();
    }

    List<GameObject> Blocks = new List<GameObject>();
    public string GetPath()
    {
        return Engine.EngineInstance.GetComponent<Engine>().lBlocksPath;
    }
    public string GetBlockPath(ushort data)
    { // converts block id to prefab path		
        return "block_" + data;
    }
    public void GetBlocks()
    { // populates the Blocks array		
        //Blocks = new GameObject[ushort.MaxValue];
        for (ushort i = 0; i < ushort.MaxValue; i++)
        {
            GameObject block = GetBlock(i);
            if (block != null)
            {
                Blocks.Add(block);
            }
        }
    }
    public GameObject GetBlock(ushort data)
    { // returns the prefab of the block with a given index        
        Object blockObject = Resources.Load(GetBlockPath(data), typeof(Object));
        GameObject block = null;
        if (blockObject != null)
        {
            block = (GameObject)blockObject;
        }
        else
        {
            return null;
        }
        if (block != null && block.GetComponent<Voxel>() != null)
        {
            return block;
        }
        else
        {
            return null;
        }
    }
}

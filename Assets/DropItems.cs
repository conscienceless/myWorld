using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using Uniblocks;

public class DropItems : MonoBehaviour
{

    List<GameObject> Blocks=new List<GameObject>();
    List<string> blocksName=new List<string>();
    public List<ushort> blocksID=new List<ushort>();
    public ushort selectedID;
    //private bool IsInitialized;

    private void Awake()
    {
        //IsInitialized = false;
        Dropdown dropdown = this.GetComponent<Dropdown>();
        dropdown.options.Clear();
        Dropdown.OptionData newdata = new Dropdown.OptionData("");
        dropdown.options.Add(newdata);
        GetBlocks();
        for (ushort i = 0; i < Blocks.Count; i++)
        {
            if (Blocks[i] != null)
            {
                Voxel voxel = Blocks[i].GetComponent<Voxel>();
                blocksName.Add(voxel.VName);
                blocksID.Add(voxel.GetID());
            }
        }
        for (ushort i = 1; i < Blocks.Count; i++)
        {
            Dropdown.OptionData optionData = new Dropdown.OptionData(blocksName[i]);           
            dropdown.options.Add(optionData);
        }
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update () {
        
    }

    public string GetPath()
    {        
        return Engine.EngineInstance.GetComponent<Engine>().lBlocksPath;
    }
    public string GetBlockPath(ushort data)
    { // converts block id to prefab path		
        return /*GetPath() + */"block_" + data;//+ ".prefab";
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

        //Object blockObject = UnityEditor.AssetDatabase.LoadAssetAtPath(GetBlockPath(data), typeof(Object));     
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

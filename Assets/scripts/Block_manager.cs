using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniblocks;

public class Block_manager : MonoBehaviour {

    public float  range = 6;
    private ushort blockID;
    private Transform selectEffect,playerPosition;
    public GameObject canvas;

	// Use this for initialization
	void Start () {
        selectEffect = GameObject.Find("selected block graphics").transform;
        playerPosition = GameObject.Find("player").transform;
        selectEffect.gameObject.SetActive(false);
        blockID = 0;
	}
	
	// Update is called once per frame
	void Update () {
        VoxelInfo info = Engine.VoxelRaycast(Camera.main.transform.position, Camera.main.transform.forward, range, false);
        SelectBlock();
        if (info !=  null)
        {
            SelectedShow(info);
            if (Input.GetMouseButtonDown(0))
                Voxel.DestroyBlock(info);
            if(Input.GetMouseButtonDown(1))
            {
                VoxelInfo newinfo = new VoxelInfo(info.adjacentIndex, info.chunk);
                if (IsOntheBlock(playerPosition, playerPosition.position, info) == true)
                    Voxel.PlaceBlock(newinfo, blockID);
            }
        }
        else
            SelectedShow(info);
	}
    private void SelectedShow(VoxelInfo voxelinfo)
    {
        if (voxelinfo != null)
        {
            selectEffect.gameObject.SetActive(true);
            selectEffect.position = voxelinfo.chunk.VoxelIndexToPosition(voxelinfo.index);
        }
        else
            selectEffect.gameObject.SetActive(false);
    }
    private void SelectBlock()
    {
        for(ushort i=1; i<10; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
                //blockID = i;
                blockID = canvas.GetComponent<UI_Controller>().dropDowns[i-1].GetComponent<DropItems>().selectedID;

        }
    }

    private bool IsOntheBlock(Transform transform,Vector3 vector3,VoxelInfo voxelInfo)
    {
        Vector3 player = new Vector3()
        {
            x = Mathf.RoundToInt(vector3.x),
            y = Mathf.RoundToInt(vector3.y),
            z = Mathf.RoundToInt(vector3.z)
        };
        Vector3 block = voxelInfo.chunk.VoxelIndexToPosition(voxelInfo.index);
        Vector3 newBlock = voxelInfo.chunk.VoxelIndexToPosition(voxelInfo.adjacentIndex);
        Vector3 vectorX = new Vector3(1, 0, 0);
        Vector3 vectorY = new Vector3(0, 1, 0);
        Vector3 vectorZ = new Vector3(0, 0, 1);
        if (player==block && newBlock == block + vectorY)
        {
            transform.position += vectorY;
            return true;
        }
        else if (player == block && newBlock == block - vectorY)
        {
            return false;
        }
        else if (player == block && newBlock == block + vectorX)
        {
            transform.position += vectorX;
            return true;
        }
        else if (player == block && newBlock == block - vectorX)
        {
            transform.position -= vectorX;
            return true;
        }
        else if(player == block && newBlock==block+vectorZ)
        {
            transform.position += vectorZ;
            return true;
        }
        else if(player == block && newBlock==block-vectorZ)
        {
            transform.position -= vectorZ;
            return true;
        }
        else
            return true;
    }
}

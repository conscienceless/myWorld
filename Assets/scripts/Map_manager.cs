using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniblocks;

public class Map_manager : MonoBehaviour
{

    private Transform playerTrans;
    private Index lastIndex;
    // Use this for initialization
    void Start()
    {
        playerTrans = GameObject.Find("player").transform;
        InvokeRepeating("MapBuild", 1, 100f);
    }

    private void MapBuild()
    {
        if (Engine.Initialized == false || ChunkManager.Initialized == false) return;
        Index currentIndex = Engine.PositionToChunkIndex(playerTrans.position);
        if (lastIndex != currentIndex)
            ChunkManager.SpawnChunks(playerTrans.position);
        lastIndex = currentIndex;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using ModestTree;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Tilemaps;
using Debug = UnityEngine.Debug;

public class BlockSpawnerPresenter : MonoBehaviour
{
    public Tilemap walls;
    public Tilemap nonDescructableBlocks;
    public GameObject[] destructableBlocks;

    void Start()
    {
        // Debug.Log($"Min: {nonDescructableBlocks.localBounds.min}");
        // Debug.Log($"Max: {nonDescructableBlocks.localBounds.max}");

        // Debug.Log($"Size: {nonDescructableBlocks.size}");

        var cellBounds = nonDescructableBlocks.cellBounds;
        var xMin = cellBounds.xMin - 1;
        var xMax = cellBounds.xMax + 1;
        var yMin = cellBounds.yMin - 1;
        var yMax = cellBounds.yMax + 1;

        for(int x=xMin; x < xMax; x++)
        for (var y = yMin; y < yMax; y++)
        {
            Debug.Log($"({x},{y}): {nonDescructableBlocks.HasTile(new Vector3Int(x, y, 0))}");
        }

    }

    void Update()
    {
    }
}


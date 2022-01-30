using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ModestTree;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Tilemaps;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;
public class BlockSpawnerPresenter : MonoBehaviour
{
    public Tilemap walls;
    public Tilemap nonDescructableBlocks;
    public GameObject[] blueBlocks;
    public GameObject[] redBlocks;
    public GameObject[] neutralBlocks;
    
    public int numberOfBlueAndRedBlocks;
    public int numberOfNeutralBlocks;

    public List<CellPosition> AvailableCells { get; private set; }
    void Start()
    {
        // Debug.Log($"Min: {nonDescructableBlocks.localBounds.min}");
        // Debug.Log($"Max: {nonDescructableBlocks.localBounds.max}");

        // Debug.Log($"Size: {nonDescructableBlocks.size}");

        AvailableCells = GetAvailableCells().ToList();
        
        SpawnBlocks(blueBlocks, numberOfBlueAndRedBlocks);
        SpawnBlocks(redBlocks, numberOfBlueAndRedBlocks);
        SpawnBlocks(neutralBlocks, numberOfNeutralBlocks);
    }

    private void SpawnBlocks(GameObject[] possibleBlocks, int numberToSpawn)
    {
        for (var i = 0; i <= numberToSpawn; i++)
        {
            var randomBlock = Random.Range(0, blueBlocks.Length);
            var block = possibleBlocks.ElementAt(randomBlock);
            SpawnBlock(block);
        }
    }

    private void SpawnBlock(GameObject block)
    {
        var cellPosition = Random.Range(0, AvailableCells.Count());
        var selectedCell = AvailableCells.ElementAt(cellPosition);
        Instantiate(block,
            selectedCell.WorldPosition, Quaternion.identity, gameObject.transform);
        
        // Remove from the available pool of cells to spawn
        AvailableCells.RemoveAt(cellPosition);
    }

    private IEnumerable<CellPosition> GetAvailableCells()
    {
        var cellBounds = nonDescructableBlocks.cellBounds;
        var xMin = cellBounds.xMin - 1;
        var xMax = cellBounds.xMax + 1;
        var yMin = cellBounds.yMin - 1;
        var yMax = cellBounds.yMax + 1;

        for(int x=xMin; x < xMax; x++)
        for (var y = yMin; y < yMax; y++)
        {
            var tilePosition = new Vector3Int(x, y, 0);
            // Debug.Log($"({x},{y}): {nonDescructableBlocks.HasTile(tilePosition)}");

            if (!nonDescructableBlocks.HasTile(tilePosition))
            {
                var worldPosition = nonDescructableBlocks.GetCellCenterWorld(tilePosition);

                yield return new CellPosition(new Vector2Int(tilePosition.x, tilePosition.y), worldPosition);
                // yield return tilePosition;
            }
            
        }
    }

    void Update()
    {
    }
}

public class CellPosition
{
    public Vector2Int GridPosition { get; }
    public Vector3 WorldPosition { get; }

    public CellPosition(Vector2Int gridPosition, Vector3 worldPosition)
    {
        GridPosition = gridPosition;
        WorldPosition = worldPosition;
    }

    public override string ToString()
    {
        return $"GridPosition: {GridPosition}, WorldPosition: {WorldPosition}";
    }
}


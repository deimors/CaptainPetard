using System.Collections.Generic;
using System.Linq;
using ModestTree;
using Newtonsoft.Json;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;
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
    public List<(CellPosition position, DestructableBlockPresenter block)> SpawnedBlocks { get; } = new();
    
    [Inject]
    public IGameCommands GameCommands { private get; set; }
    
    [Inject]
    public IPlayersEvents PlayersEvents { private get; set; }
    void Start()
    {
        // Debug.Log($"Min: {nonDescructableBlocks.localBounds.min}");
        // Debug.Log($"Max: {nonDescructableBlocks.localBounds.max}");

        // Debug.Log($"Size: {nonDescructableBlocks.size}");

        PlayersEvents.OfType<PlayersEvent, PlayersEvent.PlayerSpawned>()
            .Subscribe(HandlePlayerSpawnedEvent);

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
        var blockPresenter = Instantiate(block,
            selectedCell.WorldPosition, Quaternion.identity, gameObject.transform).GetComponent<DestructableBlockPresenter>();

        blockPresenter.BlockController = this;
        blockPresenter.CellPosition = selectedCell;
        
        // Remove from the available pool of cells to spawn
        AvailableCells.RemoveAt(cellPosition);
        SpawnedBlocks.Add((selectedCell, blockPresenter));
    }

    private IEnumerable<CellPosition> GetAvailableCells()
    {
        var cellBounds = nonDescructableBlocks.cellBounds;
        // This is because the actual size of the playable map is 1 cell bigger on all sides
        // than the tilemap because the tilemap only goes to the edges of the indestructible blocks
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

    public void HandleBlockDestroyed(CellPosition position, int points)
    {
        GameCommands.AddToScore(points);
        
        // Remove from the spawned blocks and add back to the Available cells
        SpawnedBlocks.RemoveAll(tuple => tuple.block.CellPosition.GridPosition == position.GridPosition);
        AvailableCells.Add(position);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Respawning blocks");
            RespawnBlocks();
        }
    }

    public void RespawnBlocks()
    {
        var blockCounts = GetSpawnedBlockCounts();
        SpawnBlocks(blueBlocks, numberOfBlueAndRedBlocks - blockCounts.blueCount);
        SpawnBlocks(redBlocks, numberOfBlueAndRedBlocks - blockCounts.redCount);
        SpawnBlocks(neutralBlocks, numberOfNeutralBlocks - blockCounts.neutralCount);
    }

    private (int blueCount, int redCount, int neutralCount) GetSpawnedBlockCounts()
    {
        var blueCount = SpawnedBlocks.Count(s => s.block.BlockColour == BlockColours.Blue);
        var redCount = SpawnedBlocks.Count(s => s.block.BlockColour == BlockColours.Red);
        var neutralCount = SpawnedBlocks.Count(s => s.block.BlockColour == BlockColours.Neutral);

        return (blueCount, redCount, neutralCount);
    }

    private void HandlePlayerSpawnedEvent(PlayersEvent.PlayerSpawned @event) => 
        RemoveCloseSpawnedBlocks(@event.Config.Position);

    private void RemoveCloseSpawnedBlocks(Vector2 position)
    {
        var gridCoordinate = GetGridCoordinates(position);
        var positionsToUnblock = new Vector2[]
        {
            new Vector2Int(gridCoordinate.x, gridCoordinate.y),
            new Vector2Int(gridCoordinate.x, gridCoordinate.y + 1),
            new Vector2Int(gridCoordinate.x, gridCoordinate.y - 1),
            new Vector2Int(gridCoordinate.x - 1, gridCoordinate.y),
            new Vector2Int(gridCoordinate.x + 1, gridCoordinate.y),
        };

        var blocksToRemove = SpawnedBlocks
            .Where<(CellPosition position, DestructableBlockPresenter block)>
                (tuple => positionsToUnblock.Contains(tuple.position.GridPosition)).ToArray();

        foreach (var blockToRemove in blocksToRemove)
        {
            blockToRemove.block.DestroyBlock();
        }
    }

    private Vector3Int GetGridCoordinates(Vector2 worldCoordinates) =>
        nonDescructableBlocks.layoutGrid.WorldToCell(new Vector3(worldCoordinates.x, worldCoordinates.y, 0)); 
    
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


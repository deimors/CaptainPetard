using UnityEngine;

public class DestructableBlockPresenter : MonoBehaviour
{
	public BlockColours BlockColour;
	public int points;

	public BlockSpawnerPresenter BlockController { get; set; }
	public CellPosition CellPosition { get; set; }

	public void HandleExplosion(PlayerColours bombColour)
	{
		if (BlockColour.CanBeDestroyedBy(bombColour))
		{
			Destroy(gameObject);
			if (BlockController != null)
			{
				BlockController.HandleBlockDestroyed(CellPosition, points);
			}
		}
	}
}

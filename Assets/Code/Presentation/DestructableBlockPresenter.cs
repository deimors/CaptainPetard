using UnityEngine;

public class DestructableBlockPresenter : MonoBehaviour
{
	public BlockColours BlockColour;

	public void HandleExplosion(PlayerColours bombColour)
	{
		if (BlockColour.CanBeDestroyedBy(bombColour))
		{
			Destroy(gameObject);
		}
	}
}

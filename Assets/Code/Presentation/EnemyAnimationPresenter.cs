using UnityEngine;

public class EnemyAnimationPresenter : MonoBehaviour
{
	public Animator Animator;
	public Rigidbody2D Rigidbody;

	void Start()
	{

	}

	void Update()
	{
		var velocity = Rigidbody.velocity;

		Animator.SetBool("Moving", !Mathf.Approximately(velocity.sqrMagnitude, 0));

		if (velocity.x < 0)
		{
			Animator.transform.rotation = Quaternion.Euler(0, 0, 90);
		}
		else if (velocity.x > 0)
		{
			Animator.transform.rotation = Quaternion.Euler(0, 0, -90);
		}
		else if (velocity.y < 0)
		{
			Animator.transform.rotation = Quaternion.Euler(0, 0, 180);
		}
		else
		{
			Animator.transform.rotation = Quaternion.Euler(0, 0, 0);
		}
	}
}
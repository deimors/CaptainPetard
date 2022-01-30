using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationPresenter : MonoBehaviour
{
	public Animator PlayerAnimator;
	public Rigidbody2D PlayerRigidbody;

	void Start()
	{

	}

	void Update()
	{
		var velocity = PlayerRigidbody.velocity;

		PlayerAnimator.SetFloat("Horizontal", velocity.x);
		PlayerAnimator.SetFloat("Vertical", velocity.y);

		PlayerAnimator.SetBool("StoppedHorizontal", Mathf.Approximately(velocity.x, 0));
		PlayerAnimator.SetBool("StoppedVertical", Mathf.Approximately(velocity.y, 0));
	}
}

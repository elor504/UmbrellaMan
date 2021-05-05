using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimation : MonoBehaviour
{
	[SerializeField]
	PlayerManager player;
	//
	[SerializeField]
    SpriteRenderer sprite;

	[SerializeField]
	Color HitColor;
	[SerializeField]
	Color NormalColor;

	[SerializeField]
	float invisibilityTime;

	public void HitGFX()
	{
		StartCoroutine(HitEffects());
	}

	public IEnumerator HitEffects()
	{
		sprite.color = HitColor;
		yield return new WaitForSeconds(invisibilityTime / 7);
		sprite.color = NormalColor;
		yield return new WaitForSeconds(invisibilityTime / 7);
		sprite.color = HitColor;
		yield return new WaitForSeconds(invisibilityTime / 7);
		sprite.color = NormalColor;
		yield return new WaitForSeconds(invisibilityTime / 7);
		sprite.color = HitColor;
		yield return new WaitForSeconds(invisibilityTime / 7);
		sprite.color = NormalColor;
		yield return new WaitForSeconds(invisibilityTime / 7);
		sprite.color = HitColor;
		yield return new WaitForSeconds(invisibilityTime / 7);
		sprite.color = NormalColor;
		//player.canBeHit = true;

		//player can be hitted again, need to add a bool to the player so he would not take damage while this effects is activated
	}


	public void SetPlayerNormalColor()
	{
		//will be texture in 3D
		sprite.color = NormalColor;
	}





}

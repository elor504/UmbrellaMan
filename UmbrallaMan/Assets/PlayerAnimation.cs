using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimation : MonoBehaviour
{

	//
	[SerializeField]
    SpriteRenderer sprite;

	[SerializeField]
	Color HitColor;
	[SerializeField]
	Color NormalColor;

	public void HitGFX()
	{
		StartCoroutine(HitEffects());
	}

	public IEnumerator HitEffects()
	{
		sprite.color = HitColor;
		yield return new WaitForSeconds(0.1f);
		sprite.color = NormalColor;
		yield return new WaitForSeconds(0.1f);
		sprite.color = HitColor;
		yield return new WaitForSeconds(0.1f);
		sprite.color = NormalColor;
		yield return new WaitForSeconds(0.1f);
		sprite.color = HitColor;
		yield return new WaitForSeconds(0.1f);
		sprite.color = NormalColor;
		yield return new WaitForSeconds(0.1f);
		sprite.color = HitColor;
		yield return new WaitForSeconds(0.1f);
		sprite.color = NormalColor;

		//player can be hitted again, need to add a bool to the player so he would not take damage while this effects is activated
	}


	





}

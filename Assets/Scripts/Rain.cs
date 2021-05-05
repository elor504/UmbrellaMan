using UnityEngine;

public class Rain : MonoBehaviour
{
	public PoisonCloud cloud;

	public bool IsPlayerIsProtecting(Umbrella umbrella)
	{
		if (umbrella.getIsAimingUp && umbrella.getIsShielding)
		{
			return true;
		}
		return false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{

		if (collision.gameObject.tag == "Projectile")
		{
			Debug.Log(collision.gameObject.name);
			if (cloud.getIsPurified)
				collision.GetComponent<Projectile>().ChangeProjectileType(projectileTypes.purified);
			else
				collision.GetComponent<Projectile>().ChangeProjectileType(projectileTypes.corruptive);
		}
	}



	private void OnTriggerStay2D(Collider2D collision)
	{
		//player
		if (collision.gameObject.tag == "Player")
		{
			PlayerManager player = collision.GetComponent<PlayerManager>();
			Umbrella umbrella = collision.GetComponent<Umbrella>();
			if (!IsPlayerIsProtecting(umbrella) && !cloud.getIsPurified)
			{
				player.GetDamage(1);
			}
		}

		//projectile
		if (collision.gameObject.tag == "Projectile")
		{
			Projectile projectile = collision.gameObject.GetComponent<Projectile>();
			if (projectile.getProjType == projectileTypes.purified && !cloud.getIsPurified)
			{
				projectile.ChangeProjectileType(projectileTypes.corruptive);
			}
			else if (projectile.getProjType == projectileTypes.corruptive && cloud.getIsPurified)
			{
				projectile.ChangeProjectileType(projectileTypes.purified);
			}
		}
	}
}

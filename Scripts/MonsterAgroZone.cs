using UnityEngine;

public class MonsterAgroZone : MonoBehaviour
{
	[SerializeField] private Monster[] _monsters;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") || other.CompareTag("Projectile"))
		{
			foreach (Monster mob in _monsters)
				mob.SetTarget(Player.Transform);

			Destroy(this.gameObject);
		}
	}
}
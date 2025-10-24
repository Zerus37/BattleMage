using UnityEngine;

public class AntiMageZone : MonoBehaviour
{
	[SerializeField] private float _manaBurnPerSecond = 30f;

	private void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent("Player"))
		{
			if(other.TryGetComponent<Mana>(out Mana mana))
			{
				mana.SetManaBurn(_manaBurnPerSecond);
			}
		}
		else if(other.TryGetComponent<Projectile>(out Projectile proj))
		{
			proj.TurnOff();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.GetComponent("Player"))
		{
			if (other.TryGetComponent<Mana>(out Mana mana))
			{
				mana.SetManaBurn(0);
			}
		}
	}
}
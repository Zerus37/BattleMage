using System;
using UnityEngine;

[Serializable]
public struct Spell
{
	public SpellType type;
	public Projectile projectile;
	public SelfCast selfCastComponent;
}

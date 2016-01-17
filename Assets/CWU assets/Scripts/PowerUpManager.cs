using UnityEngine;
using System.Collections;
public class PowerUpManager : MonoBehaviour
{
	public PlayerShoot[] weapons; 
	private int powerLevel = -1;

	void PowerUp()
	{
		powerLevel++;
		if(powerLevel < weapons.Length)
		{
			weapons[powerLevel].enabled = true;
		}
	}
}

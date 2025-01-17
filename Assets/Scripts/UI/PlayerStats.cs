﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour 
{
	//Debugging
	public bool debugOn = false; //if true, prints values to console

	//Player Health and Battery
	readonly public float limitHealth = 500.0f; //Limits the max amount of health you can upgrade to
	readonly public float limitBattery = 10f; //Limits the max amount of battery you can upgrade to
	public float maxBattery; //Sets the max battery possible, upgradeable
	public float maxHealth; //current limit of the full health of the player, upgradeable
	public float health; //Current health of the player
	public float battery; //Current battery of the player
	public float batteryRegenSpeed; //battery regen in float, percent should be in decimal form
	public float timeOfCooldown; //Sets an amount of time to pass before battery regen starts again
	public float currCooldown; //Timer variable for timeOfCooldown
	
	//TODO: Add ammunition inventory and more

	public PlayerStats() 
	{
		//Health and Battery
		maxHealth = 100.0f;
		maxBattery = 5.0f;
		health = 100.0f;
		battery = 5.0f;
		batteryRegenSpeed = 0.8f; //regen X of 1 charge in a second
		timeOfCooldown = 2f; //2 seconds until battery starts regen again
		currCooldown = 0f;

		//FIXME: Note down recharge calculation and set a reference percentage
		//Possibly how long does it take to regen 1 charge
		
		//TODO: Weapons & Ammunition
	}

	public void regenBattery()
	{
		if (currCooldown > 0f)
		{
			currCooldown -= 1f * Time.deltaTime;
			return;
		}
		if (battery < maxBattery)
		{
			//DeltaTime * increasePerSecond
			battery += batteryRegenSpeed * Time.deltaTime;
			if (battery > maxBattery)
			{
				battery = maxBattery;
			}
		}
	}
	
	public void raiseBatteryRegen(float floatPercent)
	{
		batteryRegenSpeed += floatPercent;
	}

	public void lowerBatteryRegen(float floatPercent)
	{
		batteryRegenSpeed -= floatPercent;
	}

	public bool recieveHealth(float heal) //Boolean could be used to show something in the UI
	{
		if (health < maxHealth) 
		{
			health += heal;
			if (health > maxHealth) 
			{
				health = maxHealth; //If health went over, set it back to max
			}
			if (debugOn) 
			{
				Debug.Log("Health Gained! " + "Health = " + health.ToString());
			}
			return true; //Event: Player healed
		}
		if (debugOn) 
		{
			Debug.Log("Cant gain anymore health!");
		}
		return false; //Event: Player has max health
	}

	public bool recieveBattery(float charges)
	{
		/* 
		According to the game design doc, battery only regenerates. This is here
		Just in case we add consumables that restore battery
		*/
		if (battery < maxBattery) 
		{
			battery += charges;
			if (battery > maxBattery) 
			{
				battery = maxBattery; 
			}
			if (debugOn) 
			{
				Debug.Log("Battery Gained! " + "Battery = " + battery.ToString());
			}
			return true; //Battery gained successfully
		}
		if (debugOn) 
		{
			Debug.Log("Cant gain anymore battery!");
		}
		return false; //Player has max battery
	}

	public bool recieveDamage(float damage)
	{
		float newHealth = health - damage;
		if ( newHealth > 0 ) //Enough Health to tank incoming damage
		{
			health = newHealth;
			if (debugOn) 
			{
				Debug.Log("Damage Taken! " + "Health = " + health.ToString()); //Possibly fix for long debug numbers
			}
			return true; //Could tank damage
		}
		if (debugOn) 
		{
			Debug.Log("You died! Health is or went below 0");
		}
		health = 0f;
		return false; //Died
	}

	public bool spendBattery(float batteryCost)
	{
		float newBattery = battery - batteryCost;
		if ( newBattery >= 0 ) 
		{
			battery = newBattery;
			if (debugOn) 
			{
				Debug.Log("Battery spent! " + "Battery = " + battery.ToString());
			}
			currCooldown = timeOfCooldown; 
			return true; //Spent battery
		}
		if (debugOn) 
		{
			Debug.Log("Not enough battery!");
		}
		return false; //Don't have enough battery
	}

	public bool raiseMaxBattery(float addedCharges) //Method upgrades the player's max amount of charges
	{
		if (maxBattery < limitBattery) 
		{
			maxBattery += addedCharges;
			if (maxBattery > limitBattery)
			{
				maxBattery = limitBattery;
			}
			if (debugOn)
			{
				Debug.Log("New Battery Limit = " + maxBattery.ToString());
			}
			return true; //Battery upgraded successfully
		}
		if (debugOn) 
		{
			Debug.Log("Upgrade Limit for battery reached!");
		}
		return false; //Cant upgrade battery anymore
	}

	public bool raiseMaxHealth(float upgradeNum) 
	{
		if (maxHealth < limitHealth) 
		{
			maxHealth += upgradeNum;
			if (maxHealth > limitHealth)
			{
				maxHealth = limitHealth;
			}
			if (debugOn) 
			{
				Debug.Log("New Health Limit = " + maxHealth.ToString());
			}
			return true; //Successfully upgraded Health
		}
		if (debugOn) 
		{
			Debug.Log("Upgrade Limit for health reached!");
		}
		return false; //Cant upgrade health any further
	}

}

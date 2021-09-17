using UnityEngine;

public class Pickups : MonoBehaviour
{
    [SerializeField] int weaponID;
    [SerializeField] bool healthPickup = false;

    private void ChangeWeapon()
    {
    	if (healthPickup == true)
    	{
    		SaveScript.health += 40;
            if (SaveScript.health > 100)
            {
                SaveScript.health = 100;
            }
    	}
    	else
    	{
    		SaveScript.weaponID = this.weaponID;
    	}
    	Destroy(this.gameObject);
    }
}

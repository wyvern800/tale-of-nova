using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
  public int maxDamageToGive;
  public GameObject particle;
  public Transform hitPoint;
  private PlayerController player;
  private EnemyHealthManager healthManager;


  // Start is called before the first frame update
  void Start()
  {
    player = GameObject.FindObjectOfType<PlayerController>();
    healthManager = GameObject.FindObjectOfType<EnemyHealthManager>();
  }

  // Update is called once per frame
  void Update()
  {
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Enemy")
    {
      if (!player.isAttacking())
        return;
      float modifierByLevel = (0.8f * player.getPlayerSkills().getLevel(0));
      int randomDamage = Random.Range(1, maxDamageToGive);
      int chanceToHit = Random.Range(1, 5);

      //Debug.Log("Chance to hit: " + chanceToHit);
      //Debug.Log("Modifier by level: " + modifierByLevel);
      //Debug.Log("Randomdamage: " + randomDamage);

      if (chanceToHit % 2 == 0)
      { // if chance division by 2 = 0, then hit
        sendHit(other, randomDamage, 0, "Normal Hit");
      }
      else if (randomDamage == maxDamageToGive)
      {
        sendHit(other, randomDamage, 1, "Critical");
      }
      else
      {
        sendHit(other, 0, 2, "Parry");
      }
    }
  }

  private void sendHit(Collider2D other, int dmg, int hitType, string debugMsg)
  {
    other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(dmg);
    other.gameObject.GetComponent<HitsplatsManager>().SendHitsplattEnemy(other, dmg, hitType);
    Instantiate(particle, hitPoint.position, hitPoint.rotation);
    Debug.Log(debugMsg);
  }
}

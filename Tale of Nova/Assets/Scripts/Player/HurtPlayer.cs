using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
  public int damageToGive;
  public GameObject particle;
  private EnemyHealthManager enemyHealthManager;

  // Start is called before the first frame update
  void Start()
  {
    enemyHealthManager = FindObjectOfType<EnemyHealthManager>();
  }

  // Update is called once per frame
  void Update()
  {
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.name == "Player")
    {
      int randomDamage = Random.Range(1, damageToGive);
      int chanceToHit = Random.Range(1, 20);

      if (chanceToHit % 2 == 0)
      { // if chance division by 2 = 0, then hit
        sendHit(other, randomDamage, 0, "Normal Hit");
      }
      else if (randomDamage == damageToGive)
      {
        sendHit(other, randomDamage, 1, "Critical");
      }
      else
      {
        sendHit(other, 0, 2, "Parry");
      }
    }
  }

  private void sendHit(Collision2D other, int dmg, int hitType, string debugMsg)
  {
    other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(dmg);
    other.gameObject.GetComponent<HitsplatsManager>().SendHitsplattPlayer(other, dmg, hitType);
    Instantiate(particle, other.transform.position, other.transform.rotation);
    //Debug.Log(debugMsg);
  }

  /*void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.name == "Player")
    {
      other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageToGive);
    }
  }*/
}

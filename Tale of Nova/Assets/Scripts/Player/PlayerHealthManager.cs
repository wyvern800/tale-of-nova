using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
  private int playerCurrentHealth;
  private PlayerController player;
  private int playerMaxHealth = 100;

  public int getMaxHealth()
  {
    return this.playerMaxHealth;
  }

  private GameObject thePlayer;
  // Start is called before the first frame update
  void Start()
  {
    player = GameObject.FindObjectOfType<PlayerController>();
    playerCurrentHealth = playerMaxHealth;
  }

  // Update is called once per frame
  void Update()
  {
    if (playerCurrentHealth <= 0)
    {
      gameObject.SetActive(false);
      playerCurrentHealth = 0;
      Destroy(gameObject);
      Debug.Log("Você Morreu");
    }
  }

  public void HurtPlayer(int damage)
  {
    if (playerCurrentHealth < 0)
      return;
    playerCurrentHealth -= damage;
    Debug.Log("Player hurt with " + damage + " dps");
  }

  public void SetMaxHealth()
  {
    playerCurrentHealth = playerMaxHealth;
  }


  public int getCurrentHealth()
  {
    return playerCurrentHealth;
  }
}


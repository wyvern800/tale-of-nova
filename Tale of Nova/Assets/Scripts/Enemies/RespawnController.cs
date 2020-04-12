using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{

  public int timeToRespawn;
  private Vector2 respawnTile;
  private EnemyHealthManager healthManager;

  // Start is called before the first frame update
  void Start()
  {
    respawnTile = gameObject.transform.position;
    healthManager = FindObjectOfType<EnemyHealthManager>();
  }

  // Update is called once per frame
  void Update()
  {
  }

  public IEnumerator RespawnNpc(GameObject obj)
  {
    Debug.Log("dead");
    obj.GetComponent<Renderer>().enabled = false;
    obj.transform.position = new Vector3(-25, -25, -25);
    //obj.SetActive(false);
    yield return new WaitForSeconds(timeToRespawn);
    Debug.Log("respawn");
    healthManager.setIsDead(false);
    obj.transform.position = respawnTile;
    obj.GetComponent<Renderer>().enabled = true;
    //obj.SetActive(true);
  }
}

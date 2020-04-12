using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; // The namespace for the UI stuff.
using UnityEngine;

public class HitsplatsManager : MonoBehaviour
{
  public GameObject hitSplattObject;
  public Sprite hitHitsplatSprite;
  public Sprite missHitsplatSprite;
  public Sprite parryHitsplatSprite;
  public Sprite critHitsplatSprite;
  private float attackTime;

  public enum hitTypes
  {
    NORMAL, CRITICAL, PARRY
  }


  // Start is called before the first frame update
  void Start()
  {
    //attackTime = gameObject.GetComponent<PlayerController>().attackTime;
  }

  // Update is called once per frame
  void Update()
  {
  }

  public void SendHitsplattEnemy(Collider2D other, int damage, int hitType)
  {
    drawHitsplatt(hitType, damage);
    Instantiate(hitSplattObject, new Vector2(other.transform.position.x, other.transform.position.y /*+ 0.7f*/), other.transform.rotation);
  }
  public void SendHitsplattPlayer(Collision2D other, int damage, int hitType)
  {
    drawHitsplatt(hitType, damage);
    Instantiate(hitSplattObject, new Vector2(other.transform.position.x, other.transform.position.y /*+ 0.9f*/), other.transform.rotation);
  }

  private void drawHitsplatt(int hitType, int damage)
  {
    SpriteRenderer spr = hitSplattObject.GetComponentInChildren<SpriteRenderer>();
    Text txt = hitSplattObject.GetComponentInChildren<Text>();
    spr.sprite = (
        hitType == (int)hitTypes.NORMAL ? hitHitsplatSprite
       : hitType == (int)hitTypes.CRITICAL ? critHitsplatSprite
       : parryHitsplatSprite);
    txt.color = (hitType == (int)hitTypes.CRITICAL ? Color.yellow : Color.white);
    txt.text = "" + damage;
  }
}

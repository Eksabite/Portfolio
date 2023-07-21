using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCode : MonoBehaviour
{
    public float fieldOfImpact;
    public float force;
    public float timeLeft;
    public LayerMask layerToHit;
    private Rigidbody2D rb;
    public float startForce;
    public float damage;
    public int playerId;
    public GameObject player;
    private float mana;
    private GameObject playerWasHit;
    public int particle;

    void Start()
    {
        playerWasHit = null;
        mana = GetComponent<PlayerToBulletID>().mana;
        playerId = GetComponent<PlayerToBulletID>().PlayerID+1;
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerToBulletID>().player;
        player.GetComponent<PlayerScript>().mp -= mana;
        rb.AddForce(transform.up * startForce, ForceMode2D.Impulse);
    }

    void Update()
    {
        
        timeLeft -= Time.deltaTime;
        if(timeLeft < 0)
        {
            explode();
            
        }
    }
    void explode()
    {
        
        Collider2D[] Objects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, layerToHit);
        foreach(Collider2D Obj in Objects)
        {
            if (Obj.CompareTag("Player"))
            {
                if (Obj.gameObject.GetComponent<PlayerScript>().id == playerId)
                {
                    Vector2 direction = Obj.transform.position - transform.position;
                    Obj.GetComponent<Rigidbody2D>().AddForce(direction / 2.5f * force, ForceMode2D.Impulse);
                }
            }
            if (Obj.CompareTag("Shield"))
            {
                if (Obj.transform.parent.transform.parent.transform.parent.transform.parent.gameObject.GetComponent<PlayerScript>().shieldActive)
                {
                    playerWasHit = Obj.transform.parent.transform.parent.transform.parent.transform.parent.gameObject;
                    PlayerInputManager.instance.PlayEffect(particle, transform);
                    Destroy(gameObject);
                    return;
                }
                else
                {
                    HitMe(Obj.gameObject);
                }
            }
            else if (Obj.CompareTag("Player") && Obj.gameObject != playerWasHit && Obj.gameObject.GetComponent<PlayerScript>().id != playerId)
            {
                
                HitMe(Obj.gameObject);
            }
            
                            
        }
        PlayerInputManager.instance.PlayEffect(particle, transform);
        Destroy(gameObject);
    }


    public void HitMe(GameObject me)
    {
        if (me.gameObject.GetComponent<PlayerScript>().id != playerId)
        {
            me.gameObject.GetComponent<PlayerScript>().takeDamage(damage);
            Vector2 direction = me.transform.position - transform.position;
            me.GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Impulse);
            if (me.GetComponent<PlayerScript>().hp <= 0)
            {
                PlayerInputManager.instance.players[playerId - 1].GetComponent<PlayerScript>().killCount++;
            }
        }
                     
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && playerId != collision.gameObject.GetComponent<PlayerScript>().id)
        {
            explode();
        }
        else if (collision.CompareTag("Shield") && playerId!= collision.transform.parent.transform.parent.transform.parent.transform.parent.gameObject.GetComponent<PlayerScript>().id)
        {
            playerWasHit = collision.transform.parent.transform.parent.transform.parent.transform.parent.gameObject;
            explode();
        }
        int p = collision.gameObject.layer;
        int r = LayerMask.NameToLayer("Obsticle");
        if (p == r)
        {
            explode();
        }

    }
}

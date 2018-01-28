using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntityController : MonoBehaviour {

    public bool IsPlayer;    
    public int life = 100;
    public GameObject DeathAnimation;
    public GameObject AimParent;
    public Sprite PlayerSprite;
    public Sprite[] PlayerLifeSprite;
    public Sprite[] EnemyLifeSprite;
    public GameObject LifeObject;
    public SpriteRenderer CooldownIndicator;
    public GameObject DamageEffect;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer lifeSpriteRenderer;
    private int MaxLife;

    private void Start()
    {
        spriteRenderer = AimParent.GetComponent<SpriteRenderer>();
        lifeSpriteRenderer = LifeObject.GetComponent<SpriteRenderer>();
        MaxLife = life;
        UpdateIndicator();
    }

    private void Update()
    {
        UpdateIndicator();
        if (IsPlayer)
        {
            if(spriteRenderer.sprite != PlayerSprite)
            {
                spriteRenderer.sprite = PlayerSprite;
                AimParent.transform.localScale = new Vector3(1, AimParent.transform.localScale.y, AimParent.transform.localScale.z);
            }
            CooldownIndicator.enabled = true;
        } else {
            CooldownIndicator.enabled = false;
        }        
    }

    public void TakeDamage(int x)
    {       
        life -= x;
        if(life > 0)
        {
            Instantiate(DamageEffect, transform.position, transform.rotation);
        }
    }

    public void Kill()
    {
        Instantiate(DeathAnimation, transform.position, transform.rotation);
        if (IsPlayer)
        {
            SceneManager.LoadScene("Lose_scn");
        }
        Destroy(this.gameObject);
    }

    public void UpdateIndicator()
    {
        if (IsPlayer)
        {            
            if (life <= 0)
            {
                lifeSpriteRenderer.sprite = PlayerLifeSprite[0];
                life = 0;
                Kill();
            }
            int SpriteToAssign = (int)((life * 20 / MaxLife)) - 1;
            if(SpriteToAssign >= 0 && SpriteToAssign < PlayerLifeSprite.Length)
            {
                lifeSpriteRenderer.sprite = PlayerLifeSprite[SpriteToAssign];
            }
            
        }
        else
        {            
            if (life <= 0)
            {
                lifeSpriteRenderer.sprite = EnemyLifeSprite[0];
                life = 0;
                Kill();
            }
            int SpriteToAssign = (int)((life * 20 / MaxLife)) - 1;
            if (SpriteToAssign >= 0 && SpriteToAssign < EnemyLifeSprite.Length)
            {
                lifeSpriteRenderer.sprite = EnemyLifeSprite[SpriteToAssign];
            }
        }
    }


}

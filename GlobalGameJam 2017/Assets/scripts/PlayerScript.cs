using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat
{
    public float stat;
    public float bonusMult;
    public float bonusAdd;

    public Stat(float nb)
    {
        stat = nb;
        bonusMult = 1;
        bonusAdd = 0;
    }

    public float GetStat()
    {
        return ((stat + bonusAdd) * bonusMult);
    }
}


enum StatType
{
    SPEED,
    POWER,
    HEALTH,
    FIRERATE
}

/// <summary>
/// Player controller and behavior
/// </summary>
public class PlayerScript : MonoBehaviour
{
    // Stats of player
    public Stat speed = new Stat(3);
    public Stat power = new Stat(0);
    private Stat maxHp = new Stat(5);
    public Stat fireRate = new Stat(1);
    public float hp = 5;
    public float point = 0;
    private int level = 1;
    private int kill = 0;
    public Text cost;
    public Text hpDisplay;
    public Text powerDisplay;
    public Text speedDisplay;
    public Text fireRateDisplay;
    public Text damageDisplay;
    public Text killDisplay;
    public Text scoreDisplay;
    public Text scoreTotalDisplay;
	public GameObject[] bloodType;
    
    // Player's weapon
    //public Weapon weapon = new Weapon();

    // For movement of player
    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;
    public Slider lifeBar;

    //shoot test
    float weaponFireRate = 2f;
    int weaponDamage = 1;
    float weaponTimer = 0;

    public float invulnerability = 0.5f;

    bool end = false;

    public float getMaxHp()
    {
        return (maxHp.stat);
    }

    public void UpdateScore()
    {
        scoreDisplay.text = "Score: " + point.ToString();
    }

    public void AddScore(int score)
    {
        if (score > 0)
        {
            point += score;
            UpdateScore();
        }
    }

    void Start ()
    {
        UpdateCostText();
        UpdateHpText();
        UpdatePowerText();
        UpdateSpeedText();
        UpdateFireRateText();
        UpdateDamageText();
    }

    void Update()
    {
        if (hp > 0 && !end)
        {
            Vector3 pos = transform.position;
            pos.z = -10;
            GameObject.Find("Main Camera").transform.position = pos;
            invulnerability -= Time.deltaTime;
            // Axis information
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");

			if (Input.GetKeyDown (KeyCode.Escape))
				Application.Quit ();

            // Set movement
            movement.x = speed.GetStat() * inputX;
            movement.y = speed.GetStat() * inputY;

            // Check hp

            //Fire rate
            weaponTimer += Time.deltaTime;
            if (Input.GetMouseButton(0) && weaponTimer >= 1 / (weaponFireRate * fireRate.GetStat()))
            {
                //int projectileDamage = (int)((1 + power.GetStat() / 100) * weapon.GetDamage());
                Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
                Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
                lookPos = lookPos - this.transform.position;
                Vector2 lookPos2 = new Vector2(lookPos.x, lookPos.y);
                lookPos2.Normalize();
                var obj = (GameObject)Instantiate(Resources.Load("prefabs/conceptProjectile2"), transform.position, Quaternion.Euler(lookPos.x, lookPos.y, 0));
                var proj = obj.GetComponent<PlayerProjectile>();
                proj.speed += speed.GetStat();
                proj.direction = new Vector2(lookPos2.x, lookPos2.y);
                proj.ps = this;
                proj.explodeDamage = ((1f + power.GetStat() / 100f) * weaponDamage);
                weaponTimer = 0;
            }
            UpdateLifeBar();
            UpdateScore();
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        if (end)
            return;
        end = true;
        UpdateLifeBar();
        UpdateHpText();
		int i = (int)Random.Range(0, 2);
        Instantiate(Resources.Load("particles/bleeding"), transform.position, Quaternion.identity);
        Instantiate(bloodType[i], transform.position, Quaternion.identity);
        //GameObject obj = (GameObject)Instantiate(Resources.Load("prefabs/GameOverMenu"));
        GameObject.Find("MenuManager").GetComponent<DisplayLevelMenu>().End();
        GameObject.Find("GameOverScore").GetComponent<Text>().text = point.ToString();
        GameObject.Find("GameOverTotalScore").GetComponent<Text>().text = (((level + 3) * (level + 4) / 2 - 10) * 10).ToString();
        GameObject.Find("GameOverKill").GetComponent<Text>().text = kill.ToString();
        Destroy(gameObject);
    }

    public void AddKill()
    {
        kill++;
    }

    void TakeDamage(Collision2D collision)
    {
        if (collision.gameObject.tag != "Enemy" || invulnerability > 0)
            return;
        TakeDamage(1);
        invulnerability = 0.5f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        TakeDamage(collision);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        TakeDamage(collision);
    }

    public void TakeDamage(float dmg)
    {
        if (invulnerability > 0)
            return;
        invulnerability = 0.5f;
        hp -= dmg;
        if (hp <= 0)
        {
            GameOver();
        }
    }

    private void UpdateLifeBar()
    {
        lifeBar.value = hp / maxHp.GetStat();
    }
    void FixedUpdate()
    {
        // 5 - Get the component and store the reference
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

        // 6 - Move the game object
        rigidbodyComponent.velocity = movement;
    }

    public void setHp(float hp)
    {
        if (hp > (int)maxHp.GetStat())
            this.hp = (int)maxHp.GetStat();
        else
            this.hp = hp;
    }

    public void addHp(float hp)
    {
        setHp(this.hp + hp);
        AddEffect("Heal");
    }

    public void AddHpPercentage(float p)
    {
        setHp((int)(this.hp + p * maxHp.GetStat()));
        AddEffect("Heal");
    }

    private void UpdateCostText()
    {
        cost.text = "Level cost: " + (level * 10 + 40).ToString() + " points";
    }

    private void UpdateHpText()
    {
        hpDisplay.text = hp.ToString() + "/" + maxHp.GetStat().ToString();
    }

    private void UpdatePowerText()
    {
        powerDisplay.text = "+ " + power.GetStat().ToString() + "% damage";
    }

    private void UpdateSpeedText()
    {
        speedDisplay.text = speed.GetStat().ToString();
    }

    private void UpdateFireRateText()
    {
        fireRateDisplay.text = fireRate.GetStat().ToString() + "/s";
    }

    private void UpdateDamageText()
    {
        int damage = weaponDamage;
        int total = (int)((1f + power.GetStat() / 100f) * damage);
        damageDisplay.text = total.ToString() + " (" + damage.ToString() + " + " + (total - damage).ToString() + ")";
    }

    public void AddEffect(string effect)
    {
        Vector3 pos = new Vector3(transform.position.x + 0.03f, transform.position.y - 0.29f, -0.6f);
        GameObject obj = Instantiate(Resources.Load("particles/" + effect), pos, Quaternion.identity) as GameObject;
        obj.transform.parent = transform;
    }

    public void BuyHp()
    {
        if (point >= level * 10 + 40)
        {
            maxHp.bonusAdd += 1;
            setHp(hp + 3);
            point -= level * 10 + 40;
            level++;
            UpdateCostText();
            UpdateHpText();
            AddEffect("levelUp");
        }
    }

    public void BuyPower()
    {
        if (point >= level * 10 + 40)
        {
            power.bonusAdd += 7;
            point -= level * 10 + 40;
            level++;
            UpdateCostText();
            UpdatePowerText();
            UpdateDamageText();
            AddEffect("levelUp");
        }
    }

    public void BuyMoveSpeed()
    {
        if (point >= level * 10 + 40)
        {
            speed.bonusMult += 0.05f;
            point -= level * 10 + 40;
            level++;
            UpdateCostText();
            UpdateSpeedText();
            AddEffect("levelUp");
        }
    }

    public void BuyFireRate()
    {
        if (point >= level * 10 + 40)
        {
            fireRate.bonusMult += 0.05f;
            point -= level * 10 + 40;
            level++;
            UpdateCostText();
            UpdateFireRateText();
            AddEffect("levelUp");
        }
    }
}

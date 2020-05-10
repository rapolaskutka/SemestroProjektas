using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boss_Fight : MonoBehaviour
{
    [SerializeField] private GameObject ground_enemy;
    [SerializeField] private GameObject flying_enemy;
    [SerializeField] private int IdleTime;
    [SerializeField] private int riseLavaStepY;
    [SerializeField] private int maxRiseLavalSteps;
    private List<GameObject> enemys;
    private GameObject lava;
    private float Enemy_Time = 0;
    private bool CanDoOtherAbility = false;
    private ParticleSystem particle;
    private TextMeshPro[] abilitys;

    enum Boss_Phases
    {
        Spawn,
        Lava,
        Projectiles
    }
    Boss_Phases phases;
    Spawns spawn;
    class Spawns
    {
        List<SpriteRenderer> spawns = new List<SpriteRenderer>();
        Enemy enemy = new Enemy();
        private bool[] availableS = new bool[5];

        public Spawns()
        {
            for (int i = 0; i < availableS.Length; i++)
            {
                availableS[i] = true;
            }
        }
        public void AddSpawn(SpriteRenderer render)
        {
            spawns.Add(render);
        }
        public void SpawnEnemys(int count = 0)
        {
            int enemycount;
            if (count == 0)
            {
                enemycount = Random.Range(1, 5);
            }
            else
            {
                enemycount = count;
            }
            Debug.Log("Count" + enemycount);
            for (int i = 0; i < enemycount; i++)
            {
                int flyenemy = Random.Range(0, 100);
                if (flyenemy >= 50 && availableS[3] && availableS[4])
                {
                    GameObject fly = GameObject.Find("Flying_Enemy");
                    GameObject flyer = Instantiate(fly);
                    Debug.Log(spawns.Count);
                    Debug.Log(spawns[3].transform.localPosition);
                    if (availableS[3])
                    {
                        enemy.AddEnemy(flyer, spawns[3]);
                        availableS[3] = false;
                    }
                    else if(availableS[4])
                    {
                        enemy.AddEnemy(flyer, spawns[4]);
                        availableS[4] = false;
                    }
                }
                else
                {
                    if (availableS[i])
                    {
                        GameObject enemys = GameObject.Find("Enemy");
                        Debug.Log(enemys.GetComponent<SpriteRenderer>().name);
                        GameObject enemies = Instantiate(enemys);
                        Debug.Log(spawns[i].transform.localPosition);
                        enemy.AddEnemy(enemies, spawns[i]);
                        availableS[i] = false;
                    }
                }
            }
        }
        public void ClearSpawns()
        {
            for (int i = 0; i < availableS.Length; i++)
            {
                availableS[i] = true;
            }
            enemy.RemoveAllEnemy();
        }
    }
    public class Enemy
    {
        private List<GameObject> enemys = new List<GameObject>();

        public void RemoveAllEnemy()
        {
            for (int i = 0; i < enemys.Count; i++)
            {
                if (enemys[i] != null)
                {
                    Enemy_Health health = enemys[i].GetComponent<Enemy_Health>();
                    health.GetDamage(1000, true, 0);
                }
            }
            enemys.Clear();
        }
        public void AddEnemy(GameObject enemy, SpriteRenderer position)
        {
            enemy.transform.position = new Vector3(position.transform.position.x, position.transform.position.y, position.transform.position.z);
            enemys.Add(enemy);
        }

    }
    void Start()
    {
        lava = GameObject.Find("Rising Lava");
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy_Time >= Time.time || !CanDoOtherAbility)
        {
            return;
        }
        int phase = Random.Range(0, System.Enum.GetValues(typeof(Boss_Phases)).Length);
        CanDoOtherAbility = false;
        switch (phase)
        {
            case 0:
                phases = Boss_Phases.Spawn;
                break;
            case 1:
                phases = Boss_Phases.Lava;
                break;
            case 2:
                phases = Boss_Phases.Projectiles;
                break;
        }
        DoAbility(phases);
    }
    private void OnEnable()
    {
        Enemy_Time = Time.time;
        Debug.Log("Starting");
        particle = GameObject.Find("Boss_Particle").GetComponent<ParticleSystem>();
        Debug.Log(particle);
        int phase = Random.Range(0, System.Enum.GetValues(typeof(Boss_Phases)).Length);
        spawn = new Spawns();
        spawn.AddSpawn(GameObject.Find("Spawn1").GetComponent<SpriteRenderer>());
        spawn.AddSpawn(GameObject.Find("Spawn2").GetComponent<SpriteRenderer>());
        spawn.AddSpawn(GameObject.Find("Spawn3").GetComponent<SpriteRenderer>());
        spawn.AddSpawn(GameObject.Find("Spawn4").GetComponent<SpriteRenderer>());
        spawn.AddSpawn(GameObject.Find("Spawn5").GetComponent<SpriteRenderer>());
        abilitys = new TextMeshPro[3];
        abilitys[0] = GameObject.Find("Lavas").GetComponent<TextMeshPro>();
        abilitys[1] = GameObject.Find("Spawn").GetComponent<TextMeshPro>();
        abilitys[2] = GameObject.Find("Projectiles").GetComponent<TextMeshPro>();

        switch (phase)
        {
            case 0:
                phases = Boss_Phases.Spawn;
                break;
            case 1:
                phases = Boss_Phases.Lava;
                break;
            case 2:
                phases = Boss_Phases.Projectiles;
                break;
        }
        DoAbility(phases);

    }
    private void DoAbility(Boss_Phases phases)
    {
        switch(phases)
        {
            case Boss_Phases.Spawn:
                abilitys[1].enabled = true;
                break;
            case Boss_Phases.Lava:
                abilitys[0].enabled = true;
                break;
            case Boss_Phases.Projectiles:
                abilitys[2].enabled = true;
                break;
        }
        StartCoroutine(Ability(phases));
    }
    IEnumerator Ability(Boss_Phases phases)
    {
        yield return new WaitForSeconds(5);
        switch (phases)
        {
            case Boss_Phases.Lava:
                abilitys[0].enabled = false;
                RiseLava();
                break;
            case Boss_Phases.Projectiles:
                abilitys[2].enabled = false;
                EmitParticle();
                break;
            case Boss_Phases.Spawn:
                abilitys[1].enabled = false;
                Spawn_Enemy();
                break;
        }
    }
    private void Spawn_Enemy()
    {
        Debug.Log("Spawn Enemy");
        int boss_health = gameObject.GetComponent<Boss_Health>().health;
        if(boss_health < 200)
        {
            spawn.SpawnEnemys(5);
        }
        else if(boss_health < 400)
        {
            spawn.SpawnEnemys(4);
        }
        else if (boss_health <= 700)
        {
            spawn.SpawnEnemys(3);
        }
        StartCoroutine(Remove_Enemy());

    }
    IEnumerator Remove_Enemy()
    {
        yield return new WaitForSeconds(15);
        Debug.Log("Remove Enemy");
        spawn.ClearSpawns();
        CanDoOtherAbility = true;
        Enemy_Time = Time.time + IdleTime;

    }
    private void EmitParticle()
    {
        Debug.Log("Emiting particle");
        int boss_health = gameObject.GetComponent<Boss_Health>().health;
        int p_count = 0;
        if (boss_health < 200)
        {
            p_count = 30;
        }
        else if (boss_health < 400)
        {
            p_count = 20;
        }
        else if (boss_health <= 700)
        {
            p_count = 15;
        }
        var disable = particle.emission;
        var count = particle.main;
        count.maxParticles = p_count;
        disable.enabled = true;
        CanDoOtherAbility = false;
        StartCoroutine(DisableParticle());
    }
    IEnumerator DisableParticle()
    {
        yield return new WaitForSeconds(10);
        var disable = particle.emission;
        disable.enabled = false;
        Enemy_Time = Time.time + IdleTime;
        CanDoOtherAbility = true;
    }
    private void RiseLava()
    {
        StartCoroutine(Rise());
    }
    IEnumerator Rise()
    {
        for (int i = 0; i < maxRiseLavalSteps; i++)
        {
            Debug.Log("Rising lava");
            yield return new WaitForSeconds(2);
            lava.transform.localScale = new Vector3(lava.transform.localScale.x, lava.transform.localScale.y + riseLavaStepY, lava.transform.localScale.z);
        }
        StartCoroutine(RemoveLava());

    }
    IEnumerator RemoveLava()
    {
        for (int i = 0; i < maxRiseLavalSteps; i++)
        {
            yield return new WaitForSeconds(2);
            lava.transform.localScale = new Vector3(lava.transform.localScale.x, lava.transform.localScale.y - riseLavaStepY, lava.transform.localScale.z);
        }
        CanDoOtherAbility = true;
        Enemy_Time = Time.time + IdleTime;

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<HealthControl>().GetDamage(5, true, 5);
        }
    }

}

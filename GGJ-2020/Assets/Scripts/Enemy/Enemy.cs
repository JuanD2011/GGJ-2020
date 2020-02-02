using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(TrailTexture))]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private string id = "";
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected float damagePerSecond = 0.4f;
    [SerializeField] protected float initialHealth = 1;
    protected float health = 1;
    private Renderer mRenderer = null;
    private MaterialPropertyBlock mProperties;


    private bool stayOnTrigger = false;
    private float timer = 0f;

    private ISpoilable spoilableUnit = null;

    public string Id { get => id; private set => id = value; }

    EnemyMovement enemyMovement;

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        mRenderer = GetComponent<Renderer>();
    }

    void Start()
    {
        mProperties = new MaterialPropertyBlock();
        health = initialHealth;
    }

    void Update()
    {
        if (!PauseManager.paused)
        {
            enemyMovement.Movement();
            if (stayOnTrigger)
            {
                timer += Time.deltaTime;
                if (timer >= 1f)
                {
                    timer = 0f;
                    spoilableUnit?.Spoil(damagePerSecond);
                }
            }
            else timer = 0f; 
        }
    }


    /// <summary>
    /// Take damage and check if the enemy is dead
    /// </summary>
    /// <param name="_damage"></param>
    public void Damage(int _damage)
    {
        health -= _damage;
        print(health + " Health" + initialHealth + " InitialHealth");
        mRenderer.GetPropertyBlock(mProperties);
        mProperties.SetFloat("_CurrentHealthPercentage", health / initialHealth);
        mRenderer.SetPropertyBlock(mProperties);

        if (health <= 0) OnDead();
    }

    protected void OnDead()
    {
        gameObject.SetActive(false);
        ShootVFX();
    }

    private void ShootVFX()
    {
        OneShotParticles ps = PoolService.Instance.GetGameObjectFromPool("DeathVFX").GetComponent<OneShotParticles>();
        ps.transform.position = transform.position;
        ps.transform.rotation = transform.rotation;
        ps.StartParticles();
    }

    private void OnTriggerEnter(Collider other)
    {
        spoilableUnit = other.GetComponent<ISpoilable>();
        if (spoilableUnit != null && stayOnTrigger == false)
        {
            stayOnTrigger = true;
            spoilableUnit.Spoil(damagePerSecond);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        spoilableUnit = other.GetComponent<ISpoilable>();
        if (spoilableUnit != null) stayOnTrigger = false;
    }

    public void OnLimit()
    {
        //TODO: DISABLE ENEMY
    }
}

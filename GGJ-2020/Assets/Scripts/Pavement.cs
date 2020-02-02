using UnityEngine;

public class Pavement : MonoBehaviour, ISpoilable
{
    [SerializeField] private float initialHealthPoints = 100;
    private float healthPoints = 100;
    private Renderer mRenderer = null;
    private MaterialPropertyBlock mProperties;

    [SerializeField] private CurrentLevelData levelData = null;

    public event Delegates.Action<float> OnPavementDamaged = null;
    public bool textureDefined { get; set; } = false;
    public RenderTexture rt { get; set; } = null;
    public MeshRenderer mMeshRenderer { get; set; }

    private void Awake()
    {
        mRenderer = GetComponent<Renderer>();
        mMeshRenderer = GetComponent<MeshRenderer>();
        mProperties = new MaterialPropertyBlock();
    }

    private void Start()
    {
        healthPoints = initialHealthPoints;
        levelData.pavementStatus = healthPoints / initialHealthPoints;
    }

    public void Spoil(float _spoilAmount)
    {
        healthPoints = Mathf.Clamp(healthPoints - _spoilAmount, 0, initialHealthPoints);
        mRenderer.GetPropertyBlock(mProperties);
        mProperties.SetFloat("_CurrentReloadingValue", 1 - (healthPoints / initialHealthPoints));
        mRenderer.SetPropertyBlock(mProperties);
        levelData.pavementStatus = healthPoints / initialHealthPoints;
        OnPavementDamaged?.Invoke(_spoilAmount);
    }

    public void PatchUp(float _amount)
    {
        healthPoints = Mathf.Clamp(healthPoints + _amount, 0, initialHealthPoints);
        mRenderer.GetPropertyBlock(mProperties);
        mProperties.SetFloat("_CurrentReloadingValue", 1 - (healthPoints / initialHealthPoints));
        mRenderer.SetPropertyBlock(mProperties);
        if(healthPoints / initialHealthPoints == 1)
        {
            AudioManager.Instance.PlaySFx(AudioManager.Instance.audioClips.perfectCement, 1f, false);
            RenderTexture temp = (RenderTexture)mMeshRenderer.material.GetTexture("_Splat");
            temp.Release();
            Graphics.Blit(temp, rt);
            mMeshRenderer.material.SetTexture("_Splat", rt);
            //RenderTexture.ReleaseTemporary(temp);
        }
    }
}

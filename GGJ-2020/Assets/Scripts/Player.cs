using UnityEngine;

public enum PlayerState
{
    Attacking,
    Repairing,
}

/// <summary>
/// Player behaviour attached to use weapons and movement
/// </summary>
public class Player : MonoBehaviour
{
    private ParticleSystem repairVFX;
    private Weapon mWeapon = null;
    private Ray ray = new Ray();
    private RaycastHit rayHit = new RaycastHit();
    private RaycastHit previousFrameRayHit = new RaycastHit();
    private PlayerState mCurrentState = PlayerState.Attacking;
    private bool onPavement = false;
    private LayerMask layerMask;

    private void Awake() => mWeapon = GetComponentInChildren<Weapon>();

    private void Start()
    {
        layerMask = LayerMask.GetMask("RayCaster");
        ButtonPlayerStateChanger.OnChangePlayerState += ChangeState;
    }

    void Update()
    {
        if (mCurrentState == PlayerState.Attacking) Attacking();
        else if (mCurrentState == PlayerState.Repairing) Repairing();
    }

    private void Attacking()
    {
        //While touching the screen look at the touching position in the world and shoot using the refresh in the weapon
        if (Input.GetMouseButton(0))
        {
            if (Camera.main != null) ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out rayHit, 100, layerMask)) 
                transform.LookAt(new Vector3(rayHit.point.x, transform.position.y, rayHit.point.z), transform.up);
            mWeapon.Shoot();
            
        }
    }

    private void Repairing()
    {
        if (Input.GetMouseButtonDown(0))
        {
            repairVFX.Play();
            Physics.Raycast(ray, out previousFrameRayHit, 100, layerMask);
            repairVFX.transform.position = previousFrameRayHit.point;
        }
        else if (Input.GetMouseButton(0))
        {
            if (Camera.main != null) ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out rayHit, 100, layerMask))
            {
                AudioManager.Instance.PlaySFx(AudioManager.Instance.audioClips.revokingWall, 1f, true);
                Pavement currentPavement = rayHit.collider.gameObject.GetComponent<Pavement>();
                float distance = Vector3.Distance(rayHit.point, previousFrameRayHit.point);
                currentPavement?.PatchUp(distance);
                repairVFX.transform.position = rayHit.point;
                previousFrameRayHit = rayHit;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            AudioManager.Instance.StopByClip(AudioManager.Instance.audioClips.revokingWall);
            repairVFX.Stop();
        }
    }

    public void ChangeState(PlayerState _desiredPlayerState)
    {
        mCurrentState = _desiredPlayerState; 
        if(_desiredPlayerState == PlayerState.Repairing)
        {
            if(repairVFX == null)
                repairVFX = PoolService.Instance.GetGameObjectFromPool("RepairVFX").GetComponent<ParticleSystem>();
        }
        else
        {
            if (repairVFX != null)
            {
                repairVFX.Stop();
                PoolService.Instance.ReturnGameObjectToPools(repairVFX.gameObject, "RepairVFX");
                repairVFX = null;
            }
        }
    }
}

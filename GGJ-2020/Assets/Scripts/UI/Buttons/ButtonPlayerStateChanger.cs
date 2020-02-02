using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPlayerStateChanger : MonoBehaviour
{
    [SerializeField] private PlayerState driveToPlayerState = PlayerState.Attacking;
    private Button mButton = null;

    public static event System.Action<PlayerState> OnChangePlayerState = null;

    private void OnDestroy()
    {
        OnChangePlayerState = null;
    }

    private void Awake()
    {
        mButton = GetComponent<Button>();
    }

    void Start()
    {
        mButton.onClick.AddListener(() => OnChangePlayerState?.Invoke(driveToPlayerState));
    }
}

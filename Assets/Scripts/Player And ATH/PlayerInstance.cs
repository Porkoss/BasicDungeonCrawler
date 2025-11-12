using UnityEngine;

public class PlayerInstance : MonoBehaviour
{
    public static PlayerInstance Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public GameObject PlayerGameObject;

    public PlayerController PlayerController;

    public Animator PlayerAnimator;

    public GameObject PlayerFbx;

    private void Start()
    {
        PlayerController = PlayerGameObject.GetComponentInChildren<PlayerController>();
        PlayerAnimator = PlayerGameObject.GetComponentInChildren<Animator>();
    }

}

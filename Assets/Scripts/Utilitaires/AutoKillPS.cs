using UnityEngine;

public class AutoKillPS : MonoBehaviour
{
    ParticleSystem ps;

    void Awake() => ps = GetComponentInChildren<ParticleSystem>();

    void LateUpdate()
    {
        if (!ps.IsAlive(true))
            Destroy(gameObject);
    }
}
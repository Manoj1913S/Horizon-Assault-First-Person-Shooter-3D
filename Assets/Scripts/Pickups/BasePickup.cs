using UnityEngine;
using UnityEngine.AI;

public abstract class BasePickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed =100f;
    const string PLAYER_STRING = "Player";

  void Update()
    {
        transform.Rotate(0f, rotationSpeed*Time.deltaTime, 0f);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(PLAYER_STRING))
        {
            CurrentActiveWeapon activeWeapon = other.GetComponentInChildren<CurrentActiveWeapon>();
            OnPickup(activeWeapon);
            Destroy(this.gameObject);
        }
    }
    protected abstract void OnPickup(CurrentActiveWeapon activeWeapon);
}

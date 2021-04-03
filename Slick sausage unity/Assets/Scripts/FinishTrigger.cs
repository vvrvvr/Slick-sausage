using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform center;
    [SerializeField] private ParticleSystem particle;

    private void OnTriggerEnter(Collider other)
    {
        if( other.gameObject == player )
        {
            player.GetComponent<Player>().FinishGame();
            /// gamemanager finish staff
            Instantiate(particle, center.position, Quaternion.identity);
        }
    }
}

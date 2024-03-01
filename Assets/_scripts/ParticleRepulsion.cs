using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RutherFord.Physics
{    
    [RequireComponent(typeof(ParticleSpawner))]
    public class ParticleRepulsion : MonoBehaviour
    {
        [SerializeField] float particleForce = 100;
        [SerializeField] float minTrigRadius = 4.5f;
        [SerializeField] float MaxTrigRadius = 8.5f;
        float minParticles = 40f;
        float MaxParticles = 250f;
        SphereCollider sphereCollider;
        ParticleSpawner particleSpawner;

        private void Awake()
        {
            particleSpawner = GetComponent<ParticleSpawner>();
            sphereCollider = GetComponent<SphereCollider>();
            particleSpawner.OnParticleListUpdate += setTrigRadius;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "alpha particle")
            {
                Rigidbody rb;
                if (other.TryGetComponent<Rigidbody>(out rb))
                {
                    var dir = -(transform.position - other.transform.position).normalized;
                    rb.AddForce(particleForce * dir);
                }
                else
                {
                    Debug.LogError("RigidBody not found", other.gameObject);
                }
            }
            
        }

        void setTrigRadius(List<GameObject> particleList)
        {
            sphereCollider.radius = (MaxTrigRadius - minTrigRadius) * ((particleList.Count - minParticles) / (MaxParticles - minParticles)) + minTrigRadius;
        }
    }
}


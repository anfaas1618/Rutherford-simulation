using System.Collections.Generic;
using UnityEngine;

namespace RutherFord.Physics
{
    [RequireComponent(typeof(ParticleSpawner))]
    public class ParticleCohesion : MonoBehaviour
    {
        [SerializeField] float cohesionForce;
        [SerializeField] List<GameObject> particles;
        ParticleSpawner particleSpawner;

        private void Awake()
        {
            particles = new();
            particleSpawner = GetComponent<ParticleSpawner>();
            particleSpawner.OnParticleListUpdate += onParticleSpawn;
        }

        private void FixedUpdate()
        {
            foreach (GameObject other in particles)
            {
                Rigidbody rb;
                if (other.TryGetComponent<Rigidbody>(out rb))
                {
                    var dir = (transform.position - other.transform.position).normalized;
                    rb.AddForce(cohesionForce * dir);
                }
                else
                {
                    Debug.LogError("RigidBody not found", other.gameObject);
                }

            }
        }

        void onParticleSpawn(List<GameObject> particle)
        {
            particles = particle;
        }

    }
}


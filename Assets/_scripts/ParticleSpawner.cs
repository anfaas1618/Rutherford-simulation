using System;
using System.Collections.Generic;
using UnityEngine;

namespace RutherFord.Physics
{
    public class ParticleSpawner : MonoBehaviour
    {
        [SerializeField] GameObject protonGameObject;
        [SerializeField] GameObject neutronGameObject;
        [SerializeField] float spawnRadius;
        [SerializeField] int initialSpawnAmount = 10;
        Vector3 centerOffset;
        List<GameObject> protons;
        List<GameObject> neutrons;
        public Action<List<GameObject>> OnParticleListUpdate;

        private void Start()
        {
            centerOffset = transform.position;
            protons = new();
            neutrons = new();
            SpawnParticles(initialSpawnAmount, initialSpawnAmount);
        }

        public void SpawnParticles(int newProtonsCount, int newNeutronsCount)
        {
            if (newProtonsCount < protons.Count || newNeutronsCount<neutrons.Count)
            {
                despawn(protons.Count - newProtonsCount, neutrons.Count-newNeutronsCount);
            }
            else
            {
                spawn(newProtonsCount - protons.Count, newNeutronsCount - neutrons.Count);
            }

        }

        private void spawn(int pAmount,int nAmount)
        {
            for (int i = 1; i <= pAmount; i++)
            {
                var randPost = (UnityEngine.Random.onUnitSphere * spawnRadius) + centerOffset;
                var particle = Instantiate(protonGameObject, randPost, Quaternion.identity);
                protons.Add(particle);
                particle.transform.parent = transform;
            }
            for (int i = 1; i <= nAmount; i++)
            {
                var randPost = (UnityEngine.Random.onUnitSphere * spawnRadius) + centerOffset;
                var particle = Instantiate(neutronGameObject, randPost, Quaternion.identity);
                neutrons.Add(particle);
                particle.transform.parent = transform;
            }
            var newList = new List<GameObject>(protons);
            newList.AddRange(neutrons);
            OnParticleListUpdate?.Invoke(newList);

        }

        void despawn(int protonsToRemove,int neutronsToRemove)
        {
            for (int i = 1; i <= protonsToRemove; i++)
            {
                var randIndex = UnityEngine.Random.Range(0, protons.Count);
                var particle = protons[randIndex];
                protons.RemoveAt(randIndex);
                Destroy(particle);
            }
            for (int i = 1; i <= neutronsToRemove; i++)
            {
                var randIndex = UnityEngine.Random.Range(0, neutrons.Count);
                var particle = neutrons[randIndex];
                neutrons.RemoveAt(randIndex);
                Destroy(particle);
            }
            var newList = new List<GameObject>(protons);
            newList.AddRange(neutrons);
            OnParticleListUpdate?.Invoke(newList);
        }

    }
}

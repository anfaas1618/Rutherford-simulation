using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RutherFord.Physics
{
    public class AlphaParticleSpawner : MonoBehaviour
    {
        [SerializeField] GameObject alphaParticleGameObject;
        [SerializeField] float spawnDelay;
        [SerializeField] float autoDestructTimer = 1;
        [SerializeField] Toggle tracesToggle;
        [Range(1, 3)]
        public float Speed;

        private void OnEnable()
        {
            StartCoroutine(spawn());
        }
        private void OnDisable()
        {
            foreach (SpriteRenderer x in transform.GetComponentsInChildren<SpriteRenderer>())
            {
                Destroy(x.gameObject);
            }
        }

        IEnumerator spawn()
        {
            while (true)
            {
                var xPost = Random.Range(-5.31f, 4.21f);
                var randPost = new Vector3(xPost, -4.65f, 0);
                var alpha = Instantiate(alphaParticleGameObject, randPost, Quaternion.identity);
                Destroy(alpha, autoDestructTimer);
                alpha.transform.parent = transform;
                alpha.GetComponent<TrailRenderer>().startWidth = (tracesToggle.isOn) ? (0.203f) : (0);
                alpha.GetComponent<Rigidbody>().velocity = Vector3.up * Speed;
                yield return new WaitForSeconds(spawnDelay);
            }
        }

    }
}

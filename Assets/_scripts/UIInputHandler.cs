using RutherFord.Physics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace RutherFord.UI
{
    public class UIInputHandler : MonoBehaviour
    {
        [SerializeField] Slider energySlider;
        [SerializeField] Slider neutronSlider;
        [SerializeField] TextMeshProUGUI neutronTMPro;
        [SerializeField] TextMeshProUGUI protonTMPro;
        [SerializeField] Slider protonSlider;
        [SerializeField] AlphaParticleSpawner alphaParticleSpawner;
        [SerializeField] ParticleSpawner particleSpawner;

        public void OnEnergyChange()
        {
            alphaParticleSpawner.Speed = energySlider.value;
        }

        public void OnProtonNeutronChange()
        {
            particleSpawner.SpawnParticles((int)protonSlider.value, (int)neutronSlider.value);
            neutronTMPro.text = (int)neutronSlider.value + "";
            protonTMPro.text = (int)protonSlider.value + "";
        }


        public void OnTracesOn(Toggle toggle)
        {
            bool isOn = toggle.isOn;
            foreach (TrailRenderer x in alphaParticleSpawner.transform.GetComponentsInChildren<TrailRenderer>())
            {
                x.startWidth = (isOn) ? (0.203f) : (0);
            }
        }

        public void Restart()
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}

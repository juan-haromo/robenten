using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "RangeUltimate", menuName = "Player/Abilities/Range/Ultimate")]
public class RangeUltimate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Camera.main.TryGetComponent(out CinemachineBrain brain))
        {
            //Debug.Log(brain.ActiveVirtualCamera.
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

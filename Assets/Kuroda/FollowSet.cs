using Unity.Cinemachine;
using UnityEngine;

public class FollowSet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FindAnyObjectByType<CinemachineCamera>().Follow = transform;
    }
}

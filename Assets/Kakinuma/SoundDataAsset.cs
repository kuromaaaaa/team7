using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundDataAsset", menuName = "ScriptableObjects/CreateSoundData")]
public class SoundDataAsset : ScriptableObject
{
    private const string PATH = "SoundDataAsset";

    private static SoundDataAsset _entity;

    public static SoundDataAsset Entity
    {
        get
        {
            if (_entity == null)
            {
                // 初アクセス時にロード
                _entity = Resources.Load<SoundDataAsset>(PATH);

                // ロードできなかったとき
                if (_entity == null)
                {
                    Debug.LogWarning($"{PATH}にSoundAssetDataが見つかりません");
                }
            }
            return _entity;
        }
    }

    [SerializeField] private List<SoundData> _soundSetList;
    [SerializeField] private GameObject _soundPlayerParentObject;
    
    public List<SoundData> SoundSetList => Entity._soundSetList;
    public GameObject SoundPlayerParentObject => _soundPlayerParentObject;
}

[Serializable]
public struct SoundData
{
    [SerializeField] private AudioClipType _audioClipType;
    [SerializeField] private AudioClip _audioClip;

    public AudioClipType AudioClipType => _audioClipType;
    public AudioClip AudioClip => _audioClip;
}
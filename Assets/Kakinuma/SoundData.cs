using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "ScriptableObjects/CreateSoundData")]
public class SoundDataAsset : ScriptableObject
{
    public const string PATH = "SoundData";

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
    public List<SoundData> SoundSetList => Entity._soundSetList;
}

[Serializable]
public struct SoundData
{
    [SerializeField] private AudioClipType _audioClipType;
    [SerializeField] private AudioClip _audioClip;

    public AudioClipType AudioClipType => _audioClipType;
    public AudioClip AudioClip => _audioClip;
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAudioManager : MonoBehaviour
{
    public AudioClip notLevelUp;
    public AudioClip levelUp;
    public AudioClip jump;
    public AudioClip shoot;
    public AudioClip stealCollect;
    public AudioClip hintCollect;
    public AudioClip dieThief;
    public AudioClip dieGuard;

    public Queue<AudioClip> queue;
    private AudioSource _audioSource;
    void Awake()
    {
        this.queue=new Queue<AudioClip>();
        this._audioSource = GetComponent<AudioSource>();
        IconManager.PanelActionActivated+=HandlePanelActions;
        Dropzone.DiceLevelupFailEvent+=HandleLevelUpFail;
        Dropzone.DiceLevelupEvent+=HandleLevelup;
        
    }

    private void HandleLevelup(GameObject arg1, UIIconController arg2)
    {
        AddToAudioQueue(levelUp);
    }

    private void HandleLevelUpFail(GameObject arg1, UIIconController arg2)
    {
        AddToAudioQueue(notLevelUp);
    }
    

    private void HandlePanelActions(ActionTypes arg1, GameObject dropzone)
    {
        AddPanelSound(arg1);
        if (dropzone.layer == LayerMask.NameToLayer("Day")){
            AddPanelSound(arg1);
        }
        else if (dropzone.layer == LayerMask.NameToLayer("Night")){
            AddPanelSound(arg1);
        }

    }

    public void AddPanelSound(ActionTypes actionType){
        switch (actionType)
        {
            case ActionTypes.jump:
                AddToAudioQueue(jump);
                break;
            case ActionTypes.shoot:
                AddToAudioQueue(shoot);
                break;
            default:
                break;
        }
    }

    void AddToAudioQueue(AudioClip audioClip){
        queue.Enqueue(audioClip);
    }
    void AddToAudioQueue(List <AudioClip> audioClips){
        
        foreach (var clip in audioClips)
        {
            queue.Enqueue(clip);
        }
    }

    void Update()
    {
        // if (!_audioSource.isPlaying){
        if (true){
            if (queue.Count>0){
                _audioSource.clip= queue.Dequeue();
                _audioSource.Play();
            }
        }
    }


}

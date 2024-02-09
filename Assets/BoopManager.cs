using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoopManager : MonoBehaviour
{
    public AudioSource boopSound;
    
    public static BoopManager boopManager { get; private set; }
    private void Awake() { 
        // If there is an instance, and it's not me, delete myself.
        if (boopManager != null && boopManager != this) { 
            Destroy(this.gameObject); 
        } else { 
            boopManager = this; 
            DontDestroyOnLoad(this.gameObject);
        } 
    }

    public void PlayBoopSoud() {
        boopSound.Play();
    }
}

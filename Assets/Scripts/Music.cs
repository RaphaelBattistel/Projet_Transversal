using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioClip musicClip;
    Collider2D collider2d;
    private AudioSource AudioSource;
    

    void Awake()
    {
        collider2d = GetComponent<Collider2D>();
        AudioSource = GetComponent<AudioSource>();
    }
    private void OnMouseDown()
    {
        AudioSource.clip = musicClip;
        AudioSource.Play();
        
    }
}

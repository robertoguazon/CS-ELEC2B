using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Bomb : MonoBehaviour, ButtonParent {

    [SerializeField]
    private Sprite pressedBombSprite;
    [SerializeField]
    private float activateBombDelay = 0.5f;
    [SerializeField]
    private float explodeBombDelay = 2.5f;
    [SerializeField]
    private Animator bombAnimator;
    [SerializeField]
    private float bombRadius = 1f;
    [SerializeField]
    private int bombPower = 10;
    [SerializeField]
    private LayerMask affectedByBomb;
    [SerializeField]
    private AudioClip explosionSound;
    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float explosionVolume = 0.5f;


    private AudioSource _audioSource;
    private bool pressed = false;

    private List<Collider2D> _colliders;

    void Start() {
        _colliders = new List<Collider2D>();
        _audioSource = gameObject.AddComponent<AudioSource>();

        _audioSource.clip = explosionSound;
        _audioSource.volume = explosionVolume;
    }

    public void Press() {
        if (!pressed) {
            pressed = true;
            bombAnimator.SetTrigger("PressBomb");
            StartCoroutine(AnimateBomb());
        }
    }

    public void PlayExplosionSound() {
        _audioSource.Play();
    }

    private IEnumerator AnimateBomb() {
        yield return StartCoroutine(ActivateBomb(activateBombDelay));
        yield return StartCoroutine(ExplodeBomb(explodeBombDelay));
        yield return StartCoroutine(DestroyObject());
    }

    private IEnumerator ActivateBomb(float delay) {
        yield return new WaitForSeconds(delay);

        //activate the bomb after delay
        bombAnimator.SetTrigger("ActivateBomb");
    }

    private IEnumerator ExplodeBomb(float delay) {
        yield return new WaitForSeconds(delay);

        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Collider2D>().enabled = false;
        enabled = false;
        //explode the bomb after delay
        PlayExplosionSound();
        bombAnimator.SetTrigger("ExplodeBomb");

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, bombRadius, affectedByBomb, -1,2);
        Debug.Log(colliders.Length); // TODO delete
        for (int i = 0; i < colliders.Length; i++) {
            Debug.Log(colliders[i].name); // TODO delete
          if (colliders[i].tag == "Player") {
                CharacterController2D player = colliders[i].GetComponent<CharacterController2D>();
                player.ApplyDamage(bombPower);
          } else if (colliders[i].tag == "Bomb") {
                Bomb bomb = colliders[i].GetComponent<Bomb>();
                bomb.Press();
            }                                              
        }
        
    }

    private IEnumerator DestroyObject() {
        yield return new WaitForSeconds(2f);

        Destroy(this.gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y, 0), bombRadius);
    }
}

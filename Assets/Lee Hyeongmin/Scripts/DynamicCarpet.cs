using UnityEngine;

public class DynamicCarpet : MonoBehaviour
{
    public MonsterCtrl monster;

    private Renderer carpetRenderer;

    // 임시 소리 재생
    private AudioSource audioSource;
    public AudioClip audioClip;
    private bool oneShot = false;

    private void Awake()
    {
        carpetRenderer = GetComponent<Renderer>();
        carpetRenderer.enabled = false;

        //audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            // 발판 렌더링 및 오디오 재생
            carpetRenderer.enabled = true;

            if (oneShot == false)
            {
                //audioSource.PlayOneShot(audioClip);
                AudioManager.Instance.PlayOneSound(audioClip);
                oneShot = true;
            }

            // 몬스터에게 내쪽으로 오라함
            monster.HeardSound(transform.position);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            print("벗어남");
            carpetRenderer.enabled = false;
            oneShot = false;
        }
    }
}

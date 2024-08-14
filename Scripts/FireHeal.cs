using UnityEngine;

public class FireHeal : MonoBehaviour
{
    public float healRate = 0.01f; // 1 health point out of 100 per second
    public float healInterval = 1f; // once per second
    public float healRadius = 3f;
    public LayerMask playerLayer;
    public GameObject healingEffectObject;

    private Animator healingEffectAnimator;
    private float lastHealTime;

    private void Start()
    {
        healingEffectAnimator = healingEffectObject.GetComponent<Animator>();

        if (healingEffectAnimator == null)
        {
            Debug.LogError("Animator component not found on healingEffectObject.");
        }
    }

    private void Update()
    {
        if (Time.time - lastHealTime >= healInterval)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, healRadius, playerLayer);

            bool shouldStartHealing = false;

            foreach (Collider2D collider in colliders)
            {
                if ((playerLayer.value & (1 << collider.gameObject.layer)) != 0)
                {
                    MainCharacter mainCharacter = collider.GetComponent<MainCharacter>();

                    if (mainCharacter != null)
                    {
                        mainCharacter.RestoreHealth(1f);

                        shouldStartHealing = true;
                    }
                }
            }

            lastHealTime = Time.time;

            if (shouldStartHealing)
            {
                healingEffectAnimator.SetBool("isHealing", true);
            }
            else
            {
                healingEffectAnimator.SetBool("isHealing", false);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, healRadius);
    }
}
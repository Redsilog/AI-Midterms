using UnityEngine;

public class CrawlForward : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float crawlSpeed = 1f;

    [Header("Animation Settings")]
    [SerializeField] private string crawlAnimationName = "Crawl";

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.applyRootMotion = false;
            animator.Play(crawlAnimationName);
        }
    }

    void Update()
    {
        Vector3 forward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        transform.position += forward * crawlSpeed * Time.deltaTime;
    }
}

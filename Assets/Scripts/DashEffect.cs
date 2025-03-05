using System.Collections;
using UnityEngine;

public class DashEffect : MonoBehaviour
{
    public GameObject ghostPrefab; // Prefab da imagem fantasma
    public float ghostLifetime = 0.3f; // Quanto tempo o fantasma fica visível
    public float ghostSpawnRate = 0.05f; // Intervalo entre fantasmas

    private bool isDashing = false;

    public void StartDashEffect()
    {
        isDashing = true;
        StartCoroutine(GhostTrail());
    }

    public void StopDashEffect()
    {
        isDashing = false;
    }

    private IEnumerator GhostTrail()
    {
        while (isDashing)
        {
            GameObject ghost = Instantiate(ghostPrefab, transform.position, transform.rotation);
            SpriteRenderer ghostRenderer = ghost.GetComponent<SpriteRenderer>();
            ghostRenderer.sprite = GetComponent<SpriteRenderer>().sprite;

            Destroy(ghost, ghostLifetime);
            yield return new WaitForSeconds(ghostSpawnRate);
        }
    }
}

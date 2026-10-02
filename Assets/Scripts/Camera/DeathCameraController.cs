using UnityEngine;
using System.Collections;

public class DeathCameraController : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera deathCamera;

    [Header("Player")]
    [SerializeField] private Transform player;

    [Header("Transition")]
    [SerializeField] private float transitionDuration = 1.5f;

    private bool deathCameraActivated = false;

    // Last position/rotation of the FPS camera
    private Vector3 lastCameraPosition;
    private Quaternion lastCameraRotation;

    // Death camera's final position/rotation
    private Vector3 deathPosition;
    private Quaternion deathRotation;

    private void Start()
    {
        if (mainCamera != null)
        {
            mainCamera.gameObject.SetActive(true);

            lastCameraPosition = mainCamera.transform.position;
            lastCameraRotation = mainCamera.transform.rotation;
        }

        if (deathCamera != null)
        {
            // Remember where Death Camera should finally go
            deathPosition = deathCamera.transform.position;
            deathRotation = deathCamera.transform.rotation;

            deathCamera.gameObject.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        // Keep remembering the latest FPS camera position
        // while player is alive
        if (mainCamera != null)
        {
            lastCameraPosition = mainCamera.transform.position;
            lastCameraRotation = mainCamera.transform.rotation;
        }

        // Player has been destroyed
        if (player == null && !deathCameraActivated)
        {
            ActivateDeathCamera();
        }
    }

    private void ActivateDeathCamera()
    {
        deathCameraActivated = true;

        if (deathCamera == null)
        {
            Debug.LogError("Death Camera is not assigned!");
            return;
        }

        // Put Death Camera exactly where FPS camera was
        deathCamera.transform.SetPositionAndRotation(
            lastCameraPosition,
            lastCameraRotation
        );

        // Activate Death Camera
        deathCamera.gameObject.SetActive(true);

        // Slowly move to final death-camera position
        StartCoroutine(DeathCameraTransition());
    }

    private IEnumerator DeathCameraTransition()
    {
        float elapsed = 0f;

        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / transitionDuration;

            // Smooth start and smooth end
            t = Mathf.SmoothStep(0f, 1f, t);

            deathCamera.transform.position =
                Vector3.Lerp(
                    lastCameraPosition,
                    deathPosition,
                    t
                );

            deathCamera.transform.rotation =
                Quaternion.Slerp(
                    lastCameraRotation,
                    deathRotation,
                    t
                );

            yield return null;
        }

        // Make sure final position is exact
        deathCamera.transform.SetPositionAndRotation(
            deathPosition,
            deathRotation
        );
    }
}
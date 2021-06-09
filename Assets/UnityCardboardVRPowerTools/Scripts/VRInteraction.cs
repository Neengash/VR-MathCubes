using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class VRInteraction : MonoBehaviour
{
    [SerializeField, Range(0.5f, 10f)]
    private float LoadTime = 2f;

    [SerializeField] public Color holdColor = new Color(.9f, .9f, .9f, 1f);
    [SerializeField] public Color clickColor = new Color(.5f, .5f, .5f, 1f);
    Coroutine loadingCoroutine;

    [SerializeField]
    UnityEvent onPointerEnter, onPointerExit, onPointerClick, onPointerLoad;

    public void OnPointerEnter() {
        onPointerEnter?.Invoke();

        if (onPointerLoad.GetPersistentEventCount() > 0) {
            loadingCoroutine = StartCoroutine(Loading());
        }
    }

    public void OnPointerExit() {
        onPointerExit?.Invoke();

        if (loadingCoroutine != null) {
            StopCoroutine(loadingCoroutine);
            loadingCoroutine = null;
            VRPointerLoader.instance.resetLoad();
        }
    }

    public void OnPointerClick() {
        onPointerClick?.Invoke();
    }

    IEnumerator Loading() {
        float time = Time.deltaTime;
        VRPointerLoader.instance.SetMaxLoad(LoadTime);

        while (time < LoadTime) {
            VRPointerLoader.instance.SetLoad(time);
            yield return null;
            time += Time.deltaTime;
        }

        onPointerLoad?.Invoke();
        loadingCoroutine = null;
        VRPointerLoader.instance.resetLoad();
    }

    private void OnDestroy() {
        if (loadingCoroutine != null) {
            loadingCoroutine = null;
            VRPointerLoader.instance.resetLoad();
        }
    }
}

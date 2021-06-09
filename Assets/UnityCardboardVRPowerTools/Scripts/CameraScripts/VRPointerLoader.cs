using UnityEngine;
using UnityEngine.UI;

public class VRPointerLoader : MonoBehaviour
{
    public static VRPointerLoader instance;
    [SerializeField] Slider loadingSlider;

    void Start()
    {
        instance = this;
        //loadingSlider.maxValue = 1f;
        loadingSlider.value = 0f;
    }

    public void SetMaxLoad(float maxLoad) {
        loadingSlider.maxValue = maxLoad;
        loadingSlider.value = 0f;
    }

    public void SetLoad(float load) {
        loadingSlider.value = load;
    }

    public void resetLoad() {
        loadingSlider.value = 0f;
    }
}

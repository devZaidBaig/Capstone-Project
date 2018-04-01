using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FpsDisplay : MonoBehaviour
{
    float deltaTime = 0.0f;
    public Text FpsText;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        FpsText.text = fps + " :: " + msec;
    }
}

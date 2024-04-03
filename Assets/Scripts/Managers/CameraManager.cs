using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    protected override void Awake()
    {
        base.Awake();
        PrepareCamera();
    }

    private void PrepareCamera()
    {
        var cam = GetComponent<Camera>();

        Vector2 nativeRes = new Vector2(1080, 1920); 
        float pixelsToUnit = 100; 

        float scale = Screen.height / nativeRes.y;
        pixelsToUnit *= scale;
        cam.orthographicSize = (Screen.height / 2.0f) / pixelsToUnit;
    }

    public void TriggerShakeEffect()
    {
        ShakeAnimation.ApplyShakeAnimation(transform, 0.5f, 0.2f);
    }
}

using UnityEngine;
using DG.Tweening;

/// <summary>
///
/// The ShakeAnimation class is responsible for managing the shake animation of a Transform.
/// It contains a method to apply the shake animation.
///
/// </summary>
public static class ShakeAnimation
{
    public static Tween ApplyShakeAnimation(Transform transform, float duration, float strength)
    {
        return transform.DOShakePosition(duration, strength);
    }
}

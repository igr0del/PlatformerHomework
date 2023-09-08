using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignationSelectionCoinByPlayer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Color _reachedColor;

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if (collision.TryGetComponent<HeroController>(out HeroController controller))
            _renderer.color = _reachedColor;
    }
}

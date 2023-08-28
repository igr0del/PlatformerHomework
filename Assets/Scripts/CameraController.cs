using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private Vector3 _position;

    private float _fixPozitionZAxis = -9f;

    private void Awake()
    {
        if (!_player)
            _player = FindObjectOfType<HeroController>().transform;
    }

    private void Update()
    {
        _position = _player.position;
        _position.z = _fixPozitionZAxis;

        transform.position = Vector3.Lerp(transform.position, _position, Time.deltaTime);
    }
}

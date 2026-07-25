
    using System;
    using UnityEngine;

    public class CameraFollow : MonoBehaviour
    {
        private Transform _player;
        private const float SmoothSpeed = 5f;
        private readonly Vector3 _offset = new (0, 0, -10);


        private const float DeadZoneWidth = 10f;
        private const float DeadZoneHeight = 8f;
        private Vector3 _targetPos;

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            gameObject.transform.position = GameController.Instance.initCameraPos();
        }

        void LateUpdate()
        {
            if (_player == null) return;

            // Current camera position (without offset)
            Vector3 camPos = transform.position - _offset;
            Vector3 playerPos = _player.position;

            // Compute horizontal offset
            float dx = playerPos.x - camPos.x;
            if (Mathf.Abs(dx) > DeadZoneWidth / 2f)
                camPos.x = playerPos.x - Mathf.Sign(dx) * (DeadZoneWidth / 2f);

            // Compute vertical offset
            float dy = playerPos.y - camPos.y;
            if (Mathf.Abs(dy) > DeadZoneHeight / 2f)
                camPos.y = playerPos.y - Mathf.Sign(dy) * (DeadZoneHeight / 2f);

            // Apply smooth movement
            _targetPos = camPos + _offset;
            transform.position = Vector3.Lerp(transform.position, _targetPos, SmoothSpeed * Time.deltaTime);
        }
    }

    

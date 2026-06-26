using UnityEngine;

namespace Tool
{
    public class Billboard : MonoBehaviour
    {
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void LateUpdate()
        {
            if (!_camera) return;

            transform.rotation = _camera.transform.rotation;
        }
    }
}
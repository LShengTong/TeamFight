using JetBrains.Annotations;
using Service;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Flow
{
    public class HeroMoveService : MonoBehaviour
    {
        [SerializeField] private Heroes heroes;
        [SerializeField] private HeroPlacementService placementService;
    
        private Camera _camera;
        [CanBeNull] private HeroView _draggingHeroView;
        private Vector3 _startPosition;
        [CanBeNull] private SlotView _selectedSlotView;
    
        public void Start()
        {
            _camera = Camera.main;
        }
    
        private void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                var ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (!Physics.Raycast(ray, out var hit)) return;
                var heroView = hit.collider.GetComponentInParent<HeroView>();
                if(!heroView || heroView.isEnemy) return;
                _draggingHeroView = heroView;
                _startPosition = _draggingHeroView.transform.position;
                return;
            }

            if (Mouse.current.leftButton.isPressed)
            {
                if (!_draggingHeroView) return;
                var ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
                var plane = new Plane(Vector3.up, _startPosition);
                if (!plane.Raycast(ray, out var enter)) return;
                _draggingHeroView.transform.position = ray.GetPoint(enter);
                _selectedSlotView?.SetSelected(false);
                _selectedSlotView = null;
                if (Physics.Raycast(ray, out var hit, 1000f, LayerMask.GetMask("Slot")))
                {
                    var hitSlot = hit.collider.GetComponentInParent<SlotView>();
                    if (hitSlot && !hitSlot.isEnemy)
                    {
                        _selectedSlotView = hitSlot;
                        _selectedSlotView.SetSelected(true);
                    }
                }
            }

            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                if(!_draggingHeroView) return;
                _draggingHeroView.transform.position = _startPosition;
                if (_selectedSlotView)
                {
                    _selectedSlotView.SetSelected(false);
                    placementService.TryPlace(_draggingHeroView.id, _selectedSlotView.index);
                    _selectedSlotView = null;
                }
                _draggingHeroView = null;
            }
        }
    }
}
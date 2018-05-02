using System;
using UnityEngine;

namespace Controls {
    [RequireComponent(typeof(LineRenderer))]
    public class Ball : MonoBehaviour {
        public GameObject BallGameObeject;

        private bool _dragged;
        private bool _launched;
        private GameObject _ball;
        private LineRenderer _line;
        private Vector2 _dragLocation;
        private Vector2 _firstPressLocation;
    
        private const int MaxDist = 150;

        // Use this for initialization
        private void Start () {
            _line = GetComponent<LineRenderer>();
            _line.SetWidth(1, 1);
            _line.SetVertexCount(2);
        }

        // Update is called once per frame
        private void Update () {
            Vector2 dir;
            
            foreach (var touch in Input.touches) {
                if (!_launched) {
                    Vector3 pos;
                    switch (touch.phase) {
                        case TouchPhase.Began:
                            pos = Camera.main.ScreenToWorldPoint(touch.position);

                            _ball = Instantiate(BallGameObeject, pos, Quaternion.identity);
                            
                            _firstPressLocation = pos;
                            _dragLocation = pos;
                            break;
                        case TouchPhase.Moved:
                            pos = Camera.main.ScreenToWorldPoint(touch.position);
    
                            if (!_dragged) {
                                _dragged = true;
                            }
                            
                            _dragLocation = pos;
                            break;
                        case TouchPhase.Stationary:
                            pos = Camera.main.ScreenToWorldPoint(touch.position);
                            _dragLocation = pos;
                            break;
                        case TouchPhase.Ended:
                            dir = (_dragLocation - _firstPressLocation).normalized;
    
                            if (_dragged) {
                                LaunchBall(dir);
                            } else {
                                Destroy(_ball, 0);
                                _ball = null;
                            }
                            
                            _firstPressLocation = Vector2.zero;
                            _dragLocation = Vector2.zero;
                            break;
                        case TouchPhase.Canceled:
                            _firstPressLocation = Vector2.zero;
                            _dragLocation = Vector2.zero;
                            
                            Destroy(_ball, 0);
                            _ball = null;
                            
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                } else {
                    return;
                }
            }

            dir = (_dragLocation - _firstPressLocation).normalized;
            var dist = Mathf.Clamp(Vector2.Distance(_firstPressLocation, _dragLocation), 0, MaxDist);
            _dragLocation = _firstPressLocation - (dir * dist);

            _line.SetPosition(0, _firstPressLocation);
            _line.SetPosition(1, _dragLocation);
        }

        private void LaunchBall (Vector2 direction) {
            _dragged = false;
            
            _ball.GetComponent<Rigidbody2D>().velocity = direction * 150;

            _launched = true;
        }
    }
}
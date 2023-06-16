using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Vector2 direction;
    public virtual void SetDirection(Vector2 dir) {
        this.direction = dir;
    }
    
    public Vector2 GetDirection() {
        return direction;
    }
    
    public Vector3 GetDirectionTo(Vector2 source, Vector2 target) {
        Vector2 position1 = source;
        Vector2 position2 = target;
        Vector2 difference = position2 - position1;
        Vector2 d = difference.normalized;
        return d.normalized;
    }
    public Vector3 GetDirectionTo(Vector3 source, Vector3 target) {
        Vector2 position1 = source;
        Vector2 position2 = target;
        Vector2 difference = position2 - position1;
        Vector2 d = difference.normalized;
        return d.normalized;
    }
}

using UnityEngine;
using System.Collections;
[RequireComponent (typeof (BoxCollider2D))]
public class Controller2D : MonoBehaviour {
    public LayerMask collisionMask;

    const float skinWidth = 0.015f;
    public int horizontalRayCount = 2;
    public int verticalRayCount = 2;

    float horizontalRaySpacing;
    float verticalRaySpacing;

    BoxCollider2D col;
    RaycastOrigins raycastOrigins;

	// Use this for initialization
	void Start () {

        col = GetComponent<BoxCollider2D>();
   

    }
    
    public void Move(Vector3 velocity){

        CalculateRaySpacing();
        UpdateRaycastOrigins();
       
        if (velocity.x != 0) {
            HorizontalCollisions(ref velocity);
        }
        if (velocity.y != 0) {
            VerticalCollisions(ref velocity);
        }

        transform.Translate(velocity,Space.World);
    }

    void VerticalCollisions(ref Vector3 velocity) {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;
        for (int i = 0; i < verticalRayCount; i++) {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;

            Debug.DrawRay(rayOrigin + Vector2.right * (verticalRaySpacing * i), Vector2.up * directionY * 2, Color.red);

            rayOrigin += Vector2.right * (verticalRaySpacing * i);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);            
           
            if (hit) {
                
                velocity.y = (hit.distance-skinWidth) * directionY;
                rayLength = hit.distance;

            }
        }
    }

    void HorizontalCollisions(ref Vector3 velocity) {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;
        for (int i = 0; i < verticalRayCount; i++) {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;

            Debug.DrawRay(rayOrigin + Vector2.up * (horizontalRaySpacing * i), Vector2.right * directionX * 2, Color.red);

            rayOrigin += Vector2.up * (horizontalRaySpacing * i);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            if (hit) {

                velocity.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;

            }
        }
    }

    void UpdateRaycastOrigins() {
    
        Bounds bounds = col.bounds;
        bounds.Expand(skinWidth*-2);
        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing() {
        Bounds bounds = col.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);


    }
    struct RaycastOrigins {

        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSensor
{
   public float castLength = 1f;
   public LayerMask layerMask = 255;
   private Vector3 origin;
   private Transform tr;
   public enum CastDirection{Forward, Left, Right, Up, Down,Backward}
   CastDirection castDirection;
   RaycastHit hitInfo;

   public RaycastSensor(Transform player)
   {
      tr = player;
   }

   public void Cast()
   {
      Vector3 worldOrigin = tr.TransformPoint(origin);
      Vector3 worldDirection = GetCastDirection();
      
      Physics.Raycast(worldOrigin, worldDirection, out hitInfo, castLength, layerMask, QueryTriggerInteraction.Ignore);
      //Debug.Log(hitInfo.collider.name);
   }
   public bool HasDetectedHit() => hitInfo.collider != null;
   public float GetDistance() => hitInfo.distance;
   public Vector3 GetNormal() => hitInfo.normal;
   public Vector3 GetPosition() => hitInfo.point;
   public Collider GetCollider() => hitInfo.collider;
   public Transform GetTransform() => hitInfo.transform;
   
   public void SetCastDirection(CastDirection direction) => castDirection = direction;
   public void SetCastOrigin(Vector3 pos) => this.origin = tr.InverseTransformPoint(pos);
   
   private Vector3 GetCastDirection()
   {
      return castDirection switch
      {
         CastDirection.Forward => tr.forward,
         CastDirection.Left => -tr.right,
         CastDirection.Right => tr.right,
         CastDirection.Up => tr.up,
         CastDirection.Down => -tr.up,
         CastDirection.Backward => -tr.forward,
         _ => Vector3.one
      };
   }
   public void DrawDebug() {
      //Debug.Log(HasDetectedHit());
      if (!HasDetectedHit()) return;

      Debug.DrawRay(hitInfo.point, hitInfo.normal, Color.red, Time.deltaTime);
      float markerSize = 5f;
      Debug.DrawLine(hitInfo.point + Vector3.up * markerSize, hitInfo.point - Vector3.up * markerSize, Color.green, Time.deltaTime);
      Debug.DrawLine(hitInfo.point + Vector3.right * markerSize, hitInfo.point - Vector3.right * markerSize, Color.green, Time.deltaTime);
      Debug.DrawLine(hitInfo.point + Vector3.forward * markerSize, hitInfo.point - Vector3.forward * markerSize, Color.green, Time.deltaTime);
   }
}

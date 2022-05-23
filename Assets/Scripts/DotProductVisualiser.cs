using System;
using System.Collections;
using System.Collections.Generic;
using Shapes;
using UnityEngine;

[ExecuteAlways]
public class DotProductVisualiser : ImmediateModeShapeDrawer
{
    public Line lineA;
    public Line lineB;
    public Line projectionLine;
    public Line extensionLine;

    private Vector3 a;
    private Vector3 b;
    
    public override void DrawShapes( Camera cam ){

        using( Draw.Command( cam ) )
        {
            Draw.BlendMode = ShapesBlendMode.Opaque;
            GetVectors();
            SetProjectionLine();
        }
    }

    private void GetVectors()
    {
        Vector3 tipA = lineA.transform.Find("Tip").position;
        Vector3 rootA = lineA.transform.Find("Root").position;
        Vector3 tipB = lineB.transform.Find("Tip").position;
        Vector3 rootB = lineB.transform.Find("Root").position;
        
        a = tipA - rootA;
        b = tipB - rootB;

    }
    
    private void SetProjectionLine()
    {
        Vector3 tipA = lineA.transform.Find("Tip").position;
        Vector3 rootB = lineB.transform.Find("Root").position;
        
        projectionLine.Start = tipA;

        float projectionMagnitude = Vector3.Dot(a, b) / b.magnitude;
        Vector3 projectedVector = b.normalized * projectionMagnitude;
        Vector3 projectionEndPoint = rootB + projectedVector;
        
        projectionLine.End = projectionEndPoint;

        extensionLine.Start = rootB;
        extensionLine.End = projectionEndPoint;

    }

}

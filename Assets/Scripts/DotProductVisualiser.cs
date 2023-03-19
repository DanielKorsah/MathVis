using System;
using System.Collections;
using System.Collections.Generic;
using Shapes;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class DotProductVisualiser : ImmediateModeShapeDrawer
{
    public Line lineA;
    public Line lineB;
    public Line projectionLine;
    public Line extensionLine;
    public Disc angleArc;

    public TMP_Text dotOutputText;
    public TMP_Text angleOutputText;
    public TMP_Text magnitudeOutputText;

    private Vector3 a;
    private Vector3 b;
    private float angle;
    
    public override void DrawShapes( Camera cam ){

        using( Draw.Command( cam ) )
        {
            Draw.BlendMode = ShapesBlendMode.Opaque;
            GetVectors();
            SetProjectionLine();
            ShowNumbers();
            ShowArc();
        }
    }

    private void ShowArc()
    {
        float signedAngleA = SignedAngleBetween(Vector3.right, a);
        float signedAngleB = SignedAngleBetween(Vector3.right, b);

        float angleDifference = Mathf.Abs(signedAngleB - signedAngleA);
        float smallestAngle = Mathf.Min(angleDifference, 2 * Mathf.PI - angleDifference);

        angleArc.AngRadiansStart = Mathf.Min(signedAngleA, signedAngleB);
        angleArc.AngRadiansEnd = angleArc.AngRadiansStart + smallestAngle;

        float smallestMagnitude = Mathf.Min(a.magnitude, b.magnitude);
        angleArc.Radius = Mathf.Clamp(smallestMagnitude/3,  0.5f, 2f);
    }

    
    private float SignedAngleBetween(Vector3 vectorA, Vector3 vectorB)
    {
        float unsignedAngle = Mathf.Acos(Vector3.Dot(vectorA.normalized, vectorB.normalized));
        float sign = Mathf.Sign(Vector3.Dot(Vector3.Cross(vectorA, vectorB), Vector3.forward));
        return unsignedAngle * sign;
    }

    private void ShowNumbers()
    {
        float dot = Vector3.Dot(a, b);
        dotOutputText.text = $"Dot Product: {dot}";
        angle = (Mathf.Acos(dot / (a.magnitude * b.magnitude)));
        angleOutputText.text = $"Angle: {angle:0.##} rad  or {Mathf.Rad2Deg * angle:0.##}\u00B0";
        magnitudeOutputText.text = $"Projected Vector Magnitude: {Vector3.Dot(a, b) / b.magnitude:0.##}";
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

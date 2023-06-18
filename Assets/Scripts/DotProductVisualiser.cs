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
        Vector3 planeNormal = Vector3.Cross(a, b).normalized;

        if (Vector3.Dot(a.normalized, b.normalized) > 0.999f) // If the vectors are nearly equal, pointing in the same direction
        {
            angleArc.AngRadiansStart = 0;
            angleArc.AngRadiansEnd = 0; // Start and end at the same angle - no arc is drawn
        }
        else if (planeNormal.magnitude < 0.001f) // If the vectors are nearly equal, but in opposite directions
        {
            Vector3 arcRotationVector = Quaternion.Euler(0, 0, -90) * a; // Rotate vector A by 90 degrees around Z-axis
            Quaternion arcRotation = Quaternion.FromToRotation(Vector3.up, arcRotationVector); // Construct rotation from up direction to the rotated vector

            angleArc.transform.position = Vector3.zero;
            angleArc.transform.rotation = arcRotation;

            angleArc.AngRadiansStart = 0;
            angleArc.AngRadiansEnd = Mathf.PI; // Draw a semicircle
        }
        else
        {
            Vector3 arbitraryTangent = Vector3.Cross(planeNormal, a).normalized;
            Quaternion rotation = Quaternion.LookRotation(planeNormal, arbitraryTangent);

            angleArc.transform.position = Vector3.zero;
            angleArc.transform.rotation = rotation;

            float signedAngle = SignedAngleBetween(a, b, planeNormal);
            angleArc.AngRadiansStart = 0;
            angleArc.AngRadiansEnd = signedAngle;
        }
    }


    
    private float SignedAngleBetween(Vector3 vectorA, Vector3 vectorB)
    {
        float unsignedAngle = Mathf.Acos(Vector3.Dot(vectorA.normalized, vectorB.normalized));
        float sign = Mathf.Sign(Vector3.Dot(Vector3.Cross(vectorA, vectorB), Vector3.forward));
        return unsignedAngle * sign;
    }
    
    private float SignedAngleBetween(Vector3 vectorA, Vector3 vectorB, Vector3 normal)
    {
        float unsignedAngle = Mathf.Acos(Vector3.Dot(vectorA.normalized, vectorB.normalized));
        float sign = Mathf.Sign(Vector3.Dot(Vector3.Cross(vectorA, vectorB), normal));
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

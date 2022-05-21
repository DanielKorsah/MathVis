using System.Collections;
using System.Collections.Generic;
using Shapes;
using UnityEngine;

[ExecuteAlways]
public class ArrowGraphics : ImmediateModeShapeDrawer
{
    public Line line;
    public float tipAngle = 60;
    public float tipSideLength = 0.5f;
    public Color headColour;

    public override void DrawShapes( Camera cam ){

        using( Draw.Command( cam ) )
        {
            print("Hello");
            MoveEnd();
            DrawHead();
        }

    }

    private void MoveEnd()
    {
        // sin(angle) = O/H
        // O = H * sin(angle)
        float sideCornerAngle = (180 - tipAngle) / 2;
        float angleRad = Mathf.Deg2Rad * sideCornerAngle;
        float triangleHeight = tipSideLength * Mathf.Sin(angleRad);
        
        //
        Vector3 tip = transform.position;
        Vector3 baseVector = (line.Start - tip).normalized * triangleHeight;
        Vector3 headBasePosition  = tip + baseVector;

        Vector3 smallPadding = -baseVector * 0.01f;
        line.End = headBasePosition + smallPadding;
    }

    private void DrawHead()
    {
        Vector3 tip = transform.position;
        // A->B = B-A
        Vector3 backwards = (line.Start - tip).normalized * tipSideLength;
        Vector3 backwardsPoint = tip + backwards;
        Vector3 left = tip + (Quaternion.AngleAxis(-tipAngle/2, transform.forward) * backwards);
        Vector3 right = tip + (Quaternion.AngleAxis(tipAngle/2, transform.forward) * backwards);
        //Debug.Log($"tip: {tip}\nleft: {left}\nright: {right}\nbackwards: {backwards}");
        Draw.Triangle(tip, left, right, headColour);
        
    }
}

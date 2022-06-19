using System.Collections;
using System.Collections.Generic;
using Shapes;
using UnityEngine;

[ExecuteAlways]
public class ArrowGraphics : ImmediateModeShapeDrawer
{
    public Line line;
    public float thickness = 0.1f;
    public float tipAngle = 60;
    public float tipSideLength = 0.5f;
    public Color arrowColour;
    public Transform headTransform;
    public Transform rootTransform;
    public bool roundBase = true;

    public override void DrawShapes( Camera cam ){

        using( Draw.Command( cam ) )
        {
            Draw.BlendMode = ShapesBlendMode.Opaque;
            LineParams();
            MoveStart();
            MoveEnd();
            DrawHead();
            if(roundBase)
                DrawRoot();
        }

    }
    
    private void LineParams()
    {
        line.Color = arrowColour;
        line.Thickness = thickness;
    }

    private void MoveStart()
    {
        line.Start = rootTransform.position;
    }

    private void MoveEnd()
    {
        // sin(angle) = O/H
        // O = H * sin(angle)
        float sideCornerAngle = (180 - tipAngle) / 2;
        float angleRad = Mathf.Deg2Rad * sideCornerAngle;
        float triangleHeight = tipSideLength * Mathf.Sin(angleRad);
        
        //
        Vector3 tip = headTransform.position;
        Vector3 baseVector = (rootTransform.position - tip).normalized * triangleHeight;
        Vector3 headBasePosition  = tip + baseVector;

        Vector3 smallPadding = -baseVector * 0.01f;
        line.End = headBasePosition + smallPadding;
    }

    private void DrawHead()
    {
        Vector3 tip = headTransform.position;
        // A->B = B-A
        Vector3 backwards = (rootTransform.position - tip).normalized * tipSideLength;
        Vector3 backwardsPoint = tip + backwards;
        Vector3 left = tip + (Quaternion.AngleAxis(-tipAngle/2, headTransform.forward) * backwards);
        Vector3 right = tip + (Quaternion.AngleAxis(tipAngle/2, headTransform.forward) * backwards);
        //Debug.Log($"tip: {tip}\nleft: {left}\nright: {right}\nbackwards: {backwards}");
        Draw.Triangle(tip, left, right, arrowColour);
    }

    private void DrawRoot()
    {
        Draw.Disc(rootTransform.position, thickness/2, arrowColour);
    }
}

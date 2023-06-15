using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using Shapes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
public class TiledBackgroundGenerator : ImmediateModeShapeDrawer
{

    public float lineWeight;
    public int size;
    
#if  !UNITY_WEBGL
    public override void DrawShapes( Camera cam ){


        using( Draw.Command( cam ) ){
            MakeBackground();
            MakeLines();
        }

    }
#endif
    public void Start()
    {
#if  UNITY_WEBGL
        WebglBackground();
        WebglLines();
#else
        DisableWebCompatibleObjects();
#endif
    }

    private void DisableWebCompatibleObjects()
    {
        throw new NotImplementedException();
    }


    private void WebglBackground()
    {
        GameObject background;
        Rectangle rect;
        
        if (transform.GetComponentInChildren<Rectangle>() == null)
        {
            background = new GameObject("Background Plane");
            background.transform.parent = gameObject.transform;
            rect = background.AddComponent<Rectangle>();
        }
        else
        {
            background = GetComponentInChildren<Rectangle>().GameObject();
            rect = background.GetComponent<Rectangle>();
        }

        background.transform.position = new Vector3(0, 0, gameObject.transform.position.z);
        rect.BlendMode = ShapesBlendMode.Opaque;
        rect.Color = Color.black;
        rect.Width = size;
        rect.Height = size;
    }

    private void WebglLines()
    {
        Line[] preExistingLines = transform.GetComponentsInChildren<Line>();
        if (preExistingLines.Length != 0)
        {
            return;
        }
        
        //Vertical
        for (int i = 0; i < (size/2)+1; i++)
        {
            Vector3 start = new Vector3(i, -size/2, 5);
            Vector3 end = new Vector3(i, size/2, 5);
            SpawnWebLine(start, end);
        }

        for (int i = -1; i > -(size/2)-1; i--)
        {
            Vector3 start = new Vector3(i, -size/2, 5);
            Vector3 end = new Vector3(i, size/2, 5);
            SpawnWebLine(start, end);
        }
        
        //Horizontal
        for (int i = 0; i < (size/2)+1; i++)
        {
            Vector3 start = new Vector3(-size/2, i, 5);
            Vector3 end = new Vector3(size/2, i, 5);
            SpawnWebLine(start, end);
        }

        for (int i = -1; i > -(size/2)-1; i--)
        {
            Vector3 start = new Vector3(-size/2, i, 5);
            Vector3 end = new Vector3(size/2, i, 5);
            SpawnWebLine(start, end);
        }
        
    }

    private void SpawnWebLine(Vector3 start, Vector3 end)
    {
        GameObject LineContainer = new GameObject("LineContainer");
        LineContainer.transform.parent = gameObject.transform;
        Line line = LineContainer.AddComponent<Line>();
        line.BlendMode = ShapesBlendMode.Opaque;
        line.Color = Color.grey;
        line.Start = start;
        line.End = end;
    }

    private void MakeBackground()
    {
        Rect rect = new Rect(-size/2.0f, -size/2.0f, size, size);
        Draw.Rectangle(new Vector3(0, 0, gameObject.transform.position.z), rect, Color.black);
    }

    private void MakeLines()
    {
        // set up static parameters. these are used for all following Draw.Line calls
        Draw.LineGeometry = LineGeometry.Flat2D;
        Draw.ThicknessSpace = ThicknessSpace.Meters;
        Draw.Thickness = lineWeight;
        
        // set static parameter to draw in the local space of this object
        Draw.Matrix = transform.localToWorldMatrix;

        //draw horizontal
        HorizontalLines();

        //draw horizontal
        VerticalLines();
    }

    private void VerticalLines()
    {
        for (int i = 0; i < (size/2)+1; i++)
        {
            Vector3 start = new Vector3(i, -size/2, 10);
            Vector3 end = new Vector3(i, size/2, 10);
            Draw.Line(start, end, Color.grey);
        }

        for (int i = -1; i > -(size/2)-1; i--)
        {
            Vector3 start = new Vector3(i, -size/2, 10);
            Vector3 end = new Vector3(i, size/2, 10);
            Draw.Line(start, end, Color.grey);
        }
    }

    private void HorizontalLines()
    {
        for (int i = 0; i < (size/2)+1; i++)
        {
            Vector3 start = new Vector3(-size/2, i, 10);
            Vector3 end = new Vector3(size/2, i, 10);
            Draw.Line(start, end, Color.grey);
        }

        for (int i = -1; i > -(size/2)-1; i--)
        {
            Vector3 start = new Vector3(-size/2, i, 10);
            Vector3 end = new Vector3(size/2, i, 10);
            Draw.Line(start, end, Color.grey);
        }
    }
}


using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using Shapes;
using UnityEngine;

[ExecuteAlways]
public class TiledBackgroundGenerator : ImmediateModeShapeDrawer
{

    public float lineWeight;
    public int size;
    
    public override void DrawShapes( Camera cam ){

        using( Draw.Command( cam ) ){
            MakeBackground();
            MakeLines();
        }

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


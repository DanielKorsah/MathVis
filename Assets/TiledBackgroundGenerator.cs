using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using Shapes;
using UnityEngine;

[ExecuteAlways]
public class TiledBackgroundGenerator : ImmediateModeShapeDrawer
{
    public override void DrawShapes( Camera cam ){

        using( Draw.Command( cam ) ){
            MakeBackground();
            MakeLines();
        }

    }

    private void MakeBackground()
    {
        Rect rect = new Rect(-1000, -1000, 2000, 2000);
        Draw.Rectangle(new Vector3(0, 0, 10), rect, Color.black);
    }

    private void MakeLines()
    {
        // set up static parameters. these are used for all following Draw.Line calls
        Draw.LineGeometry = LineGeometry.Flat2D;
        Draw.ThicknessSpace = ThicknessSpace.Meters;
        Draw.Thickness = 0.1f; // 0.1m wide

        // set static parameter to draw in the local space of this object
        Draw.Matrix = transform.localToWorldMatrix;

        //draw horizontal
        HorizontalLines();

        //draw horizontal
        VerticalLines();
    }

    private static void VerticalLines()
    {
        for (int i = 0; i < 1001; i++)
        {
            Vector3 start = new Vector3(i, -1000, 10);
            Vector3 end = new Vector3(i, 1000, 10);
            Draw.Line(start, end, Color.grey);
        }

        for (int i = -1; i > -1001; i--)
        {
            Vector3 start = new Vector3(i, -1000, 10);
            Vector3 end = new Vector3(i, 1000, 10);
            Draw.Line(start, end, Color.grey);
        }
    }

    private static void HorizontalLines()
    {
        for (int i = 0; i < 1001; i++)
        {
            Vector3 start = new Vector3(-1000, i, 10);
            Vector3 end = new Vector3(1000, i, 10);
            Draw.Line(start, end, Color.grey);
        }

        for (int i = -1; i > -1001; i--)
        {
            Vector3 start = new Vector3(-1000, i, 10);
            Vector3 end = new Vector3(1000, i, 10);
            Draw.Line(start, end, Color.grey);
        }
    }
}


using System.Linq;

public class Script_Instance_create_trolley_lines : GH_ScriptInstance
{
    public void RunScript(
        double CurrentAngleRadians,
        Line RotationAxis,
        Point3d MiddleLPoint,
        double LengthAlongArmToPusher,
        double TopDiff,
        double PusherLength,
        double DeviationParallelDistance,
        double DeviationPerpDistance,
        int Direction,
        Point3d FloorWallCornerPoint,
        double WheelHeight,
        ref object TrolleyPusher,
        ref object TrolleyVertical,
        ref object Test)
    {
        Point3d leftPtL = MiddleLPoint;
        int dir = Direction / Math.Abs(Direction);
        
        LineCurve centreToPusherStart = new LineCurve(leftPtL, leftPtL + Vector3d.YAxis * LengthAlongArmToPusher * dir);
        centreToPusherStart.Rotate(CurrentAngleRadians, RotationAxis.Direction, leftPtL);
        
        double lowY = WheelHeight;
        
        double pusherEndY = leftPtL.Y + TopDiff;
        Line horizontalLineForIntersection = new Line(Pt(leftPtL.X, pusherEndY), Pt(centreToPusherStart.PointAtEnd.X + PusherLength * dir, pusherEndY));
        Circle circleForIntersection = new Circle(Plane.WorldXY, centreToPusherStart.PointAtEnd, PusherLength);
        Point3d[] horizontalLineForIntersectionEndPts = (new [] { horizontalLineForIntersection.From, horizontalLineForIntersection.To }).OrderBy(p => p.X).ToArray();
        
        Point3d intPt1, intPt2;
        double t1, t2;
        Rhino.Geometry.Intersect.Intersection.LineCircle(horizontalLineForIntersection, circleForIntersection, out t1, out intPt1, out t2, out intPt2);
        Point3d pusherVerticalPivotPoint;
        
        if (intPt1 != null && intPt2 == null) pusherVerticalPivotPoint = intPt1;
        else if (intPt1 == null && intPt2 != null) pusherVerticalPivotPoint = intPt2;
        else
        {
          var eligible = (new [] { intPt1, intPt2 })
            .Where(p => p.X >= horizontalLineForIntersectionEndPts[0].X && p.X <= horizontalLineForIntersectionEndPts[1].X).ToList();
        
          pusherVerticalPivotPoint = (dir == 1) ? eligible.First() : eligible.Last();
        }
        
        Line pusherLine = new Line(centreToPusherStart.PointAtEnd, pusherVerticalPivotPoint);
        Vector3d vPusher = new Vector3d(pusherLine.Direction);
        vPusher.Unitize();
        Vector3d vPusherPerp = new Vector3d(-vPusher.Y, vPusher.X, 0);
        
        Point3d[] pusherPts = new Point3d[] {
          centreToPusherStart.PointAtEnd,
          centreToPusherStart.PointAtEnd + vPusher * DeviationParallelDistance + vPusherPerp * DeviationPerpDistance,
          pusherVerticalPivotPoint
          };
        
        TrolleyPusher = new PolylineCurve(pusherPts);
        TrolleyVertical = new LineCurve(pusherVerticalPivotPoint, Pt(pusherVerticalPivotPoint.X, lowY));
        
        Test = new List<object>() { horizontalLineForIntersection, circleForIntersection };
    }

private Point3d Pt(double x, double y)
  {
    return new Point3d(x, y, 0);
  }
}
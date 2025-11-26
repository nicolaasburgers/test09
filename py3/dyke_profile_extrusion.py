# @GHInput: BaseWidth (float) 
# @GHInput: CrestWidth (float) 
# @GHInput: Height (float) 
# @GHInput: SlopeUp (float) 
# @GHInput: SlopeDown (float) 
# @GHInput: TrenchDepth (float) 
# @GHInput: TrenchWidth (float) 
# @GHInput: GroundOffset (float) 
# @GHInput: Flip (bool) 
# @GHInput: ExtrudeCrv (Curve) 
# @GHOutput: Profile (Curve) 
# @GHOutput: Pts (Point3d) 
# @GHOutput: Extrusion (Brep) 

import Rhino
from Rhino.Geometry import Point3d, Polyline, Curve, Brep, SweepOneRail

# Handle None inputs and assign sensible defaults
if BaseWidth is None or BaseWidth <= 0:
    BaseWidth = 20.0
if CrestWidth is None or CrestWidth <= 0:
    CrestWidth = 4.0
if Height is None or Height <= 0:
    Height = 5.0
if SlopeUp is None or SlopeUp <= 0:
    SlopeUp = 2.0
if SlopeDown is None or SlopeDown <= 0:
    SlopeDown = 2.0
if TrenchDepth is None or TrenchDepth < 0:
    TrenchDepth = 0.0
if TrenchWidth is None or TrenchWidth <= 0:
    TrenchWidth = 3.0
if GroundOffset is None or GroundOffset < 0:
    GroundOffset = 5.0
if Flip is None:
    Flip = False

# Compute minimal dyke footprint based on slopes and height
min_half_base_left = SlopeUp * Height
min_half_base_right = SlopeDown * Height
min_base_width = min_half_base_left + CrestWidth + min_half_base_right

if BaseWidth < min_base_width:
    BaseWidth = min_base_width

# Recalculate half widths from the final base width, preserving slope ratios
remaining = BaseWidth - CrestWidth
ratio = (SlopeUp + SlopeDown)
if ratio <= 0:
    half_left = remaining * 0.5
    half_right = remaining * 0.5
else:
    half_left = remaining * (SlopeUp / ratio)
    half_right = remaining * (SlopeDown / ratio)

# Layout along X
x0 = 0.0
x_left_ground_end = x0 + GroundOffset
x_left_foot = x_left_ground_end

# Optional trench in front (left side)
pts = []

y = 0.0
z_ground = 0.0

if TrenchDepth > 0.0:
    x_left_ground_end = x0 + GroundOffset
    x_trench_left = x_left_ground_end
    x_trench_bottom_left = x_trench_left + (GroundOffset * 0.5)
    x_trench_bottom_right = x_trench_bottom_left + TrenchWidth
    x_trench_right = x_trench_bottom_right + (GroundOffset * 0.5)
    x_left_foot = x_trench_right

    pts.append(Point3d(x0, y, z_ground))
    pts.append(Point3d(x_trench_left, y, z_ground))
    pts.append(Point3d(x_trench_bottom_left, y, -TrenchDepth))
    pts.append(Point3d(x_trench_bottom_right, y, -TrenchDepth))
    pts.append(Point3d(x_trench_right, y, z_ground))
else:
    pts.append(Point3d(x0, y, z_ground))
    pts.append(Point3d(x_left_ground_end, y, z_ground))
    x_left_foot = x_left_ground_end

# Dyke geometry
x_crest_left = x_left_foot + half_left
x_crest_right = x_crest_left + CrestWidth
x_right_foot = x_crest_right + half_right

pts.append(Point3d(x_left_foot, y, z_ground))
pts.append(Point3d(x_crest_left, y, Height))
pts.append(Point3d(x_crest_right, y, Height))
pts.append(Point3d(x_right_foot, y, z_ground))

x_right_ground_end = x_right_foot + GroundOffset
pts.append(Point3d(x_right_ground_end, y, z_ground))

# Optionally flip horizontally around center
if Flip:
    xs = [p.X for p in pts]
    cx = 0.5 * (min(xs) + max(xs))
    flipped = []
    for p in pts:
        dx = p.X - cx
        flipped.append(Point3d(cx - dx, p.Y, p.Z))
    pts = flipped

# Build polyline and curve
pl = Polyline(pts)
Profile = pl.ToNurbsCurve()
Pts = pts

# Extrude/sweep along a path curve if provided
Extrusion = None
if ExtrudeCrv is not None:
    if isinstance(ExtrudeCrv, Curve) and ExtrudeCrv.IsValid and Profile is not None and Profile.IsValid:
        sweep = SweepOneRail()
        sweep.AngleToleranceRadians = Rhino.RhinoDoc.ActiveDoc.ModelAngleToleranceRadians if Rhino.RhinoDoc.ActiveDoc else 0.001
        sweep.ClosedSweep = False
        breps = sweep.PerformSweep(ExtrudeCrv, Profile)
        if breps and len(breps) > 0:
            Extrusion = breps[0]
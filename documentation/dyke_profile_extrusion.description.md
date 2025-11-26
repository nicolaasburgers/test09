Creates a 2D dyke profile curve in the XZ-plane (Y=0) from geometric parameters and optionally extrudes it along a given path.

The profile is a polyline from left to right, representing ground, optional trench, dyke base, and crest.

**Inputs:**
- BaseWidth (float, item): Overall width of the dyke base. Default 20.0.
- CrestWidth (float, item): Flat width at the top of the dyke. Default 4.0.
- Height (float, item): Height of the dyke crest above ground. Default 5.0.
- SlopeUp (float, item): Horizontal run per 1.0 vertical rise on the upstream (left) slope. Default 2.0.
- SlopeDown (float, item): Horizontal run per 1.0 vertical drop on the downstream (right) slope. Default 2.0.
- TrenchDepth (float, item): Optional trench depth below ground in front of dyke (left side). 0.0 disables trench. Default 0.0.
- TrenchWidth (float, item): Width of trench at the bottom. Only used if TrenchDepth > 0. Default 3.0.
- GroundOffset (float, item): Extra flat ground on both sides of the dyke. Default 5.0.
- Flip (bool, item): If true, flips the dyke horizontally. Default False.
- ExtrudeCrv (Curve, item): Optional curve to extrude the dyke profile along. If None, no extrusion is created.

**Outputs:**
- Profile (Curve): The resulting dyke profile as a polyline curve.
- Pts (Point3d): The ordered points used to create the profile.
- Extrusion (Brep): The resulting brep from sweeping the profile along ExtrudeCrv, or None if no valid extrusion could be made.
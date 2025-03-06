# @GHInput: BaseCrv (Curve) [Access=Item]
# @GHInput: Height (float) [Access=Item]
# @GHInput: Comments (str) [Access=List]
# @GHOutput: CappedExtrusion (Brep) 
import Rhino.Geometry as rg

if BaseCrv is None:
    CappedExtrusion = None
else:
    CappedExtrusion = rg.Extrusion.Create(BaseCrv, Height, True)

if Comments is not None and len(Comments) > 0:
    print(", ".join(Comments))
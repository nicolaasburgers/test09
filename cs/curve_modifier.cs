// @GHInput: Crv (Rhino.Geometry.Curve) [Access=Item]
// @GHInput: Scale (double) [Access=Item]
// @GHOutput: Geo

using System.Linq;
using System.Drawing;

public class Script_Instance : GH_ScriptInstance
{
    private void RunScript(Curve Crv, double Scale, ref object Geo)
    {
        if (Scale == 0) Geo = null;
        else Geo = Crv.Scale(Scale);
    }
}


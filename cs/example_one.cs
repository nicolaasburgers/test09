// @GHInput: u (System.Object) [Access=Tree; Default=Branch{0}:[10,20]|Branch{1}:[30]]
// @GHInput: v (System.Object) [Access=List; Default=[6.6,7.1]]
// @GHInput: w (System.Object) [Access=Item; Default=Test string]
// @GHOutput: A
// @GHOutput: B

public class Script_Instance : GH_ScriptInstance
{
    private void RunScript(
        DataTree<int> u,
        List<double> v,
        string w,
        ref object out,
        ref object A,
        ref object B
    )
    {
            // Some comment here
            A = (u == null) ? "empty tree" : "Tree with " + string.Join(", ", u.AllData());
            B = (v == null) ? "empty list" : "List with " + string.Join(", ", v.ToArray());
            Print(string.IsNullOrEmpty(w) ? "no string" : w);
    }
}

# @GHInput: x (float) [Access=Item]
# @GHInput: y (float) [Access=Item]
# @GHInput: z (float) [Access=Item]
# @GHOutput: v (object) 

import Rhino.Geometry as rg

# Get the input values, or 0 if no input is connected
x = x or 0.0
y = y or 0.0
z = z or 0.0

# Create a Vector3d
v = rg.Vector3d(x, y, z)
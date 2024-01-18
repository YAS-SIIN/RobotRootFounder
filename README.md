NFI coding assignment
This assignment asks you to implement a validation program that checks whether a schedule is valid. A
schedule contains the following data:
1. A set of 1 or more robots. Each robot has an origin point, which is an (x, y) coordinate.
2. A set of 1 or more time steps. In each time step, each of the robots will visit a specific (x, y) position.
For example, a schedule with 3 robots and 2 time steps could look as follows:
# The number of robots
3
# The number of time steps
2
# The robots and their (x, y) origins. Format:
# [robot ID], [origin X], [origin Y]
A, 0, 0
B, 10, 0
C, 0, 10
# The assigned position in each time step. Format:
# [time step #], [robot ID], [visited position X], [visited position Y]
0, A, 1, 2
1, A, 1, 1
0, B, 4, 6
1, B, 4, 7
0, C, 1, 6
1, C, 6, 6
A schedule is valid if there are no collision between any of the robots for all time steps. A collision is defined
as follows:
Each robot occupies a rectangular area. The origin of the robot and the point that is being visited by
the robot are the two corners of this rectangle.
When the occupied areas of two robots overlap or touch in an edge or point, then there is a collision.
See (Tip 1) for a formula that you can use to determine whether two rectangles overlap or touch.In the sample schedule that is listed above, during time step 1, none of occupied area

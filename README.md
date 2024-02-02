## A set of Grasshopper Plugins made to improve workflows in university and personal projects:
### Recursive:
#### NextClosestPoint
a recursive algorithm for finding the next n closest points to a cloud of 'obstacles' points which updates with each recursive call. Points are found from a cloud of 'candidates' points. Proximity is judged as the mass multiplication of the distances from each cndidate to every obstacle.
#### NextFurthestPoint
a recusive algorithm for finding the next n furthest points to a cloud of 'obstacles' points which updates with each recursive call....
## Procedural:
#### VariableCircleRelaxation
a plugin aimed at relaxing a set of points with a set of given radii so that there is no collision between the circles formed with the given radii. Allthough this is very possible in grasshopper using other plugins I found it very useful to condense it into one component as it was something I wanted to use often.
Data-Structures/Optimisation:
#### ClosestPointsWithinRange (R-Tree)
an algorithm that finds clusters of points around focus points, given a set of candidate points and a distance. While this is very possible with vanilla grasshopper, it is brute forced with components such as ClosestPoint so I made an algorithm to use the R-Tree data structure to search for neighbouring points.
    

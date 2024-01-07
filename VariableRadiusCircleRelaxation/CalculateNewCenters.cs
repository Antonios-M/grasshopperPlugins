using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;


namespace MainFunc
{
  class Cloud
  {
    public List<Point3d> nodes;
    public List<double> radii;

    public Cloud(List<Point3d> points, List<double> r)
    {
       nodes = points;
       radii = r;
    }
    public void Update()
    {
      int range = nodes.Count;
      List<Vector3d> forces = new List<Vector3d>();
      List<double> collisions = new List<double>();

      for (int n = 0; n < range; n++)
      {
        forces.Add(new Vector3d(0.0, 0.0, 0.0));
        collisions.Add(0.0);
      }

      for (int i = 0; i < range; i++)
      {
        for (int j = i + 1; j < range; j++)
        {
          double radiusSum = radii[i] + radii[j];
          double gap = nodes[i].DistanceTo(nodes[j]);

          if (gap > radiusSum * 0.9) continue;
          double difference = (radiusSum - gap);
          Vector3d vec = nodes[i] - nodes[j];
          vec.Unitize();

          Vector3d move = 0.1 * vec * difference;
          forces[i] += move;
          forces[j] -= move;
          collisions[i] += 1.0;
          collisions[j] += 1.0;
        }
      }

      for (int i = 0; i < range; i++)
      {
        if ((collisions[i] > 0.0) && (forces[i].Length > 0.0))
        {
          nodes[i] += (forces[i] / collisions[i]);
        }
      }
    }

  }
}
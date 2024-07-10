#nullable enable
using GShark.Core;
using GShark.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GShark.Geometry
{
    /// <summary>
    /// This class represents a NURBS curve.
    /// </summary>
    /// <example>
    /// [!code-csharp[Example](../../src/GShark.Test.XUnit/Data/NurbsBaseCollection.cs?name=example)]
    /// </example>
    public class NurbsCurve : NurbsBase, ITransformable<NurbsCurve>
    {
        /// <summary>
        /// Public constructor for a NURBS curve based on existing values for the degree, knot vector, and control points
        /// </summary>
        /// The usual constraints of NURBS data apply:
        /// * control point weight is none negative
        /// * the length of the control points and knot vector must match: knots.Length = controlPoints.Length + degree + 1  
        /// <param name="degree">The curve degree.</param>
        /// <param name="knots">A KnotVector containing none negative values in the [0,1] range. Unify the knot vector if needed </param>
        /// <param name="controlPoints">A list of control points, containing the X,Y,Z coordinate + the control points weight</param>
        public NurbsCurve(int degree, KnotVector knots, List<Point4> controlPoints) 
            : base(degree, knots, controlPoints)
        {
        }

        /// <summary>
        /// Creates a NURBS curve.
        /// </summary>
        /// <param name="points">The points of the curve.</param>
        /// <param name="degree">The curve degree.</param>
        public NurbsCurve(List<Point3>? points, int degree)
            : this(degree, new KnotVector(degree, points!.Count), points.Select(p => new Point4(p)).ToList())
        {
        }

        /// <summary>
        /// Creates a NURBS curve.
        /// </summary>
        /// <param name="points">The points of the curve.</param>
        /// <param name="weights">The weights of each point.</param>
        /// <param name="degree">The curve degree.</param>
        public NurbsCurve(List<Point3>? points, List<double> weights, int degree)
            : this(degree, new KnotVector(degree, points!.Count), points.Select((p, i) => new Point4(p, weights[i])).ToList())
        {
        }

        /// <summary>
        /// Transforms a curve with the given transformation matrix.
        /// </summary>
        /// <param name="transformation">The transformation matrix.</param>
        /// <returns>A new NURBS curve transformed.</returns>
        public NurbsCurve Transform(TransformMatrix transformation)
        {
            List<Point4> pts = ControlPoints.Select(pt => pt.Transform(transformation)).ToList();
            return new NurbsCurve(Degree, Knots, pts);
        }
    }
}

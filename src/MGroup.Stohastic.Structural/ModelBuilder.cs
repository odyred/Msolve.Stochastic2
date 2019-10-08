using MGroup.FEM.Elements;
using MGroup.FEM.Entities;
using MGroup.MSolve.Discretization.FreedomDegrees;
using MGroup.MSolve.Discretization.Loads;
using MGroup.Stochastic.Interfaces;
using MGroup.Stochastic.Structural.StochasticRealizers;

namespace MGroup.Stochastic.Structural
{
	/// <summary>A class building a model with uncertain parameters.</summary>
    public class ModelBuilder
    {
	    /// <summary>A method that specifies an uncertain parameter realizer, a stochastic domain mapper and the current iteration
	    /// and returns the current model instantiation.</summary>
        public Model GetModel(RandomVariable randomVariable, IStochasticDomainMapper domainMapper, int iteration)
        {
            var m = new Model();
            m.NodesDictionary.Add(0, new Node(id: 0, x: 0, y: 0, z: 0));
            m.NodesDictionary.Add(1, new Node(id: 1, x: 1, y: 0, z: 0));
            m.ElementsDictionary.Add(1, new Element()
            {
                ID = 1,
                ElementType = new EulerBeam3D(randomVariable.Realize(iteration, domainMapper, null), 0.3)
            });
            m.Loads.Add(new Load() { Amount = 10, DOF = StructuralDof.TranslationX, Node = m.NodesDictionary[1] });

            return m;
        }
    }
}

﻿using Logic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic.Simulation
{
    public interface IBoolSimulationRenderer
    {
        IRenderer Renderer { get; set; }
        IStyle NullStateStyle { get; set; }
        IStyle TrueStateStyle { get; set; }
        IStyle FalseStateStyle { get; set; }
        IList<IShape> Shapes { get; set; }
        IDictionary<XBlock, BoolSimulation> Simulations { get; set; }
        void Render(object dc);
    }
}

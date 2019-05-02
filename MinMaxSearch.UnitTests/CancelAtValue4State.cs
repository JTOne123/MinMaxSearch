﻿using System.Collections.Generic;
using System.Threading;

namespace MinMaxSearch.UnitTests
{
    class CancelAtValue4State : IState
    {
        public readonly int Value;
        private readonly CancellationTokenSource cancellationTokenSource;

        public CancelAtValue4State(int value, CancellationTokenSource cancellationTokenSource)
        {
            Value = value;
            this.cancellationTokenSource = cancellationTokenSource;
        }

        public IEnumerable<IState> GetNeighbors()
        {
            if (Value >= 4)
                cancellationTokenSource.Cancel();

            return new List<IState> {new CancelAtValue4State(Value + 1, cancellationTokenSource)};
        }

        public double Evaluate(int depth, List<IState> passedThroughStates) => Value;
        
        public override string ToString() => Value.ToString();
    }
}
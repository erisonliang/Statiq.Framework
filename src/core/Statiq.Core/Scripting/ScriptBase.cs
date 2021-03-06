﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Statiq.Common;

namespace Statiq.Core
{
    public abstract class ScriptBase
    {
        protected ScriptBase(IMetadata metadata, IExecutionState executionState, IExecutionContext executionContext)
        {
            Metadata = metadata;
            ExecutionState = executionState;
            Context = executionContext;
        }

        public IExecutionState ExecutionState { get; }

        public IMetadata Metadata { get; }

        public IExecutionContext Context { get; }

        public abstract Task<object> EvaluateAsync();

        // Manually implement IExecutionContext pass-throughs since we don't
        // want to automatically proxy everything in IExecutionContext

        public string PipelineName => Context?.PipelineName;

        public IReadOnlyPipeline Pipeline => Context?.Pipeline;

        public Phase Phase => Context?.Phase ?? default;

        public IExecutionContext Parent => Context?.Parent;

        public IModule Module => Context?.Module;

        public ImmutableArray<IDocument> Inputs => Context?.Inputs ?? default;
    }
}
﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Statiq.Common;

namespace Statiq.Common
{
    public interface IScriptHelper
    {
        /// <summary>
        /// Compiles and evaluates a script.
        /// </summary>
        /// <param name="code">The code to compile.</param>
        /// <param name="metadata">
        /// The metadata used to construct the script. Metadata items are exposed a properties with
        /// the name of the key and can be used directly in the script.
        /// </param>
        /// <returns>Raw assembly bytes.</returns>
        Task<object> EvaluateAsync(string code, IMetadata metadata);

        /// <summary>
        /// Evaluates a script stored as raw assembly bytes.
        /// </summary>
        /// <remarks>
        /// This loads the assembly, finds the first script type, instantiates it, and evaluates it.
        /// </remarks>
        /// <param name="rawAssembly">The raw assembly bytes.</param>
        /// <param name="metadata">The metadata that should be used for evaluation.</param>
        /// <returns>The result of the script.</returns>
        Task<object> EvaluateAsync(byte[] rawAssembly, IMetadata metadata);

        /// <summary>
        /// Evaluates a script stored as raw assembly bytes.
        /// </summary>
        /// <remarks>
        /// This instantiates the script and evaluates it.
        /// </remarks>
        /// <param name="scriptType">The script type.</param>
        /// <param name="metadata">The metadata that should be used for evaluation.</param>
        /// <returns>The result of the script.</returns>
        Task<object> EvaluateAsync(Type scriptType, IMetadata metadata);

        /// <summary>
        /// Loads a script from a raw script assembly.
        /// </summary>
        /// <remarks>
        /// This loads the assembly and finds the first script type.
        /// </remarks>
        /// <param name="rawAssembly">The raw assembly bytes.</param>
        /// <returns>The script type or <c>null</c> if a script was not found in the assembly.</returns>
        Type Load(byte[] rawAssembly);

        /// <summary>
        /// Compiles a script into an in-memory script assembly for later evaluation.
        /// </summary>
        /// <param name="code">The code to compile.</param>
        /// <param name="metadata">
        /// The metadata used to construct the script. Metadata items are exposed a properties with
        /// the name of the key and can be used directly in the script.
        /// </param>
        /// <returns>Raw assembly bytes.</returns>
        byte[] Compile(string code, IMetadata metadata);

        /// <summary>
        /// Compiles a script into an in-memory script assembly for later evaluation.
        /// </summary>
        /// <param name="code">The code to compile.</param>
        /// <param name="metadataPropertyKeys">
        /// Metadata property keys that will be exposed as properties in the script as
        /// the name of the key and can be used directly in the script.
        /// </param>
        /// <returns>Raw assembly bytes.</returns>
        byte[] Compile(string code, IEnumerable<string> metadataPropertyKeys);
    }
}

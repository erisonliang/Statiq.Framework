﻿using System;
using Statiq.Common;

namespace Statiq.Razor
{
    internal struct CompilationParameters
    {
        public IReadOnlyFileSystem FileSystem { get; set; }
        public NamespaceCollection Namespaces { get; set; }
        public Type BasePageType { get; set; }
    }
}
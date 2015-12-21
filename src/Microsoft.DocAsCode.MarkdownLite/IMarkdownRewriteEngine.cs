﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.DocAsCode.MarkdownLite
{
    using System.Collections.Immutable;

    public interface IMarkdownRewriteEngine
    {
        IMarkdownEngine Engine { get; }
        ImmutableArray<IMarkdownToken> Rewrite(ImmutableArray<IMarkdownToken> tokens);
    }
}
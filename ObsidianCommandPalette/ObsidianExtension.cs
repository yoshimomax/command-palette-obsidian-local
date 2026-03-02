// Copyright (c) Obsidian Command Palette Extension
// SPDX-License-Identifier: MIT

using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.CommandPalette.Extensions;

namespace ObsidianCommandPalette;

[ComVisible(true)]
[Guid("7E8F9A0B-1C2D-3E4F-5A6B-7C8D9E0F1A2B")]
[ComDefaultInterface(typeof(IExtension))]
public sealed class ObsidianExtension : IExtension, IDisposable
{
    private readonly ManualResetEvent _extensionDisposedEvent;
    private readonly ObsidianCommandsProvider _provider = new();

    public ObsidianExtension(ManualResetEvent extensionDisposedEvent)
    {
        _extensionDisposedEvent = extensionDisposedEvent;
    }

    public object? GetProvider(ProviderType providerType)
    {
        return providerType == ProviderType.Commands ? _provider : null;
    }

    public void Dispose()
    {
        _extensionDisposedEvent.Set();
    }
}

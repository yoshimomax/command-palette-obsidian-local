// Copyright (c) Obsidian Command Palette Extension
// SPDX-License-Identifier: MIT

using System;
using System.Threading;
using Microsoft.CommandPalette.Extensions;
using Shmuelie.WinRTServer;
using Shmuelie.WinRTServer.CsWinRT;

namespace ObsidianCommandPalette;

public class Program
{
    [MTAThread]
    public static void Main(string[] args)
    {
        if (args.Length > 0 && args[0] == "-RegisterProcessAsComServer")
        {
            var server = new global::Shmuelie.WinRTServer.ComServer();
            var extensionDisposedEvent = new ManualResetEvent(initialState: false);

            var extensionInstance = new ObsidianExtension(extensionDisposedEvent);
            server.RegisterClass(() => extensionInstance);
            server.Start();

            extensionDisposedEvent.WaitOne();
            server.Stop();
            server.UnsafeDispose();
        }
        else
        {
            Console.WriteLine("Not launched as Command Palette extension. Exiting.");
        }
    }
}

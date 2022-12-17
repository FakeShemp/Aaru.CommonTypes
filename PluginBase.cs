// /***************************************************************************
// Aaru Data Preservation Suite
// ----------------------------------------------------------------------------
//
// Filename       : PluginBase.cs
// Author(s)      : Natalia Portillo <claunia@claunia.com>
//
// Component      : Core algorithms.
//
// --[ Description ] ----------------------------------------------------------
//
//     Class to hold all installed plugins.
//
// --[ License ] --------------------------------------------------------------
//
//     Permission is hereby granted, free of charge, to any person obtaining a
//     copy of this software and associated documentation files (the
//     "Software"), to deal in the Software without restriction, including
//     without limitation the rights to use, copy, modify, merge, publish,
//     distribute, sublicense, and/or sell copies of the Software, and to
//     permit persons to whom the Software is furnished to do so, subject to
//     the following conditions:
//
//     The above copyright notice and this permission notice shall be included
//     in all copies or substantial portions of the Software.
//
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
//     OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//     MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//     IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
//     CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
//     TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
//     SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// ----------------------------------------------------------------------------
// Copyright © 2011-2023 Natalia Portillo
// ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Aaru.CommonTypes.Interfaces;

namespace Aaru.CommonTypes;

/// <summary>Contain all plugins (filesystem, partition and image)</summary>
public class PluginBase
{
    /// <summary>List of all archive formats</summary>
    public readonly SortedDictionary<string, IArchive> Archives;
    /// <summary>List of byte addressable image plugins</summary>
    public readonly SortedDictionary<string, IByteAddressableImage> ByteAddressableImages;
    /// <summary>List of all filesystem plugins</summary>
    public readonly SortedDictionary<string, Type> Filesystems;
    /// <summary>List of filter plugins</summary>
    public readonly SortedDictionary<string, IFilter> Filters;
    /// <summary>List of floppy image plugins</summary>
    public readonly SortedDictionary<string, IFloppyImage> FloppyImages;
    /// <summary>List of all media image plugins</summary>
    public readonly SortedDictionary<string, Type> MediaImages;
    /// <summary>List of all partition plugins</summary>
    public readonly SortedDictionary<string, Type> Partitions;
    /// <summary>List of read-only filesystem plugins</summary>
    public readonly SortedDictionary<string, Type> ReadOnlyFilesystems;
    /// <summary>List of writable floppy image plugins</summary>
    public readonly SortedDictionary<string, IWritableFloppyImage> WritableFloppyImages;
    /// <summary>List of writable media image plugins</summary>
    public readonly SortedDictionary<string, Type> WritableImages;

    /// <summary>Initializes the plugins lists</summary>
    public PluginBase()
    {
        Filesystems           = new SortedDictionary<string, Type>();
        ReadOnlyFilesystems   = new SortedDictionary<string, Type>();
        Partitions            = new SortedDictionary<string, Type>();
        MediaImages           = new SortedDictionary<string, Type>();
        WritableImages        = new SortedDictionary<string, Type>();
        Filters               = new SortedDictionary<string, IFilter>();
        FloppyImages          = new SortedDictionary<string, IFloppyImage>();
        WritableFloppyImages  = new SortedDictionary<string, IWritableFloppyImage>();
        Archives              = new SortedDictionary<string, IArchive>();
        ByteAddressableImages = new SortedDictionary<string, IByteAddressableImage>();
    }

    /// <summary>Adds plugins to the central plugin register</summary>
    /// <param name="pluginRegister">Plugin register</param>
    public void AddPlugins(IPluginRegister pluginRegister)
    {
        foreach(Type type in pluginRegister.GetAllFilesystemPlugins() ?? Enumerable.Empty<Type>())
            if(Activator.CreateInstance(type) is IFilesystem plugin &&
               !Filesystems.ContainsKey(plugin.Name.ToLower()))
                Filesystems.Add(plugin.Name.ToLower(), type);

        foreach(Type type in pluginRegister.GetAllFilterPlugins() ?? Enumerable.Empty<Type>())
            if(Activator.CreateInstance(type) is IFilter plugin &&
               !Filters.ContainsKey(plugin.Name.ToLower()))
                Filters.Add(plugin.Name.ToLower(), plugin);

        foreach(Type type in pluginRegister.GetAllFloppyImagePlugins() ?? Enumerable.Empty<Type>())
            if(Activator.CreateInstance(type) is IFloppyImage plugin &&
               !FloppyImages.ContainsKey(plugin.Name.ToLower()))
                FloppyImages.Add(plugin.Name.ToLower(), plugin);

        foreach(Type type in pluginRegister.GetAllMediaImagePlugins() ?? Enumerable.Empty<Type>())
            if(Activator.CreateInstance(type) is IMediaImage plugin &&
               !MediaImages.ContainsKey(plugin.Name.ToLower()))
                MediaImages.Add(plugin.Name.ToLower(), type);

        foreach(Type type in pluginRegister.GetAllPartitionPlugins() ?? Enumerable.Empty<Type>())
            if(Activator.CreateInstance(type) is IPartition plugin &&
               !Partitions.ContainsKey(plugin.Name.ToLower()))
                Partitions.Add(plugin.Name.ToLower(), type);

        foreach(Type type in pluginRegister.GetAllReadOnlyFilesystemPlugins() ?? Enumerable.Empty<Type>())
            if(Activator.CreateInstance(type) is IReadOnlyFilesystem plugin &&
               !ReadOnlyFilesystems.ContainsKey(plugin.Name.ToLower()))
                ReadOnlyFilesystems.Add(plugin.Name.ToLower(), type);

        foreach(Type type in pluginRegister.GetAllWritableFloppyImagePlugins() ?? Enumerable.Empty<Type>())
            if(Activator.CreateInstance(type) is IWritableFloppyImage plugin &&
               !WritableFloppyImages.ContainsKey(plugin.Name.ToLower()))
                WritableFloppyImages.Add(plugin.Name.ToLower(), plugin);

        foreach(Type type in pluginRegister.GetAllWritableImagePlugins() ?? Enumerable.Empty<Type>())
            if(Activator.CreateInstance(type) is IBaseWritableImage plugin &&
               !WritableImages.ContainsKey(plugin.Name.ToLower()))
                WritableImages.Add(plugin.Name.ToLower(), type);

        foreach(Type type in pluginRegister.GetAllArchivePlugins() ?? Enumerable.Empty<Type>())
            if(Activator.CreateInstance(type) is IArchive plugin &&
               !Archives.ContainsKey(plugin.Name.ToLower()))
                Archives.Add(plugin.Name.ToLower(), plugin);

        foreach(Type type in pluginRegister.GetAllByteAddressablePlugins() ?? Enumerable.Empty<Type>())
            if(Activator.CreateInstance(type) is IByteAddressableImage plugin &&
               !ByteAddressableImages.ContainsKey(plugin.Name.ToLower()))
                ByteAddressableImages.Add(plugin.Name.ToLower(), plugin);
    }
}
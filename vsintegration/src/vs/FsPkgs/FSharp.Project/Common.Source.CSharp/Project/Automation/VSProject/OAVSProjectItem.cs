// Copyright (c) Microsoft Open Technologies, Inc.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextManager.Interop;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;
using IServiceProvider = System.IServiceProvider;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.FSharp.ProjectSystem;
using EnvDTE;
using VSLangProj;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.VisualStudio.FSharp.ProjectSystem.Automation
{
    /// <summary>
    /// Represents a language-specific project item
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "OAVS")]
    [ComVisible(true), CLSCompliant(false)]
    public class OAVSProjectItem : VSProjectItem
    {
        #region fields
        private FileNode fileNode;
        #endregion

        #region ctors
        internal OAVSProjectItem(FileNode fileNode)
        {
            this.FileNode = fileNode;
        }
        #endregion

        #region VSProjectItem Members

        public virtual Project ContainingProject
        {
            get { return fileNode.ProjectMgr.GetAutomationObject() as Project; }
        }

        public virtual ProjectItem ProjectItem
        {
            get { return fileNode.GetAutomationObject() as ProjectItem; }
        }

        public virtual DTE DTE
        {
            get { return (DTE)this.fileNode.ProjectMgr.Site.GetService(typeof(DTE)); }
        }

        public virtual void RunCustomTool()
        {
#if SINGLE_FILE_GENERATOR
            this.FileNode.RunGenerator();
#endif
        }

        #endregion

        #region public properties
        /// <summary>
        /// File Node property
        /// </summary>
        public FileNode FileNode
        {
            get
            {
                return fileNode;
            }
            set
            {
                fileNode = value;
            }
        }
        #endregion

    }
}

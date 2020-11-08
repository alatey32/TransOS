using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransOS.Plugin.Helper.FileSystem
{
    public enum FILEOP_FLAGS : int
    {
        /// <summary>
        /// Indicates that the to member specifies multiple destination files (one for each source file) rather than one directory where all source files are to be deposited.
        /// </summary>
        FOF_MULTIDESTFILES = 0x1,

        /// <summary>
        /// Does not display a progress dialog box.
        /// </summary>
        FOF_SILENT = 0x4,

        /// <summary>
        /// Gives the file being operated on a new name (such as "Copy #1 of...") in a move, copy, or rename operation if a file of the target name already exists.
        /// </summary>
        FOF_RENAMEONCOLLISION = 0x8,

        /// <summary>
        /// Responds with "yes to all" for any dialog box that is displayed.
        /// </summary>
        FOF_NOCONFIRMATION = 0x10,

        /// <summary>
        /// Preserves undo information, if possible. With del, uses recycle bin.
        /// </summary>
        FOF_ALLOWUNDO = 0x40,

        /// <summary>
        /// Performs the operation only on files if a wildcard filename (*.*) is specified.
        /// </summary>
        FOF_FILESONLY = 0x80,

        /// <summary>
        /// Displays a progress dialog box, but does not show the filenames.
        /// </summary>
        FOF_SIMPLEPROGRESS = 0x100,

        /// <summary>
        /// Does not confirm the creation of a new directory if the operation requires one to be created.
        /// </summary>
        FOF_NOCONFIRMMKDIR = 0x200,

        /// <summary>
        /// don't put up error UI
        /// </summary>
        FOF_NOERRORUI = 0x400,

        /// <summary>
        /// don't copy file security attributes
        /// </summary>
        FOF_NOCOPYSECURITYATTRIBS = 0x800,

        /// <summary>
        /// Only operate in the specified directory. Don't operate recursively into subdirectories.
        /// </summary>
        FOF_NORECURSION = 0x1000,

        /// <summary>
        /// Do not move connected files as a group (e.g. html file together with images). Only move the specified files.
        /// </summary>
        FOF_NO_CONNECTED_ELEMENTS = 0x2000,

        /// <summary>
        /// Send a warning if a file is being destroyed during a delete operation rather than recycled. This flag partially overrides FOF_NOCONFIRMATION.
        /// </summary>
        FOF_WANTNUKEWARNING = 0x4000,
    }
}

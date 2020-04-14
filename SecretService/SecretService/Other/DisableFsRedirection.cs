using System;
using System.Runtime.InteropServices;

namespace SGet.Other
{

    sealed class DisableFsRedirection : IDisposable
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

        [ThreadStatic] IntPtr _oldValue;
        [ThreadStatic] bool _revert;

        public DisableFsRedirection()
        {
            _oldValue = IntPtr.Zero;
            try
            {
                _revert = Wow64DisableWow64FsRedirection(ref _oldValue);
            }
            catch (EntryPointNotFoundException)
            {
                GC.SuppressFinalize(this);
            }
        }

        ~DisableFsRedirection()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        void Dispose(bool disposing)
        {
            if (disposing)
            {
                GC.SuppressFinalize(this);
            }

            if (_revert)
            {
                Wow64RevertWow64FsRedirection(_oldValue);
                _oldValue = IntPtr.Zero;
                _revert = false;
            }
        }
    }

}

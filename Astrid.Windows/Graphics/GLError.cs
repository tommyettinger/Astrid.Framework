﻿using System;
using OpenTK.Graphics.ES20;

namespace Astrid.Windows.Graphics
{
    public static class GLError
    {
        public static void ThrowOnError(Func<string> getErrorMessage)
        {
#if ANDROID
            var errorCode = GL.GetErrorCode();
#else
            var errorCode = GL.GetError();
#endif
            if (errorCode != ErrorCode.NoError)
            {
                var message = getErrorMessage();
                throw new InvalidOperationException(message);
            }
        }
    }
}

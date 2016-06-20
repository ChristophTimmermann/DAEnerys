using OpenTK;
using OpenTK.Graphics;

namespace DAEnerys
{
    class CustomGLControl : GLControl
    {
        // 32bpp color, 24bpp z-depth, 8bpp stencil and 4x antialiasing
        // OpenGL version is major=3, minor=0
        public CustomGLControl(int fsaaSamples)
            : base(new GraphicsMode(32, 24, 8, fsaaSamples), 3, 0, GraphicsContextFlags.ForwardCompatible)
        { }
    }
}
﻿using Astrid.Core;
using Astrid.Framework.Assets;
using Astrid.Framework.Graphics;
using Astrid.Windows.Graphics.GLPrograms;
using OpenTK.Graphics.OpenGL;

namespace Astrid.Windows.Graphics
{
    public class GLGraphicsDevice : GraphicsDevice
    {
        public GLGraphicsDevice()
        {
        }

        private GLSpriteBatchProgram _spriteBatchProgram;
        private GLPrimitiveBatchProgram _primitiveBatchProgram;
        private GLBatchProgram _program;

        public void Resize(int width, int height)
        {
            var matrix = Matrix.CreateOrthographicOffCenter(0, width, height, 0, 1, -1);
            GL.Viewport(0, 0, width, height);

            _spriteBatchProgram.Use();
            GL.UniformMatrix4(_spriteBatchProgram.MatrixLocation, 1, false, Matrix.ToFloatArray(matrix));

            _primitiveBatchProgram.Use();
            GL.UniformMatrix4(_primitiveBatchProgram.MatrixLocation, 1, false, Matrix.ToFloatArray(matrix));

            _program.Use();
        }
        
        public void Initialize()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            _spriteBatchProgram = new GLSpriteBatchProgram();
            _spriteBatchProgram.Build();

            _primitiveBatchProgram = new GLPrimitiveBatchProgram();
            _primitiveBatchProgram.Build();

            UseSpriteBatchProgram();
            SetViewMatrix(Matrix.Identity);
        }

        public override void EnableDepthMask()
        {
            GL.DepthMask(true);
        }

        public override void DisableDepthMask()
        {
            GL.DepthMask(false);
        }

        public override void BindTexture(Texture texture)
        {
            GL.BindTexture(TextureTarget.Texture2D, texture.Id);
        }

        public override void RenderBatch(float[] vertexData, int vertexCount)
        {
            _program.Render(vertexData, vertexCount);
        }

        public override void Clear(Color color)
        {
            GL.ClearColor(color.ToColor4());
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }

        public override void SetViewMatrix(Matrix viewMatrix)
        {
            GL.UniformMatrix4(_program.ViewMatrixLocation, 1, false, Matrix.ToFloatArray(viewMatrix));
        }

        public override void UsePrimitiveBatchProgram()
        {
            _program = _primitiveBatchProgram;
            _program.Use();
        }

        public override void UseSpriteBatchProgram()
        {
            _program = _spriteBatchProgram;
            _program.Use();
        }
    }
}
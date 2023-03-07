using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace Maneger
{
    internal class Shader
    {
        private int _VertexShader = 0;
        private int _FragmentShader = 0;

        private readonly int _Program = 0;
        public Shader(string VertexFile, string fragmentFile)
        {
            _VertexShader = CreateShader(ShaderType.VertexShader, VertexFile);
            _FragmentShader = CreateShader(ShaderType.FragmentShader, fragmentFile);

            _Program = GL.CreateProgram();
            GL.AttachShader(_Program, _VertexShader);
            GL.AttachShader(_Program, _FragmentShader);
            GL.LinkProgram(_Program);

            GL.GetProgram(_Program, GetProgramParameterName.LinkStatus, out var code);

            if (code != (int)All.True)
            {
                var infoLog = GL.GetProgramInfoLog(_Program);
                throw new Exception($"Ошибка компыляции шейдера {_Program}\n\n{infoLog}");
            }

            DeleteShader(_VertexShader);
            DeleteShader(_FragmentShader);
        }
        private int CreateShader(ShaderType shaderType, string ShaderCodePath)
        {
            string ShaderCode = File.ReadAllText(ShaderCodePath);
            int ShaderID = GL.CreateShader(shaderType);
            GL.ShaderSource(ShaderID, ShaderCode);
            GL.CompileShader(ShaderID);

            GL.GetShader(ShaderID, ShaderParameter.CompileStatus, out var code);
            if (code != (int)All.True)
            {
                var infoLog = GL.GetShaderInfoLog(ShaderID);
                throw new Exception($"Ошибка компыляции шейдера {ShaderID}\n\n{infoLog}");
            }
            return ShaderID;
        }
        public void ActiveProgram()
        {
            GL.UseProgram(_Program);
        }
        public void DiactiveProgram()
        {
            GL.UseProgram(0);
        }
        public void DeleteShaderProgram()
        {
            GL.DeleteProgram(_Program);
        }
        private void DeleteShader(int Shader)
        {
            GL.DetachShader(_Program, Shader);
            GL.DeleteShader(Shader);
        }
        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(_Program, attribName);
        }
    }
}

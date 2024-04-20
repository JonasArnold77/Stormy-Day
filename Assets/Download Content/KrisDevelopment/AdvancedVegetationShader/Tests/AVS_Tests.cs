#if UNITY_EDITOR && UNITY_INCLUDE_TESTS

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace KrisDevelopment.AVS.Tests
{
    internal class AVS_Tests
	{
        [Test]
        public void TestShaderErrors()
        {
			foreach (var _shaderInfo in ShaderUtil.GetAllShaderInfo())
			{
				if (!_shaderInfo.supported)
				{
					continue;
				}

				Assert.IsTrue(!_shaderInfo.hasErrors, $"Shader {_shaderInfo.name} has compilation errors.");
			}
			//Debug.Assert(ShaderUtil.ShaderHasError(_shaderAsset));
		}
    }
}

#endif
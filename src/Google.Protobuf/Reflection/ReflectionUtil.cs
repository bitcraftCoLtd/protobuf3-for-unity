#region Copyright notice and license
// Protocol Buffers - Google's data interchange format
// Copyright 2008 Google Inc.  All rights reserved.
// https://developers.google.com/protocol-buffers/
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are
// met:
//
//     * Redistributions of source code must retain the above copyright
// notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above
// copyright notice, this list of conditions and the following disclaimer
// in the documentation and/or other materials provided with the
// distribution.
//     * Neither the name of Google Inc. nor the names of its
// contributors may be used to endorse or promote products derived from
// this software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
// A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
// OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
// SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
// LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
// THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Google.Protobuf.Reflection
{
    /// <summary>
    /// In order to run on iOS (no JIT) we had to use Invoke which results in a bit
    /// of a performance cost. The original description is as follows:
    /// The methods in this class are somewhat evil, and should not be tampered with lightly.
    /// Basically they allow the creation of relatively weakly typed delegates from MethodInfos
    /// which are more strongly typed. They do this by creating an appropriate strongly typed
    /// delegate from the MethodInfo, and then calling that within an anonymous method.
    /// Mind-bending stuff (at least to your humble narrator) but the resulting delegates are
    /// very fast compared with calling Invoke later on.
    /// </summary>
    internal static class ReflectionUtil
    {
        /// <summary>
        /// Empty Type[] used when calling GetProperty to force property instead of indexer fetching.
        /// </summary>
        internal static readonly Type[] EmptyTypes = new Type[0];

        /// <summary>
        /// Creates a delegate which will cast the argument to the appropriate method target type,
        /// call the method on it, then convert the result to object.
        /// </summary>
        internal static Func<IMessage, object> CreateFuncIMessageObject(MethodInfo method)
        {
			return (IMessage message) =>
			{
				var returnValue = method.Invoke(message, null) as object;
				return returnValue;
			};
        }

        /// <summary>
        /// Creates a delegate which will cast the argument to the appropriate method target type,
        /// call the method on it, then convert the result to the specified type.
        /// </summary>
        internal static Func<IMessage, T> CreateFuncIMessageT<T>(MethodInfo method)
        {
			return (IMessage message) =>
			{
				var returnValue = (T)method.Invoke(message, null);
				return returnValue;
			};
        }

        /// <summary>
        /// Creates a delegate which will execute the given method after casting the first argument to
        /// the target type of the method, and the second argument to the first parameter type of the method.
        /// </summary>
        internal static Action<IMessage, object> CreateActionIMessageObject(MethodInfo method)
        {
			return (IMessage arg1, object arg2) =>
			{
				method.Invoke(arg1, new object[]{ arg2 });
			};
        }

        /// <summary>
        /// Creates a delegate which will execute the given method after casting the first argument to
        /// the target type of the method.
        /// </summary>
        internal static Action<IMessage> CreateActionIMessage(MethodInfo method)
        {
			return (IMessage obj) =>
			{
				method.Invoke(obj, null);
			};
        }        
    }
}
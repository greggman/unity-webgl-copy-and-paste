/*
 * Copyright 2020, Gregg Tavares.
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are
 * met:
 *
 *     * Redistributions of source code must retain the above copyright
 * notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above
 * copyright notice, this list of conditions and the following disclaimer
 * in the documentation and/or other materials provided with the
 * distribution.
 *     * Neither the name of Gregg Tavares. nor the names of its
 * contributors may be used to endorse or promote products derived from
 * this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 * "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 * LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
 * A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
 * OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
 * LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
mergeInto(LibraryManager.library, {
    initWebGLCopyAndPaste: function(objectNamePtr, copyFuncNamePtr, pasteFuncNamePtr) {
      window.becauseUnityIsBadWithJavascript_webglCopyAndPaste =
          window.becauseUnityIsBadWithJavascript_webglCopyAndPaste || {
         initialized: false,
         objectName: Pointer_stringify(objectNamePtr),
         copyFuncName: Pointer_stringify(copyFuncNamePtr),
         pasteFuncName: Pointer_stringify(pasteFuncNamePtr),
      };
      var g = window.becauseUnityIsBadWithJavascript_webglCopyAndPaste;

      if (!g.initialized) {
        window.addEventListener('copy', function(e) {
          e.preventDefault();
          var isApple = /(Mac|iPhone|iPod|iPad)/i.test(window.navigator.platform);
          var keyCode = isApple ? '%c' : '^c';
          SendMessage(g.objectName, g.copyFuncName, keyCode);
          event.clipboardData.setData('text/plain', g.clipboardStr);
        });
        window.addEventListener('paste', function(e) {
          var result = e.clipboardData.getData('text')
          SendMessage(g.objectName, g.pasteFuncName, result);
        });
      }
    },
    passCopyToBrowser: function(stringPtr) {
      var g = window.becauseUnityIsBadWithJavascript_webglCopyAndPaste;
      var str = Pointer_stringify(stringPtr);
      g.clipboardStr = str;
    },
});



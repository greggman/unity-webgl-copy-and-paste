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

// #define WEBGL_COPY_AND_PASTE_SUPPORT_TEXTMESH_PRO

using UnityEngine;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

[Preserve]
public class WebGLCopyAndPasteAPI
{

#if UNITY_WEBGL

    [DllImport("__Internal")]
    private static extern void initWebGLCopyAndPaste(StringCallback cutCopyCallback, StringCallback pasteCallback);
    [DllImport("__Internal")]
    private static extern void passCopyToBrowser(string str);

    delegate void StringCallback( string content );


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    private static void Init()
    {
        if ( !Application.isEditor )
        {
            initWebGLCopyAndPaste(GetClipboard, ReceivePaste );
        }
    }

    private static Event CreateKeyboardEventWithControlAndCommandKeysPressed(string baseKey)
    {
        var keyboardEvent = Event.KeyboardEvent(baseKey);
        keyboardEvent.control = true;
        keyboardEvent.command = true;
        return keyboardEvent;
    }

    private static void SendKey(string baseKey, bool forceLabelUpdate = false)
      {
        var currentEventSystem = EventSystem.current;
        if (currentEventSystem == null) {
            return;
        }
        var currentObj = currentEventSystem.currentSelectedGameObject;
        if (currentObj == null) {
          return;
        }
#if WEBGL_COPY_AND_PASTE_SUPPORT_TEXTMESH_PRO
        {
          var input = currentObj.GetComponent<TMPro.TMP_InputField>();
          if (input != null) {
            input.ProcessEvent(CreateKeyboardEventWithControlAndCommandKeysPressed(baseKey));
            if (forceLabelUpdate)
                input.ForceLabelUpdate();
            return;
          }
        }
#endif
        {
          var input = currentObj.GetComponent<UnityEngine.UI.InputField>();
          if (input != null) {
            input.ProcessEvent(CreateKeyboardEventWithControlAndCommandKeysPressed(baseKey));
            if (forceLabelUpdate)
                input.ForceLabelUpdate();
            return;
          }
        }
      }

      [AOT.MonoPInvokeCallback( typeof(StringCallback) )]
      private static void GetClipboard(string key)
      {
        SendKey(key);
        passCopyToBrowser(GUIUtility.systemCopyBuffer);
      }

      [AOT.MonoPInvokeCallback( typeof(StringCallback) )]
      private static void ReceivePaste(string str)
      {
        // Assigning the text to "GUIUtility.systemCopyBuffer" causes it to be automatically pasted on some browsers on the next frame,
        // but not on all (e.g. Firefox 120.0.1, Windows 10, Unity 2022.3.10).
        // Using "SendKey" with the "v" key properly pastes the text on all tested browsers (in the current frame),
        // but it needs "GUIUtility.systemCopyBuffer" to be set,
        // and doing so would paste the text twice on browsers in which setting "GUIUtility.systemCopyBuffer" works.
        // As a workaround, we set "GUIUtility.systemCopyBuffer", then call "SendKey", and then set "GUIUtility.systemCopyBuffer" to null;
        // this prevents the paste that occurs on the next frame, and only the "SendKey" one is made.
        // Confirmed to work on:
        //   - Edge 120.0.2210.61 (Chromium) on Windows 10, Unity 2022.3.10, 2021.3.25 and 2020.3.18.
        //   - Firefox 120.0.1 on Windows 10, Unity 2022.3.10, 2021.3.25 and 2020.3.18.
        //   - Safari 16.6 on macOS Ventura 13.6, Unity 2022.3.10.
        //   - Chrome 118.0.5993.70 on macOS Ventura 13.6, Unity 2022.3.10.
        //   - Firefox 120.0.1 on macOS Ventura 13.6, Unity 2022.3.10.
        GUIUtility.systemCopyBuffer = str;
        SendKey("v", true);
        GUIUtility.systemCopyBuffer = null;
      }

#endif

}

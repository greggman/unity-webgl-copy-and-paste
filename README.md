# Unity WebGL Copy and Paste

For some reason Unity decided not to support copy and paste in WebGL.
Here is an attempt to add it back in. No promises that it's perfect 😅

![screenshot](https://user-images.githubusercontent.com/234804/85100802-ff0bcf00-b23b-11ea-9030-24206085bd54.gif)

At the moment there is only support for [InputField](https://docs.unity3d.com/2019.1/Documentation/Manual/script-InputField.html) and
[`TMPro.TMP_InputField`](https://docs.unity3d.com/Packages/com.unity.textmeshpro@2.1/api/TMPro.TMP_InputField.html)

Example: https://greggman.github.io/unity-webgl-copy-and-paste/

## Instructions

1. Download and add in [this unity package](https://github.com/greggman/unity-webgl-copy-and-paste/releases/latest) into your project.

2. Make a `GameObject` and add in the `WebGLCopyAndPaste` script.

3. If you are using [`TMPro.TMP_InputField`](https://docs.unity3d.com/Packages/com.unity.textmeshpro@2.1/api/TMPro.TMP_InputField.html) then edit `Assets/WebGLCopyAndPaste/Scripts/WebGLCopyAndPaste.cs`
and uncomment this line

```
// #define WEBGL_COPY_AND_PASTE_SUPPORT_TEXTMESH_PRO
```
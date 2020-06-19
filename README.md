# Unity WebGL Copy and Paste

For some reason Unity decided not to support copy and paste in WebGL.
Here is an attempt to add it back in. No promises that it's perfect 😅

![screenshot](https://user-images.githubusercontent.com/234804/85100802-ff0bcf00-b23b-11ea-9030-24206085bd54.gif)

At the moment there is only support for [`InputField`](https://docs.unity3d.com/2019.1/Documentation/Manual/script-InputField.html) and
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

## Alternatives

This might be better?

https://github.com/kou-yeung/WebGLInput

## Issues

* Non Alphabetic characters

  See [this thread](https://forum.unity.com/threads/japanese-hiragana-characters-dont-work-in-webgl.356097/). 
  You apparently need to include the fonts in your Unity project.

* Ctrl-A/⌘-A selects other HTML on the page

  1. Make your own [WebGL template](https://docs.unity3d.com/Manual/webgl-templates.html) that doesn't have
     anything to select. Maybe [this one](https://github.com/greggman/better-unity-webgl-template) though I
     didn't try it.

  2. Make your own [WebGL template](https://docs.unity3d.com/Manual/webgl-templates.html) and
     use [`user-select: none;`](https://developer.mozilla.org/en-US/docs/Web/CSS/user-select) in your CSS
     to make whatever parts of the page you want to prevent from being selected.

## ChangeList

* 0.0.2

  * Support cut

  * Support Safari

* 0.0.1

  * Initial Release

## License: BSD-3-Clause

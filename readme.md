# MockDataDebugVisualizer #

A VS2010 debug visualizer plugin to create current state initilization code for an object.

## Usage ##
Build solution and put the .dll either in the VS2010 install path: 

_VisualStudioInstallPath_\Common7\Packages\Debugger\Visualizers (e.g. C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\Packages\Debugger\Visualizers) or in My Documents\ _VisualStudioVersion_\Visualizers. 

During debug use the "Watch" window to wrap an object in a WeakReference e.g. new System.WeakReference(myObject) and use the magnifying glass to generate the initilization code which will be put in the clipboard.
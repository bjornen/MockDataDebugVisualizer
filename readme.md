# MockDataDebugVisualizer #

A VS2010 debug visualizer plugin to create current state initilization code for an object.
This is still on a proof of concept level.

## Usage ##

During debug use "watch" to wrap an object in a WeakReference e.g. new System.WeakReference(myObject) and use the magnifying glass to generate initilization code which will be put in the clipboard.
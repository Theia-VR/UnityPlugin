# UnityPlugin 
[![Build Status](https://travis-ci.org/Valentin-Bourcier/UnityPlugin.svg?branch=master)](https://travis-ci.org/Valentin-Bourcier/UnityPlugin)

Projet Fin A1 TheiaVr_Unity

# Project Goal:
Given a C# kinectStudio streamer to UDP network, Student project for streaming Kinect flow to Unity3D

# Team : 
## Tutor: 
Cedric Dumas
## Students: 
Valentin Bourcier 
Alexis Delforges
Baptiste Vrignaud

# Required Material
## Hardware : 
Windows 10,
MicrosoftKinect,
UDP local network
## Software : 
Kinect SDK 2.0,
Kinect Studio 2.0,
TheaiVr KinectStreamer,
Unity 4.5.2,
Visual Studio 2017,
.NET Framework 3.5

## How to use

First, you need to launch KinectStreamer with the correct IP address of the remote host and port.
Then, you need to launch Kinect Studio and load your video OR wire up a kinect to your computer. Click on connect and on play to start the stream.
In the KinectStreamer, start to send the stream with UDP.

You can know launch Unity and add the .dll of this project into your assets folder. You will then see a new menu named Kinect Plugin. Click on it and on "Show plugin". Write the ip address of the computer using KinectStreamer, the correct ports and number of points wanted. Check what you want to receive and render.

Then, go on play mode in Unity to be able to start the Kinect Plugin with the start button.


# Development Environment : 
## Revision Control system : 
GitLab = https://gitlab.com/cedric.dumas/Theia_VR,
GitHub = https://github.com/Valentin-Bourcier/UnityPlugin 

## Test : 
FrameWork  = Nunit
## Continous Integration
Travis on the Github
## Agile Methods : 
TpOnDemand = timeScheduler + burndownChart

# Development cycle:
### Sprint 1 : 16/05/2018 to 31/05/2018
### Sprint 2 : 01/06/2018 to 16/06/2018

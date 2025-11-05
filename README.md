This repository contains a 2D platformer game built using Unity, featuring smooth movement controls, responsive touch inputs for Android, and keyboard controls for Windows testing and play.

--Project Structure
Folder / File	Description
Assets/	Contains all Unity project assets — scripts, scenes, prefabs, audio, and UI elements.
Android_Build/	Includes the Android APK build of the game for direct installation and testing on Android devices.
WinBuild/	Contains the Windows executable build of the game — can be launched on PC directly.
Packages/	Unity package references and dependencies.
ProjectSettings/	Project-wide settings (input, quality, tags, etc.).
.vscode/	VS Code workspace configuration files.
.gitignore	Git ignore rules to exclude temporary and build files.

--Features
Android Touch Controls – On-screen buttons for Left, Right, and Jump.
Editor & PC Keyboard Controls – Seamless keyboard input for testing (A/D or ←/→ for movement, Space for jump).
Multi-Level Scene Support – Touch controls auto-load across all game levels.
Responsive Design – Buttons dynamically scale and anchor for all screen sizes.
Cross-Platform Build Support – Works flawlessly on both Android and Windows builds.

--Controls
Platform	Movement	Jump
Windows / Editor	A / D or ← / →	Space
Android	On-screen Left / Right buttons	On-screen Jump button

--Build Information
-Android Build:
Located in the Android_Build folder.
→ File: Android_Build.apk
Install directly on your Android device to play.
-Windows Build:
Located in the WinBuild folder.
→ Contains Unfair_sample.exe and data files for Windows execution.

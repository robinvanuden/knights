# Knights (Unity Project)

## Overview
Knights is a Unity project set up with the 2D toolchain and Universal Render Pipeline (URP). The repository currently contains Unity scenes and project configuration; no custom C# gameplay scripts were detected yet. It’s ready to be used as a base for a 2D URP project with the new Input System and the Unity Test Framework enabled.

## Tech Stack / Dependencies
- Engine: Unity (detected locally via environment: 6000.2.14f1)
  - 2D packages: `com.unity.2d.animation`, `com.unity.2d.sprite`, `com.unity.2d.psdimporter`, `com.unity.2d.spriteshape`, `com.unity.2d.tilemap`, `com.unity.2d.tilemap.extras`
  - Rendering: `com.unity.render-pipelines.universal` (URP)
  - Input: `com.unity.inputsystem`
  - UI: `com.unity.ugui`
  - Timeline: `com.unity.timeline`
  - Visual Scripting: `com.unity.visualscripting`
  - IDE integrations: `com.unity.ide.rider`, `com.unity.ide.visualstudio`
  - Tests: `com.unity.test-framework`
  - See `Packages/manifest.json` for the full list and exact versions.

## Project Structure
At the repository root:
- `Assets/`
  - `Scenes/`
    - `SampleScene.unity`
  - `Settings/Scenes/`
    - `URP2DSceneTemplate.unity` (template)
- `Packages/manifest.json` — Unity packages and versions
- `ProjectSettings/` — Unity project settings (editor, graphics, input, etc.)
- `UserSettings/` — Per-user editor settings (not for CI)
- `Logs/`, `Library/`, `Temp/` — Unity-generated; not meant for version control
- `knights.sln` — Solution file for IDEs (Rider/Visual Studio)
- `README.md` — This file

## Requirements
- Unity Editor: 6000.2.14f1 or compatible
  - TODO: Confirm the exact required version from `ProjectSettings/ProjectVersion.txt` if present.
- Platform build support modules for your target(s): Windows/Mac/Linux, Android, iOS, WebGL, etc. (install via Unity Hub)
- An IDE with Unity integration (optional but recommended):
  - JetBrains Rider (Unity plugin) or Visual Studio with the Unity workload

## Getting Started
1. Install Unity via Unity Hub (recommended version: 6000.2.14f1).
2. Clone the repository:
   ```bash
   git clone <this-repo-url>
   cd knights
   ```
3. Open the project:
   - Unity Hub: Add the project folder and open it; or
   - IDE: Open `knights.sln` in Rider/Visual Studio, then open in Unity when prompted.
4. Unity will import packages automatically based on `Packages/manifest.json`.

## Running in the Editor
1. In Unity, open a scene (e.g., `Assets/Scenes/SampleScene.unity`).
2. Press Play.

Entry point notes:
- Unity’s runtime entry point is determined by the first scene listed in Build Settings. The repository currently includes `SampleScene.unity` and a URP 2D scene template.
- TODO: Define the production build scenes order in File → Build Settings and commit any new scenes under `Assets/Scenes/`.

## Building
In Unity Editor:
- File → Build Settings → select your target platform → Add Open Scenes → Build.

Headless/CI example (Windows, adjust paths/platform as needed):
```powershell
"C:\Program Files\Unity\Hub\Editor\6000.2.14f1\Editor\Unity.exe" `
  -batchmode -nographics -quit `
  -projectPath "C:\path\to\knights" `
  -buildWindows64Player "C:\path\to\build\Knights.exe"
```
Notes:
- Ensure the target platform support module is installed in Unity Hub.
- Replace the editor path/version to match your environment.

## Scripts and Automation
This Unity project does not use external package managers (like npm, pip) for runtime code. Package management is handled by Unity’s Package Manager via `Packages/manifest.json`.

Common editor tasks (manual):
- Reimport all assets: Assets → Reimport All
- Regenerate solution: Assets → Open C# Project
- Update packages: Window → Package Manager → In Project → Update

Command-line testing (example): see Testing section below for `-runTests` configuration.

## Environment Variables
No required environment variables are defined for local development.
- TODO: Document any environment variables needed for CI/CD (e.g., `UNITY_LICENSE`, `UNITY_EMAIL`, `UNITY_PASSWORD` or `UNITY_SERIAL` for legacy activation, or `UNITY_AUTH_TOKEN` for ULF-based activation).
- TODO: Document any game-specific config once implemented.

## Testing
Unity Test Framework is included (`com.unity.test-framework`). You can write PlayMode and EditMode tests.

Run tests in the Editor:
1. Window → General → Test Runner (or Window → Test Runner depending on version)
2. Select EditMode/PlayMode and Run All

Run tests from CLI (example, adjust editor path):
```powershell
"C:\Program Files\Unity\Hub\Editor\6000.2.14f1\Editor\Unity.exe" `
  -batchmode -nographics -quit `
  -projectPath "C:\path\to\knights" `
  -runTests -testPlatform EditMode `
  -logFile "C:\path\to\logs\editmode-tests.log" `
  -testResults "C:\path\to\results\editmode-results.xml"
```

Project status:
- No custom C# test files were detected yet in `Assets` at the time of writing.

## Known Scenes / Content
- `Assets/Scenes/SampleScene.unity`
- `Assets/Settings/Scenes/URP2DSceneTemplate.unity` (template)

TODOs:
- Add and document main gameplay scenes
- Configure URP assets and renderer settings if needed

## Troubleshooting
- Stuck on import/compilation: Close Unity, delete `Library/` and `Temp/` (Unity will regenerate), then reopen.
- Missing packages or compilation errors: Open Package Manager and click “Resolve” or “Update”. Ensure your Unity version matches or exceeds the versions required by packages in `manifest.json`.
- IDE integration issues: Reopen the project from Unity via Assets → Open C# Project. Ensure Rider/VS Unity plugins are installed.

## CI/CD
- TODO: Add CI workflow (e.g., GitHub Actions) for building and running tests via Unity in batchmode.
  - Consider using GameCI or Unity Builder actions.

## License
No license file was found in the repository.
- TODO: Add a `LICENSE` file (e.g., MIT, Apache-2.0, or proprietary) and update this section.

# EndlessTankDriving: Gesture-Controlled Endless Runner Game

## Gameplay Demonstration

![demo](./demo.gif)

## Environment

### Unity

- Unity3D 2022.3.8f1c1

### Python

- python == 3.8.17
- keyboard == 0.13.5
- mediapipe == 0.10.5
- numpy == 1.26.3
- opencv_python == 4.8.1.78

## Get Started

To start the game, build the Unity project to generate the `Tankathon.exe` executable file.

### Keyboard Control

- A - Move Left.
- D - Move Right.
- J - Rotate Turret Left.
- L - Rotate Turret Right.
- K - Fire Standard Shell.
- P - (Consumes MP for special ability) Fire Auto-Tracking Missile.

### Gesture Interaction

- While running the game, the script `hand_control_model.py` must also be executed simultaneously.
- Gesture Control Guide: 
  - Left thumb's left-right swings control the tank's steering.
  - Right thumb's left-right swings control the turret rotation.
  - Pressing down with the left thumb fires standard shells. 
  - Pressing down with the right thumb fires auto-tracking missiles.
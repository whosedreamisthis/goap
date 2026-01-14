# GOAP Hospital Simulation

A Goal-Oriented Action Planning (GOAP) implementation in Unity, simulating a hospital environment where Patients and Nurses interact dynamically to complete medical workflows.

## Overview

This project utilizes a **GOAP (Goal-Oriented Action Planning)** architecture. Instead of hard-coding every NPC behavior, agents are given a set of actions and a goal. The `GPlanner` dynamically determines the most efficient sequence of actions (the "plan") to satisfy the agent's goals based on the current world state and their own beliefs.



### Key Components

* **`GPlanner.cs`**: The "brain" of the system. It builds a state-graph to find the cheapest path of actions to reach a goal.
* **`GAgent.cs`**: The controller for NPCs (Patients and Nurses). It manages goals, beliefs, and executes the action queue.
* **`GAction.cs`**: The base class for individual behaviors. Each action has preconditions (what must be true to start) and effects (what changes after completion).
* **`GWorld.cs`**: A singleton that tracks the global state, such as the number of waiting patients and available cubicles.
* **`GInventory.cs`**: A helper class for agents to store and retrieve GameObjects, such as assigned cubicles.

---

## Agent Logic & Workflows

### Patient Logic
The **Patient** agent aims to move from arrival to treatment and finally return home.
* **GoToHospital**: Arrives at the facility.
* **GoToWaitingRoom**: Enters the queue and updates the global world state so nurses know they are waiting.
* **Follow**: Once a nurse is assigned, the patient follows the nurse to an available cubicle.
* **GoHome**: Leaves the hospital once treatment is complete.

### Nurse Logic
The **Nurse** agent is focused on the recurring goal of treating patients.
* **GetPatient**: Retrieves the next patient from the `GWorld` queue and reserves a cubicle.
* **GoToCubicle**: Leads the patient to the reserved cubicle. Upon arrival, the nurse signals the patient that the escort is finished via the patient's beliefs.

---

## Technical Details

| Class | Responsibility |
| :--- | :--- |
| **`Node`** | Represents a state in the planning graph, tracking path cost and world state. |
| **`SubGoal`** | Defines a specific state (key-value pair) an agent wants to achieve. |
| **`WorldStateItem`** | A serializable helper to define preconditions and effects in the Unity Inspector. |

---

## How to Use

1.  **Environment Setup**: Ensure your Unity scene has a **NavMesh** baked, as agents rely on it for movement.
2.  **Resources**: Place cubicle objects in the scene and tag them as **"Cubicle"**. The `GWorld` will automatically find and queue them at start.
3.  **NPC Setup**:
    * Create a **Nurse** prefab with the `Nurse.cs` script and relevant `GAction` scripts (e.g., `GetPatient`, `GoToCubicle`).
    * Create a **Patient** prefab with the `Patient.cs` script and relevant `GAction` scripts (e.g., `GoToHospital`, `Follow`, `GoHome`).
4.  **Configuration**: In the Unity Inspector, set the **Preconditions** and **Effects** for each action. For example, `GetPatient` should have the effect `treatPatient = true` to satisfy the Nurse's goal.
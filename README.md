# 🔮 Amaze - Survival Maze Game
## todo after MVP
- [ ] Generating procedural mazes
- [ ] Hunger/Thirst System
- [ ] Dynamic change of day and night
- [ ] Arize ???
- [ ] Potions
- [ ] Library ???
- [ ] Damn hardcode

"Find the Exit. If It Exists." - by somebody

## 🔥**MVP**
**CORE COMPONENTS:**
1. **Two Playable Sections**:
    - `tutorial`
    - `winter`
2. **Simple `potion` System**
3. **Physics-based `inventory`**

## ❄️**SECTIONS**

The labyrinth is divided into **4 conceptual zones**:  
**Tutorial, Winter, ~~Music~~, ~~Darkness~~.**  
_(Strikethrough = tentative/unconfirmed)_

### **Tutorial**

_"Tutorial section is a.... tutorial (sorry for that <3)"_
**Completion Condition**:  
Exit to **Winter** section by:
1. Standard tutorial (WASD/brewing basics, etc).
2. Solving a **multi-path puzzle** with help from _"one strange gnome"_ 🙃.
3. that's it

### **Winter**

_"i practically have no info to share yet :)"_

**Core Mechanic**:
- **Cold Damage** → Little by little kills player without `Potion of Cold Immunity`.


## **🧪POTION BREWING SYSTEM**

Amaze has rather complicated potion making system. It goes through 3 stages.
#### **Stage 1: Ingredient Processing**

- **Location**: Alchemy Table
- **Action**:  
    Take a raw ingredient (e.g., a frog) → Gut it → Extract specific part (e.g., heart).  
    _Example: "Frog → Processed → Frog Heart"_

#### **Stage 2: Base Preparation**

- **Location**: Alchemy Table (same station)
- **Action**:
    1. Combine water + any herbs → Let steep for **1-2 in-game days**.
    2. Add processed ingredient (e.g., heart) to the infusion.

#### **Stage 3: Final Brewing**

- **Location**: Cauldron (new workstation)
- **Action**
    1. Transfer infusion to cauldron.
    2. **Mouse-controlled stirring** while **monitoring temperature** to avoid burning.

#### **Scene Flow**
- **Full-screen view** switches between:
    - Alchemy Table (Stages 1-2)
    - Cauldron (Stage 3)

## **🎒 Inventory System**
#### **Pocket Structure**

- **Two physical pockets**:
    - `LEFT POCKET` → Displayed on **left screen edge**.
    - `RIGHT POCKET` → Displayed on **right screen edge**.
        (no damn way, huh)

- **Interaction**:  
    Clicking a pocket opens **exclusive menu** showing _only its contents_.

#### **Item Behavior**

- **Placement**:
    - Items **do not snap to grid**.
    - Maintain **rotation/orientation** from when player dropped them.

- **Physics**:
    - **Weak gravity** affects items (e.g., slight settling/movement).
    - **Risk mechanic**: While sprinting → Small RNG chance for item to **fall out** of pocket.

# Sphere-of-the-Ancients
# 7th Quiz - THE REAL TEST! 🚀

## 🎯 Project Scope & Core Elements

| Component          | Description |
|--------------------|-------------|
| 🧠 Game Concept    | A sphere ascends a mountain, dodges falling rocks, collects blue shards, and reaches a portal to win |
| 📦 Key Prefabs     | Sphere (Player), Rock, Shard, Portal, Trees |
| 🎮 Movement        | Physics-based rolling using Rigidbody.AddTorque() with acceleration and jump |
| 🧱 Terrain Setup   | Generated via heightmap, textured with terrain layers, decorated with tree prefabs |
| 🎥 Camera System   | Third-person follow camera with dynamic offset using Camera.main.transform.position and LateUpdate() |
| 💡 Environment     | Soft fog, real-time lighting, ambient wind sounds, chill night-skybox |
| 🔁 Game Loop       | Rocks fall from above (deal damage), shards spawn every 5s (25% chance), player regenerates shield on collection |
| 🌀 Portal End      | Triggers win condition when player enters |

## 🛠️ Script Summary & Behavior

| Script                  | Purpose | Key Functions |
|-------------------------|---------|---------------|
| MagicSphereMovement.cs  | Manages movement, collisions, and jumping | AddTorque(), OnCollisionEnter, AddForce() |
| CameraFollow.cs         | Smooth camera follow with slight lag | LateUpdate(), offset logic |
| RockSpawner.cs          | Spawns rocks at intervals | InvokeRepeating(), destroy rocks when velocity is low |
| Rock.cs                 | Handles rock behavior on collision and self-cleanup | OnCollisionEnter(), IsSleeping() |
| ShardSpawner.cs         | Spawns shards with 25% probability | Random.value, Instantiate() |
| Shard.cs                | Detects collision with player, heals shield | OnTriggerEnter(), UI update |
| Portal.cs               | Ends game upon contact | OnTriggerEnter() |

## 🌌 Game Story

*A magical sphere must climb a dangerous mountain to reach the Summit Portal, a mysterious gateway at the peak. But the journey is not easy, as huge rocks fall from above, threatening to break its protective blue shield.*

*To survive, the sphere must dodge falling rocks while collecting blue shards, which restore lost parts of its shield. Every hit from a rock weakens the shield, making the climb even riskier.*

*Can the sphere overcome the mountain's challenges and complete its journey?*

## 💡 Beyond the Basics
✅ Sound effects for rain falling  
✅ Sound effects for rock impact and shard pickup  
✅ Simple UI with health/shield bar and shard count  

---

## 🏞️ 1. Terrain Development

![Terrain Heightmap](https://github.com/user-attachments/assets/c3057406-fc14-4e02-ae79-e4313c35b1f1)
![Terrain Texturing](https://github.com/user-attachments/assets/8d67f8f2-ee96-4cd4-b06f-106726587b16)
![Terrain Details](https://github.com/user-attachments/assets/cae165cb-81f1-47a2-921f-6684484f5311)

**💡 Extra Mile:**  
- Integrated "smoothlayers" heightmap script for soft blending  
- Optimized billboard distance and max mesh trees  
- Increased detail distance and shadow settings  

### Environment Enhancements
![Fog Settings](https://github.com/user-attachments/assets/a715aa93-deef-4dea-89d8-2dc264c1bd2c)  
*Added atmospheric fog to complement rain system*

![Rain Sound](https://github.com/user-attachments/assets/8f4c67fc-f8ee-45c1-8bd2-bc6004e82278)  
*Implemented rain sound effects for immersion*

[Rain Particle Tutorial](https://www.youtube.com/watch?v=MBVGUD5nZeA)

---

## 🌳 2. Tree System

![Tree Placement](https://github.com/user-attachments/assets/81ab7c7b-cb99-4b79-928b-5e8a36823d1c)
![Tree Colliders](https://github.com/user-attachments/assets/b28b315b-e021-46e0-8b5a-cefb5bab1830)
![Wind Effect](https://github.com/user-attachments/assets/9a6b657c-86d7-4434-ad34-710d2fc0af1f)
![Tree Bend](https://github.com/user-attachments/assets/347b7144-1d55-43b4-aedc-7c78cc0f3573)

**💡 Extra Mile:**  
Added bend factor to trees for natural wind motion  

---

## 🌪️ 3. Wind System (EAST Direction)

![Wind Zone](https://github.com/user-attachments/assets/122be97b-c53c-4f2a-9fbf-bd9776c18689)
![Wind Settings](https://github.com/user-attachments/assets/c852a75a-e3e3-404b-aae4-ac8a6553c133)

---

## 🎥 4. Camera System

![Camera Orbit](https://github.com/user-attachments/assets/0e49aa62-a6e0-4f6f-a398-41a8646150cf)
![Camera Follow](https://github.com/user-attachments/assets/02eb5ff7-9668-4359-8adf-7b990ac76bc0)
![Camera Perspective](https://github.com/user-attachments/assets/35fb1fbb-2246-4a56-9a69-bceeeaddf420)

*Implemented 3-ring orbit follow system for optimal gameplay perspectives*

---

## 🌌 5. Portal Mechanics

![Portal Design](https://github.com/user-attachments/assets/f9b0114a-b034-45d3-afc9-0499198c1167)
![Portal Effect](https://github.com/user-attachments/assets/63d7b086-b833-4be7-97a5-7ca257ffc66f)
![Portal Blink](https://github.com/user-attachments/assets/c50daabc-dccb-43a2-99a1-346d4f704638)

**💡 Extra Mile:**  
Created blinking effect instead of static appearance  

---

## ✨ 6. Magic Sphere Player

![Sphere Design](https://github.com/user-attachments/assets/aaaf3034-36a2-412f-b7ce-130c9d1d417f)
![Sphere Physics](https://github.com/user-attachments/assets/534f4207-775c-4dfa-bfcb-9e9a1cd514cc)
![Sphere Movement](https://github.com/user-attachments/assets/889e08fa-e92d-4691-a165-aaff5332efee)

**Movement Features:**  
- No Flappy Bird jumping (grounded checks only)  
- Torque-based movement for realistic rolling  
- Rigidbody physics integration  

---

## 🌧️ 7. Rain System

![Rain Particles](https://github.com/user-attachments/assets/2c111373-9fef-43bf-8873-fe34016cb272)
![Rain Settings](https://github.com/user-attachments/assets/85509a60-f5cd-46c9-a64d-0964ddd3a8d7)

**System Configuration:**  
- 200 particles/sec emission  
- Collision-enabled drops  
- Follow-player system for performance  
- Sound effects synchronization  

**💡 Extra Mile:**  
"I didn't just make particles - I engineered weather 🌧️🛠️"

---
## 🎮 What Makes This Game "Me" - The Ultimate Breakdown

| Category          | What I Learned 🧠 | Funny Moments 😂 | Special Touches ✨ | Technical Wins 💻 |
|-------------------|-------------------|------------------|--------------------|-------------------|
| **Physics**       | Torque > Force for rolling | Sphere moonwalk bug 🕺 | Realistic rock tumbles | Perfect grounded checks |
| **Environment**   | Heightmap magic | Antarctica simulator ❄️ | Dynamic weather system | Optimized tree LODs |
| **Game Feel**     | Juice matters | Portal that blinks...menacingly 👁️ | Satisfying shard "ping" | Camera orbit rings |
| **Debugging**     | Console.log is life | "Why are trees ghosts?!" 👻 | Added debug rain toggle | Fixed infinite jump |
| **Art**           | Shader graph basics | "Modern art" terrain phase 🎨 | Custom portal glow | Performance-first shadows |
| **Sound**         | AudioSource nuances | Rain that sounds like bacon 🥓 | Environmental wind FX | Spatial blend setup |
| **UX**            | UI communicates state | Health bar that lied 😅 | Clear shard counter | Shield regen feedback |
| **Story**         | Emergent narrative | "Rock attack: revenge" plot 📖 | Environmental storytelling | Win/lose conditions |

| **My Signature Moves** | **Description** |
|------------------------|----------------|
| 🌧️ Rain Engineer | Built meteorologically-accurate weather |
| 🌀 Torque Master | Physics-based movement that feels *just right* |
| 🎮 Juice Wizard | Added satisfying SFX/VFX to every interaction |
| 🐛 Debug Shaman | Fixed bizarre bugs with creative solutions |
| 🖌️ Lazy Artist | Made simple assets look polished through lighting |


# [BulletPool (GMTK Game Jam)](https://omgyanii.itch.io/bulletpool)

A puzzle-platforming prototype created for the GMTK Game Jam.

You play as a definitely-not-trademark-infringing mercenary tasked with eliminating high-profile targets. The only problem? You didn't bring enough ammunition. Every bullet has to count, forcing you to solve each level through careful positioning, ricochets, and creative shot placement.

This was my first game jam, and completing a fully playable game within the time limit was a fantastic learning experience. While a few planned features had to be cut before the deadline, all five puzzle levels are fully playable.

## Features

* Five handcrafted puzzle levels
* Light platforming
* Bullet ricochet mechanics
* Bullet piercing
* Destructible objects

## Technical Highlights

Although this project was built in Unity, one of its more unusual aspects is that it avoids Unity's built-in physics system for projectile simulation.

Instead, bullet movement, collision detection, and ricochet calculations are handled manually through custom code. This approach gave me much finer control over projectile behavior and was an interesting challenge during development.

Like many game jam projects, some systems are more polished than others. The gameplay code generally follows a loose MVP-inspired architecture, although a few scripts became increasingly coupled as the jam progressed and time became limited.

## Project Status

This repository reflects the game as it was submitted (plus a few post-jam changes).

Some planned features were cut to meet the jam deadline, including:

* Level select
* Save/load functionality

The save system was removed during later development and is planned to be redesigned in the future alongside level selection.

## Controls

| Action | Control           |
| ------ | ----------------- |
| Move   | WASD              |
| Jump   | Space             |
| Shoot  | Left Mouse Button |

## Running the Project

Open the project using a compatible version of Unity.

Once opened, load the MainMenu scene and press Play.

## Credits

### Art

Craftpix.net

https://craftpix.net/sets/cyberpunk-platformer-asset-pixel-art/

### Music

Kulakovka (Pixabay)

https://pixabay.com/music/beats-urban-298903/

### Sound Effects

* saboteurcomics (Pixabay)
* micahlg (Freesound)
* morganpurkis (Freesound)

https://pixabay.com/sound-effects/film-special-effects-superfast-crunch-405118/

https://pixabay.com/sound-effects/people-male-hurt7-48124/

https://pixabay.com/sound-effects/film-special-effects-usp-pistol-sfx-80490/

## License

The source code is licensed under the GNU General Public License v3.0 (GPL-3.0).

Third-party artwork, music, fonts, and other assets are **not** covered by the GPL and remain the property of their respective copyright holders under their original licenses.

## Development Philosophy

No generative AI was used during the development of this project.

I do not use generative AI for programming, writing, art, music, design, ideation, planning, or other creative or technical work. Unless explicitly credited otherwise, everything in this project was created through my own skill, effort, and research.

## Acknowledgements

Thanks for checking out the project! Feedback, bug reports, and suggestions are always appreciated. This was my first game jam, and there's still plenty for me to improve, but I'm proud of what I was able to build within the time limit.

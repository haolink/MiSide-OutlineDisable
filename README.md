# MiSide Outline Disable Mod

-----

Mod to potentially disable outlines used in MiSide.

-----

BepInEx installation guide:
1. download `BepInEx-Unity.IL2CPP-win-x64-6.0.0-pre.2.zip` from https://github.com/BepInEx/BepInEx/releases (direct link https://github.com/BepInEx/BepInEx/releases/download/v6.0.0-pre.2/BepInEx-Unity.IL2CPP-win-x64-6.0.0-pre.2.zip )
2. extract all files to MiSide folder (it is in [steam]\steamapps\common\MiSide by default)
3. launch the game and check if it loads properly, you should see a console window appearing
4. (optional) If it doesn't – please open `doorstop_config.ini` in the MiSide folder and search for the line `ignore_disable_switch` – and set it to `true`. Yes, they warn you about this. But yes, on some setups this might be required (on mine for example).
5. Exit the game and install the plugin - all information is provided on NexusMods.

# Attribution

The code of this plugin is based on the MiSide console unlocker, available at https://github.com/Rist8/MiSide-Console-Unlocker . It is not a fork, it is just based on some conceptual ideas.
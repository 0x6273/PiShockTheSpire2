# PiShockTheSpire2

> [!CAUTION]
> The contents of this repository are related with the use of an adult themed toy with the explicit purpose of inflicting pain. Viewer discretion is advised.

> [!Warning]
> This mod is in beta. Some errors are expected to happen while using it. Feel free to report any issues or give any feedback you want.

Source code for the PiShock The Spire 2 mod.


## Quick Setup Guide for PiShockTheSpire2 

> [!Note]
> If you play on steam, you can download PiShock The Spire 2 directly from the Workshop: [Click here](https://steamcommunity.com/sharedfiles/filedetails/?id=3747608882).

- **Step 0 (optional):** Set your game in the **public-beta** branch.

We recomend playing in the stable version of the mod, but if you wish to play the beta version of the game, the mod will have a beta-branch release version to support it.

- **Step 1:** Download the intended version of the mod listed in the [Releases section](https://github.com/Yahlunna/PiShockTheSpire2/releases).

Select between "Stable Version" if you are playing normally, or "Beta Version" if you wish to play in the beta version of the game. Keep in mind the beta branch is always changing, and its prone to break stuff! If beta-branch isnt working, please use the stable version until the beta version us properly updated (this could take a few days)
This mod uses BaseLib as a dependency. You can download BaseLib from [here](https://github.com/Alchyr/BaseLib-StS2). 

- **Step 2:** Install the mod.

First, browse into your StS2 installation folder. Once there, create a "mods" folder.

Extract the files from "PiShockTheSpire2.zip" into this folder. You will also need to keep BaseLib here. A successful installation should look like this:

![](https://i.imgur.com/uQXrXvu.png)

- **Step 3:** Execute the game and make sure the mod is running:

You can see your list of mods at `Settings > General > Modding`, by clicking in the Mod Settings button.

![](https://i.imgur.com/sMEKwjR.png)

This should look similar to this:

![](https://i.imgur.com/AOzuDsT.png)

> [!Warning]
> If you mod Slay The Spire 2, your save file will be safely stored and you will start your modded StS2 with a different one.
> To restore your main save file, simply disable all your mods in the menu above, and reset the game.

- **Step 4:** Configure the mod

You can now open the Mod Settings menu and configure PiShock The Spire 2 to link it with PiShock or OpenShock shockers.

The configuration menu will look similar to this:

![](https://i.imgur.com/ju8EZS3.png)

You will need to fill 2 parameters in order to run this mod: an **API Key** for your account, and the **IDs of the Shockers** you want to use.

<details>

<summary>PiShock Instructions</summary>

1. Log in to the [PiShock website](https://login.pishock.com).
2. On the Account page, go to API keys and click "ADD NEW."
3. Give it a name and click "Create."
4. Copy it and paste it into the ingame mod settings "API Key" field.
5. Go to Control and then click the gear icon for the shocker you want to use.
6. Copy the shocker ID number. (Note that this is a different thing from your **Hub ID**. You have to use the **Shocker ID**.)
7. Paste this ID number into the mod settings "Shocker ID" field.
8. You can add additional shockers this way using the "Optional Extra Shocker ID" fields.
</details>

<details>

<summary>OpenShock Instructions</summary>

1. Log in to the [OpenShock webapp](https://openshock.app).
2. Click "API Tokens" then "Generate Token".
3. Give it a name and click Generate. You don't need to change any permissions.
4. Copy it and paste it into the ingame mod settings "API Key" field.
5. Click "Shockers" on the OpenShock webapp.
6. For the shocker you want to use, click the 3 dots to the right of the name and then "Copy ID."
7. Paste this ID into the mod settings "Shocker ID" field.
8. You can add additional shockers this way using the "Optional Extra Shocker ID" fields.

#### Self-hosted OpenShock Instructions
If you self-host OpenShock, you will need to edit the mod config file manually to override the API URL. Open the Slay the Spire 2 data directory in your file explorer (**not** the game install directory). On Windows this is in `AppData\Roaming\SlayTheSpire2`.
Open the `mod_configs` directory and open `PiShockTheSpire2.cfg` in a text editor. Find the line `"OpenShockApiUrl": "",` and put your own URL (for example "http://localhost:8080") inside the empty quotes.
</details>

![](https://i.imgur.com/kufZzvc.png)

Feel free to adjust all the other given parameters to your liking to adapt the mod to your preferred experience.


- **Step 5 (optional):** Debugging

You can use the Test current Shocker button in The PiShockTheSpire2 menu to check if the Shocker has linked properly with the game.
Pressing the test button will try to make the Shocker vibrate at the max intensity and duration that you have configured.

If your shocker is not working properly pr you want to report an issue with the mod, please send me your ingame logs (you can find them at `(...)\AppData\Roaming\SlayTheSpire2\logs`, under the name of godot.log

**Advanced:** You can also manually debug any issue you have in the BaseLib console to check for the API response codes when a Shock operation is requested.

You can open an ingame console with the [`] key. On it, just type:

> showlog  

This command opens a live log dump where you can see any relevant debug information for the game, BaseLib, PiShockTheSpire2 and other mods. Feel free to use it to track any API issue you might be experiencing.

The command also opens your ingame log folder automatically with the following command:

> open logs

This way, you dont need to search for your SlayTheSpire2\logs folder.

Feel free to use the "Verbose Logs" option in the mod settings in order to find additional info on where the mod might be experiencign issues!

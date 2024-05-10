[img]https://i.imgur.com/fVVaDCS.gif[/img]


[img]https://i.imgur.com/3D6U1Br.png[/img]
[quote]
A PvP Arena Map inspired by the old X-Wing vs. TIE Fighter games. Players are assigned to either Rebels or Imperials and, once the match is started by an admin, can spawn X-Wings and TIE Fighters from their spawn station's spawn bays to fight their opponents in a team deatchmatch. The score screen displays the remaining tokens for each faction and the faction that runs out, loses the match. The map can be reset by reloading it.
[/quote]

[url=https://steamcommunity.com/sharedfiles/filedetails/?id=2978742816][img]https://i.imgur.com/XytuSmz.png[/img][/url]


[img]https://i.imgur.com/TGu2yer.png[/img]
[img]https://i.imgur.com/vVlK2jA.gif[/img]
[img]https://i.imgur.com/UEB2ALo.gif[/img]
[img]https://i.imgur.com/97cLjUt.gif[/img]

[img]https://i.imgur.com/Z6WAb5w.png[/img]
[olist]
[*][b]Load[/b] the map on Steam or add it onto your dedicated server.
[list][*]If you're using a [b]dedicated server[/b], ensure that all the mods and their dependencies are added to the world.
[/list]
[*][b]Host the map[/b] and wait for players to join. They will be [b]automatically assigned[/b] to either Rebels or Imperials and can spawn on their respective spawn station.
[list][*]Be sure to [b]switch your character[/b] to the Rebel Pilot / TIE Pilot one in the medbay's character dropdown menu!
[/list]
[*]Once you're [b]ready to start[/b], the admin must send the [b]/start[/b] chat command. Now the players can [b]spawn X-Wings and TIE Fighters[/b] from the [b]red buttons in the spawning bay[/b] of the main hall in the spawn stations.
[list][*][b]IMPORTANT:[/b] Do not leave or depower your ship - that counts as leaving the fight and you will be destroyed!
[/list]
[*]Each team has a [b]pool of tokens[/b] assigned to them. A token is used up, when a player of said team is killed in battle. Once a faction's tokens run out, no more TIEs or X-Wings can be spawned and the match ends.
[list][*]Players cannot leave the arena, which spans 5km across. A warning will be displayed when they reach the edge and they will be bounced back into bounds should they try to leave the area.
[/list]
[*][b]Reload the map[/b] to start the next round.
[/olist]

[img]https://i.imgur.com/pygx0qC.png[/img]

[img]https://i.imgur.com/bKN1G3e.png[/img]
[h2]Balance[/h2]
[list]
[*]Balance is still a work in progress. Part of the reason why I'm releasing this is to improve it, so if you have feedback, please let me know!
That being said, balance is also not necessarily supposed to be equal. An X-Wing should defeat a TIE Fighter in a 1 vs 1 on a normal basis. But if the TIE Fighter flies well and / or works together with other TIE Fighters, they should be able to take down an X-Wing.
[*]TIEs generally are much more agile and accelerate faster than X-Wings. Their lasers also do more damage, but overheat faster too. X-Wings are slower to turn and accelerate but have shields and can shoot for longer before overheating. This makes the X-Wing a bit more beginner-friendly than the TIE Fighter.
[/list]

[h2]Configuration[/h2]
[list]
[*]A configuration file will be created in the world's folder on first load. This config file can be used to customize certain aspects of the experience. It can be found in [b]/WorldName/Storage/<GFABattle>/[/b]. The main thing it will allow you to do is to alter the amount of tokens per faction.
Note that the current distribution of 40:60 is intended to take into account the X-Wing's edge in combat power over the TIE Fighter.
[*]If you want to experiment with balance, note that the [url=https://github.com/Ash-LikeSnow/WeaponCore/wiki/CoreSystems-Server-Config-Options]WeaponCore server configs[/url], which you will find inside your savegame, allow you to overwrite weapon parameters.
[/list]

[h2]Feedback[/h2]
[list]
[*]Feedback is very much welcome!
[*]If you'd like to help contribute to the balancing effort, please consider sending me the WeaponCore Combat Logs. They contain a significant amount of data about damage dealt and by which weapons, which is invaluable for analysis of how things are performing. You can find these logs in:
[b]\%APPDATA%\Roaming\SpaceEngineers\Storage\3154371364.sbm_CoreSystems\[/b]
The relevant log types are:
[list]
[*]combat
[*]dmgstats
[*]griddmgstats
[/list]
[/list]


[img]https://i.imgur.com/4canKDu.png[/img]

[b]Q: Can you make a version without WeaponCore / Animation Engine?[/b]
No. Both mods are integral to the functionality.

[b]Q: Will there be a mod.io version?[/b]
No. This map requires various scripts to work, none of which work on consoles.

[b]Q: How do I spawn a ship?[/b]
Once an admin has started the round with the [b]/start[/b] chat command, ships can be spawned from your faction's spawn bays on the main floor of the spawn station. To spawn a ship, press the red button on the button panel in front of the bay.

[b]Q: How do I switch my character to the TIE Pilot / Rebel Pilot?[/b]
There are medical bays on the top floor of the spawn stations. Access their right side character section (where you'd also equip different suit options) and select the character you want to use from the dropdown at the bottom of the medbay screen.

[b]Q: Why do the Imperials have more tokens than the Rebels?[/b]
This is part of the effort to balance out the power imbalance (which is intended, to a degree) between the X-Wing and the TIE Fighter. The classic situation is many TIEs against fewer X-Wings, and the Imperials having more respawn tokens simulates that.


[img]https://i.imgur.com/Ky5F2bo.png[/img]
[list]
[*][b]Klime[/b] - For writing the entire score tracking / spawning / rounds system without which this map would barely work.
[*][b]Chipstix213[/b] - For bringing the amazing TIE Fighter and Rebel Pilot characters into the game - and of course general help.
[*][b]The Default Trash Bag[/b] - For building the centerpiece Imperial Construction Module.
[*][b]Invalid[/b] - For providing me with the script to restrict the play area.
[/list]
![Header](MacroDeckVoicemeeterSocial.png)

<img alt="Macro Deck 2 Community Plugin" height="64px" align="center" href="https://macrodeck.org" src="https://macrodeck.org/images/macro_deck_2_community_plugin.png"/>

***
*This is a plugin for Macro Deck 2, it does NOT function as a standalone app*
***
## Control Voicemeeter from Macro Deck
Create a button and configure how you want to control Voicemeeter.

[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/S6S87RY9H)

***
## Features
#### Available actions

| Action           | Function                                                     | Description                                                                                                                                                                                                                       |
|------------------|--------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Toggle           | Toggle a button on a strip, a bus, or the recorder(cassette) | The most common toggles are implemented and can be selected by device name and option <br/> (Mute, A1-B3, Record/Play/Stop cassette, etc)                                                                                         |
| Slider control   | Change a slider by a given amount                            | Change slider by a relative amount (between -10 and +10)                                                                                                                                                                          |
| Program commands | Execute Voicemeeter commands                                 | Shutdown, Restart, Show, Reset/Load/Save configuration, Load/Eject cassette                                                                                                                                                       |
| Advanced/Custom  | Send a custom command to Voicemeeter                         | This option allows configuring custom commands using the Voicemeeter API language. <br/> Please see the official [VoicemeeterRemoteAPI pdf](https://download.vb-audio.com/Download_CABLE/VoicemeeterRemoteAPI.pdf) for more info. |
| Use Macro Button | Activate/Toggle a Macro Button (Voicemeeter)                 | Activate or toggle a Voicemeeter Macro Button. Ids are 0-79 and you will need to find and confugure them separately, according to the documentation for your Voicemeeter version.                              |

#### Available parameters/variables

| Device    | Toggles                   | Ranges | Strings |
|-----------|---------------------------|--------|---------|
| Strip     | Mute, Mono, Solo, A1-B3*  | Gain   | Label   |
| Bus       | Mute, Mono, Eq.On*, Sel*  | Gain   | Label   |
| Recorder* | Stop, Play, Record, Pause | Gain   | -       |

**Availablity of these varies with your Voicemeeter installation. Some features can only be accessed with Banana or Potato*

#### Add custom parameters to variables

Any parameter in Voicemeeter that you want to monitor can be added with the same syntax as the Advanced/Custom action.\
As a bonus, the parameters you add can also be used in the available actions as if they had been provided in the plugin! (must be related to a device)\
Access this window from the Plugin Configuration button or by clicking the status icon.\
![Addtional Parameters](addtionalParameters.png)

Clicking on an option that you've already included allows you to edit or delete it. To add a new parameter, you must provide the Parameter and the Type.

>- Check the API docs: 0 or 1 values are generally used as bool, ranges are almost always float, strings are...strings.
>- Make sure to use the New, Save and Delete buttons before you click Ok.
>- **Restart Macro Deck** to refresh with the variables you've added or removed
>- I can only provide minimal support for this since it's all your own stuff being added. Use it wisely.

#### Use Voicemeeter Macro Buttons

Macro Buttons are a feature of Voicemeeter that allows you to execute a series of commands with a single button press.\
Some actions are only possible with Macro Buttons, like sending special commands (SendText, Wait, etc.).

To use a Macro Button, you must first create it in Voicemeeter and then configure the plugin to use it.\
The plugin will only activate or toggle the button, it will not create or edit the button for you.

The Macro Button ID is a number between 0 and 79, and you can find it in the Voicemeeter Macro Buttons editor window as "Logical ID".\

***
## Need this in your language?
This plugin has its own localization files, independent of Macro Deck.
If your language is not available, the plugin will default to English.

Check the files available in source under [Languages](MacroDeck.Voicemeeter/MacroDeck.Voicemeeter/Languages).
If your language is missing or incomplete, please consider [helping me out by translating](https://poeditor.com/join/project/I1exM7PsOc)! 

Currently available languages:
- English
- Italian
- Spanish (by Danivar)
- Chinese (by PENC)

***
## Third party licenses
This plugin makes use of:
- [Macro Deck 2 by SuchByte (Apache License 2.0)](https://macrodeck.org)
- [Extended Voicemeeter Remote API wrapper (MIT)](https://github.com/A-tG/voicemeeter-remote-api-extended)
- [Voicemeeter Remote API dll C# dynamic wrapper (MIT)](https://github.com/A-tG/Voicemeeter-Remote-API-dll-dynamic-wrapper)
- [Dynamic wrapper for unmanaged dll (MIT)](https://github.com/A-tG/Dynamic-wrapper-for-unmanaged-dll)

***

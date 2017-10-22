<p align="center">
<a href="https://github.com/cDima/Aerial" target="_blank">
<img align="center" alt="apple tv windows aerial screen saver screencast" src="https://github.com/cDima/Aerial/raw/master/imgs/win.gif" />
</a>
</p>

<p align="center">
<a href="https://github.com/cDima/Aerial/stargazers" target="_blank">
<img alt="GitHub stars" src="https://img.shields.io/github/stars/cDima/Aerial.svg" />
</a>
<a href="https://github.com/cDima/Aerial/releases" target="_blank">
<img alt="Github All Releases" src="https://img.shields.io/github/downloads/cdima/aerial/total.svg?maxAge=2592000" />
</a>
<a href="https://ci.appveyor.com/project/cDima/aerial" target="_blank">
<img alt="Build status" src="https://ci.appveyor.com/api/projects/status/stpj3okbjpftjsad?svg=true" />
</a>
</p>

## Aerial - Apple TV Aerial Views Screen Saver for Windows 7, 8, 10+
Aerial is a Windows screen saver based on the new Apple TV screen saver that displays the aerial movies Apple shot over New York, San Francisco, Hawaii, China, etc.

Aerial for Windows is based on the [Mac Aerial Screen Saver](https://github.com/JohnCoates/Aerial) by [John Coates](https://github.com/JohnCoates).

### As featured on  

* <a href="http://lifehacker.com/get-the-apple-tvs-breathtaking-aerial-screensaver-on-ma-1790533201" target="_blank">lifehacker.com</a>
* <a href="https://www.neowin.net/news/download-aerial---apple-tv-aerial-views-screen-saver-for-windows" target="_blank">neowin.com</a>
* <a href="http://fieldguide.gizmodo.com/get-apple-tvs-new-screensavers-on-any-windows-pc-or-mac-1741804379" target="_blank">gizmodo.com</a>
* <a href="https://www.screensaversplanet.com/screensavers/apple-tv-aerial-views-827/" target="_blank">screensaversplanet.com</a>
* <a href="http://www.intowindows.com/get-new-apple-tv-aerial-screen-saver-in-windows-10-8-7/" target="_blank">intowindows.com</a>

<p align="center">
<a href="http://lifehacker.com/get-the-apple-tvs-breathtaking-aerial-screensaver-on-ma-1790533201" target="_blank">
<img align="center" alt="lifehacker aerial apple tv screensaver" src="http://advertising.gawker.com/assets/img/resources/lifehacker.png" />
</a>
<a href="http://fieldguide.gizmodo.com/get-apple-tvs-new-screensavers-on-any-windows-pc-or-mac-1741804379" target="_blank">
<img align="center" alt="gizmodo aerial apple tv screensaver" src="http://advertising.gawker.com/assets/img/resources/gizmodo.png" /></a>
<a href="https://www.neowin.net/news/download-aerial---apple-tv-aerial-views-screen-saver-for-windows" target="_blank">
<img align="center" alt="neowin aerial apple tv screensaver" src="https://upload.wikimedia.org/wikipedia/en/1/11/Neowin_Logo_1.png" /></a>
</p>

## Installation on Windows 7, and Windows 8

1. **[Download the Aerial .zip release](https://github.com/cDima/Aerial/releases)**
2. Right-click and select "Extract All..." to unzip the downloaded file. Just accept the default options from the Extraction wizard.
3. You should be presented with a folder containing **AerialScreenSaverV3.scr**  and **AerialScreenSaverV3.exe**. Copy the **AerialScreenSaverV3.scr** file to your Windows installation folder (by default this should be c:\Windows on most PCs).
4. Right click **AerialScreenSaverV3.scr** and choose Install, Windows will open Screen open Screen Saver Settings and set Aerial as the selected screensaver for you.
5. If any issues occur, please read the FAQ below or enter an issue to the tracker.

## Installation on Windows 10

1. **[Download the Aerial .zip release](https://github.com/cDima/Aerial/releases)**
2. Right-click and select "Extract All..." to unzip the downloaded file. Just accept the default options from the Extraction wizard.
3. You should be presented with a folder containing **AerialScreenSaverV3.scr**  and **AerialScreenSaverV3.exe**. Copy the **AerialScreenSaverV3.scr** file to your Windows installation folder (by default this should be c:\Windows on most PCs).
4. Type "screen saver" in the task bar search box. Select "Change screen saver" to open Screen open Screen Saver Settings.
5. Select **AerialScreenSaverV3** from the list of screen savers.
6. If any issues occur, please read the FAQ below or enter an issue to the tracker.

## Temporary 4K version
Apple has released another set of videos that have 4K support.  This isn't currently supported in the main branch, but a user has forked this project and added support.  To use the 4K video's instead, download Aerial.exe and Aerial.scr from [this folder](https://github.com/jonathonwpowell/Aerial/tree/temp4krelease/ScreenSaver/bin/Release).  Then run the exe from anywhere, click the settings button in the top right, and go to the video source tab and click the 4K button and save.  Close out of that program, then install the scr as you normally would and it should now use the 4K videos.

## Video instructions

* [The Geek Circle](https://www.youtube.com/watch?v=8fTiSQgb8Io)
* [Technical Ustad](https://www.youtube.com/watch?v=UZzyJhMoj_k)
* [Ryan Heuer](https://www.youtube.com/watch?v=cHy36rfocQo)

## Uninstallation on Windows 7, 8 and 10

To uninstall, delete the downloaded `.scr` file.

## Features
* **Auto Load Latest Aerials:** Aerials are loaded directly from Apple, so you're never out of date.
* **Play Different Aerial On Each Display:** If you've got multiple monitors, this setting loads a different aerial for each of your displays.

<p align="center"><img align="center" alt="windows aerial screen saver settings" src="imgs/settings.png" /></p>

## Compatibility
Aerial is written in C# for [.Net Framework v4.6](https://www.microsoft.com/en-us/download/details.aspx?id=48130).

## FAQ

> After insalling on Windows XP, 7, 8.1, I get an error message "This application could not be started"

Please [install Microsoft's .Net Framework 4.6](https://support.microsoft.com/en-us/kb/2715633).

> The app freezes or returns to desktop?

Try to install `Windows Media Player` via `Turn Windows features on or off` in the control panel.

> Blank black screen on screen saver preview?

The application needs an internet connection to work.

> BitBlocker / McAfee / execution blocking the download?

Historically `.scr` files have a bad history with anti-virus software — erroneously positive reports of this screensaver being a Malware or Generic Trojan or Unverified Executable is a [known issue](https://github.com/cDima/Aerial/issues/9), you can help by [reporting the source](https://www.opswat.com/blog/what-do-i-do-if-engine-detects-my-safe-file-threat) of this open source repository to the faulty anti-virus software companies. The builds are verified to be clean.

> Where are the .mov files located on Apple TV servers?

The movie files are [located at apple.com](https://github.com/cDima/Aerial/issues/55), they are cached to your local user directory, usually this: `C:\Users\YOURUSERNAME\AppData\Local\Aerial` 

> There's a red line on the screen saver!

This is an issue with video cards and  Windows Media Player, this can be solved by [disabling 'Demo Mode'](https://github.com/cDima/Aerial/issues/71#issuecomment-250318463).

> How do I setup a proxy?

To setup proxy information, create a [.config file as shown](https://github.com/cDima/Aerial/issues/87) alongside the screensaver.

> How do I change my screen saver on the most recent update of Windows 10?

Press the windows key, type in "lock screen settings", then it will be under "Screen Saver Settings" on that page.

## Community
- **Find a bug?** [Open an issue](https://github.com/cdima/Aerial/issues/new). Try to be as specific as possible.
- **Have a feature request** [Open an issue](https://github.com/cdima/Aerial/issues/new). Tell me why this feature would be useful, and why you and others would want it.

## Contribute
I appreciate all pull requests. Caching hasn't been added yet.

## Changelog

- October 27th, 2015 - 0.1: First release.
- November 5th, 2015 - 0.2: Multi-screenoptions and scaling:

<p align="center"><img align="center" alt="multiscreen aerial screensaver" src="imgs/multiscreen.gif" /></p>

## License
[MIT License](https://raw.githubusercontent.com/JohnCoates/Aerial/master/LICENSE)


#### Coded with :heart: in New York by [Dmitry Sadakov](http://sadakov.com/)

<p align="center"><img align="center" alt="Dmtiry Sadakov" src="imgs/dmitrysadakov.jpg" /></p>

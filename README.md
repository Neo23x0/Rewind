# Rewind
Immediate Virus Infection Counter Measures

## The Idea

Users that have opened a weaponized document are often aware that something is wrong with that document. In many cases they notice suspicious activity or aspects like 

- a command line window pops up for a fraction of a second
- no contents show up after clicking "Enable Content" 
- a ransom note appears on the desktop
- another program gets started without their interaction (e.g. browser window)
- the attached document doesn't open at all (.docx.exe extensions)

We can use the moments of realization to prevent further damage caused by malicious code by providing an emergency button that users can press when they believe that sh++ hit the fan. 

Rewind tries to kill or undo changes that occured recently by ending all recently spawned processes and removing all recently created files of certain types, as well as removing recently modified registry keys used for persistence (e.g. RUN key entries) regardless of their actual values. 

This generic appreach doesn't recognize a specific type of threat but radically removes every process, file or registry key created within a very short time frame after a potential infection. 

## Features

- Kills all processes started within the last X minutes and accessible from the current user context

Not yet implemented in the POC
- Removes all files written to disk within the last X minutes if extension is in the predefined blacklist (.exe, .dll, .ps1,.vbs, .hta, .bat, .js)
- Removes values from certain registry keys used for persitence if modified within the last X minutes

## Screenshots

![Proof-of-Concept](https://raw.githubusercontent.com/Neo23x0/Rewind/master/screens/poc1.png)
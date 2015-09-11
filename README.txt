First off, I hope you enjoy the works I've provided.  I hope someone can use this to provide 
a new Server Listing utility for Ultima Online freeshards.

STATE OF THIS PROJECT
---------------------
This is what was to be the next version of ConnectUO 2.  I was refactoring a lot of code, 
replacing libraries, making tons of changes, etc.  I do not know how stable this version 
is, I got distracted by real life in the middle of it and never got around to really testing 
it.  I have made sure that it currently compiles, as long as the libraries noted below are 
installed.  A lot of this code was written years ago, and never modernized, so sorry if its 
sloppy...  I was working to make things a bit more modular (or composite if you will).  This 
is why i started using Ninject.  Hopefully at the very least, this opens up peoples eyes to
how much work is really involved in an app that looks so simple (When you think about it, 
ConnectUO from a users perspective is really simple).

QUESTIONS
---------
Q: Why am I releasing this?
A: In short, I had a fallout with the way RunUO was being run, and due to that I was banned.  
Because of this, I see no reason to keep this work private.  I invested a lot of time into 
this, and for it to simply disappear would be a complete waste.

Q: You said you wouldn't release the source code because it could have an ill effect on the
community, why release it now?A: While this is true, I am not releasing the source code 
to the loader and patching dll's.  This work was not done by me, and because of this, I do 
not really have the right to release it.  The source code in the 2 dlls 
(EncPatcher.dll and Loader.dll) contain segments of the Razor source code and were both 
written by Zippy.  While I did maintain them, I do not feel it is right to open source his 
work.  The code within these 2 dlls are what could destroy the freeshard community, as it 
would open up the door to more malicious Razor style 3rd party apps.

Q: Are you releasing this to get back at Ryan and the RunUO Team?
A: Negative, while I feel Ryan has not been telling the truth about how things went down,
I do not care what happens with RunUO or ConnectUO in the future.  Hopefully it lives on, 
if not, oh well.  I truely wish RunUO and ConnectUO the best.  I want to release this as a 
tutorial/sample/example of what it takes to write a tool like this.  Looking back, I really
see no reason why ConnectUO was a closed ource project.  It could have been so much more 
had the community been involved in it's evolution.

REQUIRED LIBRARIES TO COMPILE THE SOLUTION
------------------------------------------
SQLite for .NET - http://sqlite.phxsoftware.com/
Ninject - http://ninject.org/
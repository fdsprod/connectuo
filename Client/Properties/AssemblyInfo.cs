using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("ConnectUO")]
[assembly: AssemblyDescription("ConnectUO is a Ultima Online free shard listing application maintained by Jeff Boulanger of the RunUO Development Team.")]
[assembly: AssemblyConfiguration("Alpha")]
[assembly: AssemblyCompany("Microsoft")]
[assembly: AssemblyProduct("ConnectUO")]
[assembly: AssemblyCopyright("Copyright © Microsoft 2008")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("3eb6c227-fe0d-4486-8dc3-62b48f4ded22")]

/*
    major.minor[.maintenance[.build]]

    The major number is increased when there are significant jumps in functionality
    The minor number is incremented when only minor features or significant fixes have been added.
                
    Maintenance can be used:                
        0 for alpha status
        1 for beta status
        2 for release candidate
        3 for public release
             
    Examples: 
        1.2.0.1 – For our internal unit/system test – While we make sure everything is ok when transitioning from development to system test.
        1.2.1.2 - (beta with some bug fixes) –System Test (at client discretion we could put this on their site, preferably not)
        1.2.2.3 - Ready for UAT (at the client’s)
        1.2.3.0 – Production, baby.
        1.2.3.5 – Production with many bug fixes (just to exemplify, hopefully is not the case)            
*/

[assembly: AssemblyVersion("2.0.3.1")]
[assembly: AssemblyFileVersion("2.0.3.1")]

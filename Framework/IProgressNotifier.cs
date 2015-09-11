/********************************************************
 * 
 *  $Id: IProgressNotifier.cs 29 2009-09-11 22:19:25Z jeff $
 *  
 *  $Author: jeff $
 *  $Date: 2009-09-11 15:19:25 -0700 (Fri, 11 Sep 2009) $
 *  $Revision: 29 $
 *  
 *  $LastChangedBy: jeff $
 *  $LastChangedDate: 2009-09-11 15:19:25 -0700 (Fri, 11 Sep 2009) $
 *  $LastChangedRevision: 29 $
 *  
 *  (C) Copyright 2009 Jeff Boulanger
 *  All rights reserved. 
 *  
 ********************************************************/

using System;

namespace ConnectUO.Framework
{
    public interface IProgressNotifier
    {
        event EventHandler<ProgressUpdateEventArgs> ProgressUpdate;
        event EventHandler<ProgressStartedEventArgs> ProgressStarted;
        event EventHandler<ProgressCompletedEventArgs> ProgressCompleted;
    }
}

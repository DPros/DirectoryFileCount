﻿using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceProcess;
using System.Threading;
using DirectoryFileCount.Tools;

namespace DirectoryFileCount.Service
{
    public class DirectoryFileCountWindowsService : ServiceBase
    {
        internal const string CurrentServiceName = "DirectoryFileCountService";
        internal const string CurrentServiceDisplayName = "Directory File Count Service";
        internal const string CurrentServiceSource = "DirectoryFileCountServiceSource";
        internal const string CurrentServiceLogName = "DirectoryFileCountServiceLogName";
        internal const string CurrentServiceDescription = "Directory File Count for learning purposes.";
        private ServiceHost _serviceHost = null;
        private EventLog _serviceEventLog;
        private void InitializeComponent()
        {
            _serviceEventLog = new EventLog();
            ((System.ComponentModel.ISupportInitialize)(_serviceEventLog)).BeginInit();
            ServiceName = CurrentServiceName;
            ((System.ComponentModel.ISupportInitialize)(_serviceEventLog)).EndInit();
        }
        public DirectoryFileCountWindowsService()
        {
            ServiceName = CurrentServiceName;
            InitializeComponent();
            try
            {
                if (!EventLog.SourceExists(CurrentServiceSource))
                    EventLog.CreateEventSource(CurrentServiceSource, CurrentServiceLogName);
                _serviceEventLog.Source = CurrentServiceSource;
                _serviceEventLog.Log = CurrentServiceLogName;
                _serviceEventLog.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 0);
                _serviceEventLog.MaximumKilobytes = 8192;
            }
            catch (Exception ex)
            {
                Logger.Log("Failed To Initialize Log", ex);
            }
            try
            {
                AppDomain.CurrentDomain.UnhandledException += UnhandledException;
                try
                {
                    _serviceEventLog.WriteEntry("Initialization");
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                }
            }
            catch (Exception ex)
            {
                _serviceEventLog.WriteEntry(string.Format("Initialization{0}{1}ex.StackTrace = {2}{1}ex.InnerException.Message = {3}", ex.Message, Environment.NewLine, ex.StackTrace, (ex.InnerException == null ? "null" : ex.InnerException.Message)),
                    EventLogEntryType.Error);
                Logger.Log("Initialization", ex);
            }
        }

        protected override void OnStart(string[] args)
        {
            Logger.Log("OnStart");
            RequestAdditionalTime(120 * 1000);
#if DEBUG
//            for (int i = 0; i < 10; i++)
//            {
//                Thread.Sleep(1000);
//            }
#endif
            try
            {
                if (_serviceHost != null)
                    _serviceHost.Close();
            }
            catch
            {
            }
            try
            {
                _serviceHost = new ServiceHost(typeof(DirectoryFileCountService));
                _serviceHost.Open();
            }
            catch (Exception ex)
            {
                _serviceEventLog.WriteEntry(string.Format("Opening The Host: {0}", ex.Message), EventLogEntryType.Error);
                Logger.Log("OnStart", ex);
                throw;
            }
            Logger.Log("Service Started");
        }

        protected override void OnStop()
        {
            Logger.Log("OnStop");
            RequestAdditionalTime(120 * 1000);
#if DEBUG
            //Thread.Sleep(10000);
#endif
            try
            {
                _serviceHost.Close();
            }
            catch (Exception ex)
            {
                _serviceEventLog.WriteEntry(string.Format("Trying To Stop The Host Listener{0}", ex.Message),
                    EventLogEntryType.Error);
                Logger.Log("Trying To Stop The Host Listener", ex);
            }
            _serviceEventLog.WriteEntry("Service Stopped", EventLogEntryType.Information);
            Logger.Log("Service Stopped");
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            var ex = (Exception)args.ExceptionObject;
            _serviceEventLog.WriteEntry(string.Format("UnhandledException {0} ex.Message = {1}{0} ex.StackTrace = {2}", Environment.NewLine, ex.Message, ex.StackTrace),
                EventLogEntryType.Error);

            Logger.Log("UnhandledException", ex);
        }
    }
}

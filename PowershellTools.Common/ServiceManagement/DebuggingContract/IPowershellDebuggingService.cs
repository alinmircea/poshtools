﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellTools.Common.ServiceManagement.DebuggingContract
{
    [ServiceContract(CallbackContract = typeof(IDebugEngineCallback))]
    public interface IPowerShellDebuggingService
    {
        [OperationContract]
        void SetBreakpoint(PowerShellBreakpoint bp);

        [OperationContract]
        void EnableBreakpoint(PowerShellBreakpoint bp, bool enable);

        [OperationContract]
        void RemoveBreakpoint(PowerShellBreakpoint bp);

        [OperationContract]
        void ClearBreakpoints();

        [OperationContract]
        bool Execute(string cmdline);

        [OperationContract]
        string ExecuteDebuggingCommandOutDefault(string cmdline);

        [OperationContract]
        string ExecuteDebuggingCommandOutNull(string cmdline);

        [OperationContract]
        void SetDebuggerResumeAction(DebuggerResumeAction resumeAction);

        [OperationContract]
        void Stop();

        [OperationContract]
        void SetRunspace(bool overrideExecutionPolicy);

        [OperationContract]
        void AttachToRunspace(uint pid);

        [OperationContract]
        void DetachFromRunspace();

        [OperationContract]
        bool IsAttachable(uint pid);

        [OperationContract]
        Collection<Variable> GetScopedVariable();

        [OperationContract]
        Collection<Variable> GetExpandedIEnumerableVariable(string varFullName);

        [OperationContract]
        Collection<Variable> GetPSObjectVariable(string varFullName);

        [OperationContract]
        Collection<Variable> GetObjectVariable(string varFullName);

        [OperationContract]
        IEnumerable<CallStack> GetCallStack();

        [OperationContract]
        string GetPrompt();

        [OperationContract]
        RunspaceAvailability GetRunspaceAvailability();

        [OperationContract]
        int GetPSBreakpointId(PowerShellBreakpoint bp);

        [OperationContract]
        void SetOption(PowerShellRawHostOptions option);

        [OperationContract]
        DebugScenario GetDebugScenario();
    }

}

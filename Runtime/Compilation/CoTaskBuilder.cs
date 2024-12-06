using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using Polib.CoTasks.Structs;

namespace Polib.CoTasks.Compilation
{
    [StructLayout(LayoutKind.Auto)]
    public struct CoTaskBuilder
    {
        private CoTask task;
        
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CoTaskBuilder Create()
        {
            return new CoTaskBuilder
            {
                Task = new CoTask()
            };
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public CoTask Task
        {
            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => task;
            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private set => task = value;
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetException(Exception ex)
        {
            // throw exception;
            throw ex;
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetResult()
        {
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            task.src = new CoTaskSource(awaiter);
            var cont = BuilderShares.Bind(task.MarkComplete, stateMachine.MoveNext);
            awaiter.OnCompleted(cont);
            // MovableRunner.Singleton.AwaitSource(task, awaiter, stateMachine);
        }

        [DebuggerHidden]
        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter,
            ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            task.src = new CoTaskSource(awaiter);
            var cont = BuilderShares.Bind(task.MarkComplete, stateMachine.MoveNext);
            awaiter.OnCompleted(cont);
            // MovableRunner.Singleton.AwaitSource(task, awaiter, stateMachine);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            // don't use boxed stateMachine.
        }
    }
}

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0436
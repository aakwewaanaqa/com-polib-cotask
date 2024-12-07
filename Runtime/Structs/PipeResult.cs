using System;
using Polib.CoTasks.Classes;
using Polib.CoTasks.Interfaces;

namespace Polib.CoTasks.Structs
{
    public struct PipeResult : PipeReturn
    {
        public       bool               IsFaulty   => Ex is not null;
        public       bool               IsEnd      => then is null;
        public       Exception          Ex         { get; }
        private      CoPipe             then       { get; }
        public async CoTask<PipeReturn> Continue() => await then();
    }
}
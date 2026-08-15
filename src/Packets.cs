using System.Collections.Generic;
using ProtoBuf;

namespace Swatch
{
    /// <summary>Client asks for the current selection and the materials in it.</summary>
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class SwInfoRequest { }

    /// <summary>
    /// Server's answer: what the selection covers and which material tokens in it
    /// have at least one valid replacement. Names/Counts are parallel lists.
    /// </summary>
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class SwInfoResponse
    {
        public bool HasSelection;
        public string SelectionDesc;
        public List<string> Sources = new();
        public List<int> Counts = new();
    }

    /// <summary>Client picked a source material; asks what it can become.</summary>
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class SwTargetsRequest
    {
        public string From;
    }

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class SwTargetsResponse
    {
        public string From;
        public List<string> Targets = new();
        public List<int> Counts = new();
    }

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class SwApplyRequest
    {
        public string From;
        public string To;
    }

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class SwUndoRequest { }

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class SwClearRequest { }

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class SwStatus
    {
        public bool Success;
        public string Message;
    }
}

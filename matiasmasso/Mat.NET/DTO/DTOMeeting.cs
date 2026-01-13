using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOMeeting
    {
        public Guid Guid { get; set; }
        public DateTime Fch { get; set; }
        public DTOContact Place { get; set; }
        public Medias Media { get; set; }
        public string Subject { get; set; }
        public List<DTOUser> Presents { get; set; }
        public DTOUser UsrCreated { get; set; }
        public string PresentOthers { get; set; }
        public List<DTOMeetingStatement> Statements { get; set; }

        public bool IsNew { get; set; }
        public bool IsLoaded { get; set; }

        public enum Medias
        {
            NotSet,
            Presencial,
            VideoCall
        }
    }

    public class DTOMeetingStatement
    {
        public Guid Guid { get; set; }
        public int Ord { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<DTOMeetingTask> Tasks { get; set; }
    }

    public class DTOMeetingTask
    {
        public Guid Guid { get; set; }
        public string Task { get; set; }
        public DateTime Deadline { get; set; }
        public DTOUser User { get; set; }
        public StatusEnum Status { get; set; }

        public enum StatusEnum
        {
            Open,
            EnCurso,
            Done
        }
    }
}

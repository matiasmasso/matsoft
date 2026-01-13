using Newtonsoft.Json;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;

public class DTOContest : DTOContestBase
{
    public List<DTOContestParticipant> Participants { get; set; }

    public DTOContest() : base()
    {
    }

    public DTOContest(Guid oGuid) : base(oGuid)
    {
    }
}

public class DTOContestParticipant : DTOContestBaseParticipant
{
    [JsonIgnore]
    public Image Selfie { get; set; }

    public DTOContestParticipant() : base()
    {
    }

    public DTOContestParticipant(Guid oGuid) : base(oGuid)
    {
    }
}

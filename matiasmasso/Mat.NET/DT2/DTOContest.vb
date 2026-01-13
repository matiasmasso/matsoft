


Public Class DTOContest
    Inherits DTOContestBase

    Property Participants As List(Of DTOContestParticipant)

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class

Public Class DTOContestParticipant
    Inherits DTOContestBaseParticipant

    <JsonIgnore> Property Selfie As Image

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class

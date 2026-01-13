Public Class DTOMeeting
    Property Guid As Guid
    Property Fch As Date
    Property Place As DTOContact
    Property Media As Medias
    Property Subject As String
    Property Presents As List(Of DTOUser)
    Property UsrCreated As DTOUser
    Property PresentOthers As String
    Property Statements As List(Of DTOMeetingStatement)

    Property IsNew As Boolean
    Property IsLoaded As Boolean

    Public Enum Medias
        NotSet
        Presencial
        VideoCall
    End Enum
End Class

Public Class DTOMeetingStatement
    Property Guid As Guid
    Property Ord As Integer
    Property Subject As String
    Property Body As String
    Property Tasks As List(Of DTOMeetingTask)
End Class

Public Class DTOMeetingTask
    Property Guid As Guid
    Property Task As String
    Property Deadline As Date
    Property User As DTOUser
    Property Status As StatusEnum

    Public Enum StatusEnum
        Open
        EnCurso
        Done
    End Enum

End Class



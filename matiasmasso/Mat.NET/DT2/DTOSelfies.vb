Public Class DTOSelfiePlayer
    Inherits DTORaffleParticipant

    Overloads Property Category As Categories
    Property EmailAddress As String
    Property FirstName As String
    Property LastName As String
    Property Address As String
    Property Zip As String
    Property Location As String
    Property Province As String
    Property Birthday As Date
    Property Tel As String
    Property ChildCount As Integer
    Property LastChildBirthday As Nullable(Of Date)
    Property AcceptedTerms As Boolean

    Property IsNewLead As Boolean
    Property IsNewPlayer As Boolean
    Property HasPlayedToday As Boolean
    Property IsMissingData As Boolean

    Public Enum Categories
        NotSet
        DolceAtesa
        Mamma
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class

Public Class DTOSelfie
    Property Hash As String
    Property Player As DTOSelfiePlayer
    <JsonIgnore> Property Image As Image
    <JsonIgnore> Property Thumbnail As Image
    Property Width As Integer
    Property Height As Integer
    Property Length As Integer
    Property Mime As MimeCods
    Property Fch As Date
    Property Winner As Boolean
    Property Rating As Ratings

    Property IsNew As Boolean
    Property IsLoaded As Boolean

    Public Enum Ratings
        NotSet
        Discard
        Stars1
        Stars2
        Stars3
        Stars4
        Stars5
    End Enum
End Class

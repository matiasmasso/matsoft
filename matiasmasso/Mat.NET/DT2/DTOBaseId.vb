Public Class DTOBaseId
    Private _Id As Integer

    Property AuxObject As Object

    Property IsNew As Boolean
    Property IsLoaded As Boolean

    Public Sub New(iId As Integer)
        MyBase.New()
        _Id = iId
    End Sub

    ReadOnly Property Id As Integer
        Get
            Return _Id
        End Get
    End Property

    Public Shadows Function UnEquals(oCandidate As DTOBaseId) As Boolean
        Dim retval As Boolean = Not Equals(oCandidate)
        Return retval
    End Function

    Public Shadows Function Equals(oCandidate As DTOBaseId) As Boolean
        Dim retval As Boolean
        If oCandidate IsNot Nothing Then
            retval = _Id = oCandidate.Id
        End If
        Return retval
    End Function

End Class

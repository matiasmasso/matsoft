Public Class DTOCnap
    Inherits DTOBaseGuid

    Property Id As String
    Property Parent As DTOCnap
    Property Children As List(Of DTOCnap)
    Property NomShort As DTOLangText
    Property NomLong As DTOLangText
    Property Tags As List(Of String)
    Property Products As List(Of DTOProduct)

    Public Sub New()
        MyBase.New
        _NomShort = New DTOLangText
        _NomLong = New DTOLangText
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Public Function ShortFullNom(oLang As DTOLang) As String
        Dim retval As String = String.Format("{0} {1}", _Id, _NomShort.tradueix(oLang))
        Return retval
    End Function

    Public Function FullNom(oLang As DTOLang) As String
        Dim sNom As String = _NomLong.tradueix(oLang)
        If sNom = "" Then sNom = _NomShort.tradueix(oLang)
        Dim retval As String = String.Format("{0} {1}", _Id, sNom)
        Return retval
    End Function

    Public Function Parents() As List(Of DTOCnap)
        Dim retval As New List(Of DTOCnap)
        Dim item As DTOCnap = Me
        Do While item.Parent IsNot Nothing
            retval.Add(item.Parent)
            item = item.Parent
        Loop
        Return retval
    End Function

    Public Function IsSelfOrChildOf(oParent As DTOCnap) As Boolean
        Dim retval As Boolean = oParent.Equals(Me)
        If Not retval Then
            retval = IsChildOf(oParent)
        End If
        Return retval
    End Function

    Public Function IsSelfOrChildOfAny(oParents As List(Of DTOCnap)) As Boolean
        Dim retval As Boolean = oParents.Any(Function(x) x.Equals(Me) OrElse Me.IsChildOf(x))
        Return retval
    End Function

    Public Function IsChildOf(oParent As DTOCnap) As Boolean
        Dim retval As Boolean = Me.Parents.Any(Function(x) x.Equals(oParent))
        Return retval
    End Function


End Class

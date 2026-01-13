Public Class DTOStaffPos
    Inherits DTOBaseGuid

    Property langNom As DTOLangText
    Property Ord As Integer

    Public Sub New()
        MyBase.New()
        _langNom = New DTOLangText
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _langNom = New DTOLangText
    End Sub

    Shared Function Nom(oStaffPos As DTOStaffPos, oLang As DTOLang) As String
        Dim retval As String = ""
        If oStaffPos IsNot Nothing Then
            retval = oStaffPos.langNom.tradueix(oLang)
        End If
        Return retval
    End Function
End Class

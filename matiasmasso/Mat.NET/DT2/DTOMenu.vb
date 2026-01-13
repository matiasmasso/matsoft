Public Class DTOMenu
    Inherits DTOBaseGuid

    Property App As DTOApp.AppTypes
    Property Cod As Cods

    Property Caption As DTOLangText
    Property Url As String
    Property Ord As String
    Property Action As String
    Property Controller As String
    Property Rols As List(Of DTORol)
    Property Parent As DTOMenu
    Property Items As List(Of DTOMenu)

    Public Enum Cods
        NotSet
        Queries
        Forms
        Profile
        Customer
        Rep
        Comercial
        Staff
    End Enum

    Public Sub New()
        MyBase.New()
        _Caption = New DTOLangText(MyBase.Guid, DTOLangText.Srcs.WebMenuItem)
        _Rols = New List(Of DTORol)
        _Items = New List(Of DTOMenu)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Caption = New DTOLangText(MyBase.Guid, DTOLangText.Srcs.WebMenuItem)
        _Rols = New List(Of DTORol)
        _Items = New List(Of DTOMenu)
    End Sub

    Shared Function Factory(nom As String, Optional url As String = "") As DTOMenu
        Dim retval As New DTOMenu
        With retval
            .Caption.Esp = nom
            .Url = url
        End With
        Return retval
    End Function

    Public Function AddChild(nom As String, url As String) As DTOMenu
        Dim retval = DTOMenu.Factory(nom, url)
        _Items.Add(retval)
        Return retval
    End Function


    Shared Function SegmentUrl(oMenu As DTOMenu, ParamArray oParams() As String) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append(oMenu.Url)
        For Each sParam As String In oParams
            sb.Append("/" & sParam)
        Next
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Public Class Collection
        Inherits List(Of DTOMenu)
    End Class
End Class



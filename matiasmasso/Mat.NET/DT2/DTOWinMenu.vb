Public Class DTOWinMenuItem
    Inherits DTOBaseGuid

    Property Parent As DTOWinMenuItem
    Property LangText As DTOLangText
    Property Ord As Integer
    Property Action As String
    Property Cod As Cods
    Property CustomTarget As CustomTargets
    Property Tag As DTOBaseGuid
    <JsonIgnore> Property Icon As Image

    Property Emps As List(Of DTOEmp)
    Property Rols As List(Of DTORol)
    Property Children As List(Of DTOWinMenuItem)

    Public Enum Cods
        NotSet
        Folder
        Item
    End Enum

    Public Enum CustomTargets
        None
        Bancs
        Staff
        Reps
        UserTasks
    End Enum

    Public Sub New()
        MyBase.New()
        _Children = New List(Of DTOWinMenuItem)
        _LangText = New DTOLangText
        _Rols = New List(Of DTORol)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Children = New List(Of DTOWinMenuItem)
        _LangText = New DTOLangText
        _Rols = New List(Of DTORol)
    End Sub

    Shared Function Factory(oParent As DTOWinMenuItem) As DTOWinMenuItem
        Dim retval As New DTOWinMenuItem()
        With retval
            .Parent = oParent
            .Emps = oParent.Emps
        End With
        Return retval
    End Function

    Shared Sub loadBancs(items As List(Of DTOBanc), ByRef oParent As DTOWinMenuItem)
        If oParent.Children.Count = 0 Then
            For Each item In items
                Dim oMenuitem As New DTOWinMenuItem
                With oMenuitem
                    .Icon = item.Logo
                    .Tag = item
                    .LangText = New DTOLangText(item.AbrOrNom)
                    .Cod = DTOWinMenuItem.Cods.Item
                    .CustomTarget = DTOWinMenuItem.CustomTargets.Bancs
                End With
                oParent.Children.Add(oMenuitem)
            Next
        End If
    End Sub

    Shared Sub loadReps(oReps As List(Of DTORep), ByRef oParent As DTOWinMenuItem)
        If oParent.Children.Count = 0 Then
            For Each item As DTORep In oReps
                Dim oMenuitem As New DTOWinMenuItem
                With oMenuitem
                    .Icon = item.Img48
                    .Tag = item
                    .LangText = New DTOLangText(item.NickName)
                    .Cod = DTOWinMenuItem.Cods.Item
                    .CustomTarget = DTOWinMenuItem.CustomTargets.Reps
                End With
                oParent.Children.Add(oMenuitem)
            Next
        End If
    End Sub

    Shared Sub loadStaffs(oStaffs As List(Of DTOStaff), ByRef oParent As DTOWinMenuItem)
        If oParent.Children.Count = 0 Then
            For Each item As DTOStaff In oStaffs
                Dim oMenuitem As New DTOWinMenuItem
                With oMenuitem
                    .Icon = item.Logo
                    .Tag = item
                    .LangText = New DTOLangText(item.Abr)
                    .Cod = DTOWinMenuItem.Cods.Item
                    .CustomTarget = DTOWinMenuItem.CustomTargets.Staff
                End With
                oParent.Children.Add(oMenuitem)
            Next
        End If
    End Sub

End Class

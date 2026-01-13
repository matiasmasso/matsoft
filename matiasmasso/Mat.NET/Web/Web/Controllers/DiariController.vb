Public Class DiariController
    Inherits _MatController


    Async Function Diari() As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Await BaseDiari(Nothing, Today.Year, Today.Month, Today.Day, DtoDiariItem.Levels.Yea)
        Return retval
    End Function

    Async Function ContactDiari(contact As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Await BaseDiari(contact, Today.Year, Today.Month, Today.Day, DtoDiariItem.Levels.Yea)
        Return retval
    End Function

    Async Function BaseDiari(contact As Guid, year As Integer, month As Integer, day As Integer, level As DTODiariItem.Levels) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim Model As New DTODiari()
        With Model
            .Lang = ContextHelper.Lang
            .User = ContextHelper.GetUser()
            .emp = GlobalVariables.Emp
            .MaxDisplayableBrands = GetMaxBrandsPerDisplaySize(Request.UserAgent)
            Select Case .User.Rol.Id
                Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.SalesManager
                    If contact <> Nothing Then
                        .Owner = Await FEB2.Contact.Find(contact, exs)
                    End If
                Case DTORol.Ids.Comercial, DTORol.Ids.Rep
                    If contact = Nothing Then
                        '.Owner = await FEB2.User.GetRep(.User,exs)
                    Else
                        .Owner = Await FEB2.Contact.Find(contact, exs)
                    End If
                   ' FEB2.Contact.Load(.Owner)
                Case DTORol.Ids.CliFull, DTORol.Ids.CliLite
                    If contact = Nothing Then
                        Dim oContacts = Await FEB2.User.Contacts(exs, .User)
                        oContacts = oContacts.Where(Function(x) x.Obsoleto = False).ToList
                        If exs.Count = 0 AndAlso oContacts.Count > 0 Then
                            '.Owner = FEB2.User.Contacts(.User)(0)
                        End If
                    Else
                        '.Owner = New DTOContact(contact)
                    End If
                    ' FEB2.Contact.Load(.Owner)

                Case Else
                    ' .Owner = New DTOContact()
                    ' .Owner.Rol = .User.Rol
            End Select
            .Year = year
            .Month = month
            .Day = day
        End With

        Model = Await FEB2.Diari.LoadBrands(Model, exs)
        If exs.Count > 0 Then
            If Debugger.IsAttached Then Stop
        End If

        Select Case level
            Case DtoDiariItem.Levels.Yea, DtoDiariItem.Levels.Mes
                retval = LoginOrView("Diari", Model)
            Case DtoDiariItem.Levels.Dia
                retval = LoginOrView("DiariDetail", Model)
        End Select
        Return retval
    End Function

    Private Function GetMaxBrandsPerDisplaySize(sUserAgent As String) As Integer
        Dim retval As Integer = 100
        If sUserAgent.Contains("iPad") Then
            retval = 3
        ElseIf sUserAgent.Contains("iPhone") Then
            retval = 0
        End If
        Return retval
    End Function

End Class
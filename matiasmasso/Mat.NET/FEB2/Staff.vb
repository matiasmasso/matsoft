Public Class Staff
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOStaff)
        Return Await Api.Fetch(Of DTOStaff)(exs, "staff", oGuid.ToString())
    End Function

    Shared Async Function Avatar(exs As List(Of Exception), oGuid As Guid) As Task(Of Byte())
        Return Await Api.FetchImage(exs, "staff/avatar", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef ostaff As DTOStaff) As Boolean
        If Not ostaff.IsLoaded And Not ostaff.IsNew Then
            Dim pstaff = Api.FetchSync(Of DTOStaff)(exs, "staff", ostaff.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOStaff)(pstaff, ostaff, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function


    Shared Async Function Update(exs As List(Of Exception), value As DTOStaff) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            oMultipart.AddFileContent("avatar", value.Avatar)
            retval = Await Api.Upload(oMultipart, exs, "staff/upload")
        End If
        Return retval
    End Function

    Shared Async Function Delete(exs As List(Of Exception), ostaff As DTOStaff) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOStaff)(ostaff, exs, "staff")
    End Function

End Class

Public Class Staffs

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTOStaff))
        Return Await Api.Fetch(Of List(Of DTOStaff))(exs, "Staffs", oEmp.Id, 0)
    End Function

    Shared Async Function All(exs As List(Of Exception), oExercici As DTOExercici) As Task(Of List(Of DTOStaff))
        Return Await Api.Fetch(Of List(Of DTOStaff))(exs, "Staffs", oExercici.Emp.Id, oExercici.Year)
    End Function

    Shared Async Function AllActiveWithIcons(exs As List(Of Exception), oEmp As DTOEmp, itemWidth As Integer, itemHeight As Integer) As Task(Of List(Of DTOStaff))
        Dim oStaffs = Await AllActive(oEmp, exs)
        If exs.Count = 0 And oStaffs.Count > 0 Then
            Dim oSprite = Await AllActiveSprite(exs, oEmp, itemWidth, itemHeight)
            If exs.Count = 0 Then
                For idx As Integer = 0 To oStaffs.Count - 1
                    oStaffs(idx).Logo = LegacyHelper.SpriteHelper.Extract(oSprite, idx, oStaffs.Count)
                Next
            End If
        End If
        Return oStaffs
    End Function

    Shared Async Function AllActive(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of List(Of DTOStaff))
        Return Await Api.Fetch(Of List(Of DTOStaff))(exs, "Staffs/active", oEmp.Id)
    End Function

    Shared Async Function AllActiveSprite(exs As List(Of Exception), oEmp As DTOEmp, itemWidth As Integer, itemHeight As Integer) As Task(Of Byte())
        Dim retval As Byte() = Await Api.FetchImage(exs, "staffs/active/sprite", oEmp.Id, itemWidth, itemHeight)
        Return retval
    End Function

    Shared Async Function Ibans(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of List(Of DTOIban))
        Return Await Api.Fetch(Of List(Of DTOIban))(exs, "staffs/ibans", oEmp.Id)
    End Function

    Shared Async Function Saldos(oExercici As DTOExercici, exs As List(Of Exception)) As Task(Of List(Of DTOPgcSaldo))
        Return Await Api.Fetch(Of List(Of DTOPgcSaldo))(exs, "staffs/saldos", oExercici.Emp.Id, oExercici.Year)
    End Function



End Class
Public Class Staff
    Shared Function Find(oGuid As Guid) As DTOStaff
        Dim retval As DTOStaff = StaffLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Avatar(oGuid As Guid) As Byte()
        Dim retval = StaffLoader.Avatar(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oStaff As DTOStaff) As Boolean
        Dim retval As Boolean = StaffLoader.Load(oStaff)
        Return retval
    End Function

    Shared Function Update(oStaff As DTOStaff, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = StaffLoader.Update(oStaff, exs)
        Return retval
    End Function

    Shared Function Delete(oStaff As DTOContact, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = StaffLoader.Delete(oStaff, exs)
        Return retval
    End Function

    Shared Function AvatarUrl(oStaff As DTOStaff, Optional AbsoluteUrl As Boolean = False) As String
        Return UrlHelper.Image(DTO.Defaults.ImgTypes.Staff, oStaff.Guid, AbsoluteUrl)
    End Function

End Class

Public Class Staffs
    Shared Function All(oEmp As DTOEmp, Optional oExercici As DTOExercici = Nothing, Optional IncludeAvatar As Boolean = False) As List(Of DTOStaff)
        Dim retval As List(Of DTOStaff) = StaffsLoader.All(oEmp)

        If oExercici IsNot Nothing Then
            retval = retval.
                    Where(Function(x) x.Alta = Nothing Or x.Alta.Year <= oExercici.Year).
                    Where(Function(x) x.Baixa = Nothing Or x.Baixa.Year >= oExercici.Year).
                    OrderBy(Function(y) y.Nom).
                    ToList

        End If
        Return retval
    End Function

    Shared Function Active(oEmp As DTOEmp) As List(Of DTOStaff)
        Dim retval As List(Of DTOStaff) = StaffsLoader.All(oEmp, OnlyActive:=True)
        Return retval
    End Function

    Shared Function Sprite(oEmp As DTOEmp, OnlyActive As Boolean, itemWidth As Integer, itemHeight As Integer) As Byte()
        Dim oAvatars = StaffsLoader.ActiveAvatars(oEmp, OnlyActive:=OnlyActive)
        Dim retval = LegacyHelper.SpriteBuilder.Factory(oAvatars, itemWidth, itemHeight)
        Return retval
    End Function

    Shared Function Ibans(oEmp As DTOEmp) As List(Of DTOIban)
        Dim retval As List(Of DTOIban) = StaffsLoader.Ibans(oEmp)
        Return retval
    End Function

    Shared Function Saldos(oExercici As DTOExercici) As List(Of DTOPgcSaldo)
        Dim retval As List(Of DTOPgcSaldo) = StaffsLoader.Saldos(DTOExercici.Current(oExercici.Emp))
        Return retval
    End Function

    Shared Function Excel(oStaffs As List(Of DTOStaff), oExercici As DTOExercici, oLang As DTOLang) As MatHelper.Excel.Sheet
        Dim retval As New MatHelper.Excel.Sheet(oExercici.Year, "M+O Staff")
        With retval
            .AddColumn("Nom", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("NIF", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("Num SS", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("Alta", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            .AddColumn("Baixa", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            .AddColumn("Dies " & oExercici.Year, MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("Home", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("Dona", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("Neixament", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            .AddColumn("Edat", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("Grup de Cotització", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Categoría Professional", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Posició", MatHelper.Excel.Cell.NumberFormats.PlainText)
        End With

        For Each oStaff In oStaffs
            Dim oRow As MatHelper.Excel.Row = retval.AddRow
            With oRow
                .AddCell(oStaff.Nom)
                .AddCell(oStaff.PrimaryNifValue())
                .AddCell(oStaff.NumSs)
                .AddCell(oStaff.Alta)
                .AddCell(oStaff.Baixa)
                .AddFormula("IF(ISBLANK(RC[-1]),""" & oExercici.Year & "-12-31"",MIN(""" & oExercici.Year & "-12-31"",RC[-1]))-MAX(""" & oExercici.Year & "-01-01"",RC[-2])")
                .AddCell(If(oStaff.Sex = DTOEnums.Sexs.Male, 1, 0))
                .AddCell(If(oStaff.Sex = DTOEnums.Sexs.Female, 1, 0))
                .AddCell(oStaff.Birth)
                .AddFormula(oExercici.Year & "-YEAR(RC[-1])")
                .AddCell(DTOStaff.SegSocialGrup(oStaff))
                .AddCell(DTOStaff.Categoria(oStaff))
                .AddCell(DTOStaff.Posicio(oStaff, oLang))
            End With
        Next
        Return retval
    End Function
End Class

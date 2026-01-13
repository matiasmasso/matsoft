Public Class DTOStaff
    Inherits DTOContact

    Property abr As String
    <JsonIgnore> Property avatar As Image

    Property alta As Date
    Property baixa As Date
    Property birth As Date
    Property sex As DTOEnums.Sexs
    Property numSs As String
    Property iban As DTOIban
    Property staffPos As DTOStaffPos

    Property category As DTOStaffCategory

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function FromContact(oContact As DTOContact) As DTOStaff
        Dim retval As DTOStaff = Nothing
        If oContact Is Nothing Then
            retval = New DTOStaff
        Else
            retval = New DTOStaff(oContact.Guid)
            With retval
                .Emp = oContact.Emp
                .Nom = oContact.Nom
                .NomComercial = oContact.NomComercial
                .Nif = oContact.Nif
                .Address = oContact.Address
                .ContactClass = oContact.ContactClass
                .Lang = oContact.Lang
                .Rol = oContact.Rol
            End With
        End If
        Return retval
    End Function


    Shared Function Posicio(oStaff As DTOStaff, oLang As DTOLang) As String
        Dim retval As String = ""
        If oStaff IsNot Nothing Then
            If oStaff.StaffPos IsNot Nothing Then
                retval = DTOStaffPos.Nom(oStaff.StaffPos, oLang)
            End If
        End If
        Return retval
    End Function

    Shared Function Categoria(oStaff As DTOStaff) As String
        Dim retval As String = ""
        If oStaff IsNot Nothing Then
            If oStaff.Category IsNot Nothing Then
                retval = oStaff.Category.Nom
            End If
        End If
        Return retval
    End Function

    Shared Function SegSocialGrup(oStaff As DTOStaff) As String
        Dim retval As String = ""
        If oStaff IsNot Nothing Then
            If oStaff.Category IsNot Nothing Then
                If oStaff.Category.SegSocialGrup IsNot Nothing Then
                    retval = oStaff.Category.SegSocialGrup.Nom
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function AliasOrNom(oStaff As DTOStaff) As String
        Dim retval As String = oStaff.Abr
        If retval = "" Then retval = oStaff.Nom
        If retval = "" Then retval = oStaff.FullNom
        Return retval
    End Function

    Public Function Age(Optional toFch As Date = Nothing) As Integer
        Dim retval As Integer = 0

        If _birth <> Nothing Then
            If toFch = Nothing Then toFch = Date.Today
            retval = toFch.Year - _birth.Year
            'Go back to the year the person was born in case of a leap year
            If (_birth.Date > toFch.AddYears(-retval)) Then
                retval -= 1
            End If
        End If
        Return retval
    End Function

    Shared Function Excel(oStaffs As List(Of DTOStaff), oLang As DTOLang, Optional oExercici As DTOExercici = Nothing) As MatHelperStd.ExcelHelper.Sheet
        Dim sSheetName As String = "staff"
        If oExercici IsNot Nothing Then sSheetName = oExercici.year
        Dim retval As New MatHelperStd.ExcelHelper.Sheet(sSheetName, "M+O Staff")
        With retval
            .AddColumn("Nom", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("NIF", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("Num SS", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("Alta", MatHelperStd.ExcelHelper.Sheet.NumberFormats.DDMMYY)
            .AddColumn("Baixa", MatHelperStd.ExcelHelper.Sheet.NumberFormats.DDMMYY)
            If oExercici IsNot Nothing Then
                .AddColumn("Dies " & oExercici.year, MatHelperStd.ExcelHelper.Sheet.NumberFormats.Integer)
            End If
            .AddColumn("Home", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Integer)
            .AddColumn("Dona", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Integer)
            .AddColumn("Neixament", MatHelperStd.ExcelHelper.Sheet.NumberFormats.DDMMYY)
            .AddColumn("Edat", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Integer)
            .AddColumn("Grup de Cotització", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("Categoría Professional", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("Posició", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("Adreça", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("Codi postal", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("Població", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("Telefon", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("Iban", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
        End With

        For Each oStaff In oStaffs
            Dim oRow As MatHelperStd.ExcelHelper.Row = retval.AddRow
            With oRow
                .AddCell(oStaff.nom)
                .AddCell(oStaff.nif)
                .AddCell(oStaff.numSs)
                .AddCell(oStaff.alta)
                .AddCell(oStaff.baixa)
                If oExercici IsNot Nothing Then
                    .AddFormula("IF(ISBLANK(RC[-1]),""" & oExercici.year & "-12-31"",MIN(""" & oExercici.year & "-12-31"",RC[-1]))-MAX(""" & oExercici.year & "-01-01"",RC[-2])")
                End If
                .AddCell(If(oStaff.sex = DTOEnums.Sexs.Male, 1, 0))
                .AddCell(If(oStaff.sex = DTOEnums.Sexs.Female, 1, 0))
                .AddCell(oStaff.birth)
                If oStaff.birth = Nothing Then
                    .AddCell()
                Else
                    If oExercici Is Nothing Then
                        .AddCell(oStaff.Age())
                    Else
                        .AddFormula(oStaff.Age(New Date(oExercici.year, 1, 1)))
                    End If
                End If
                .AddCell(DTOStaff.SegSocialGrup(oStaff))
                .AddCell(DTOStaff.Categoria(oStaff))
                .AddCell(DTOStaff.Posicio(oStaff, oLang))
                oRow.AddCell(oStaff.address.text)
                oRow.AddCell(oStaff.address.zip.zipCod)
                oRow.AddCell(DTOAddress.LocationFullNom(oStaff.address, oLang))
                oRow.AddCell(oStaff.telefon)
                oRow.AddCell(DTOIban.Formated(oStaff.iban.digits))
            End With
        Next
        Return retval
    End Function


End Class

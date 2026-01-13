Public Class DTOPnd
    Inherits DTOBaseGuid

    'Partides pendents de comptabilitat

    Property emp As DTOEmp
    Property id As Integer
    Property Contact As DTOContact
    Property Cfp As FormasDePagament
    Property Fpg As String
    Property Amt As DTOAmt
    Property Vto As Date
    Property Cod As Codis = Codis.NotSet
    Property Yef As Integer
    Property FraNum As String = ""
    Property Invoice As DTOInvoice
    Property Fch As Date
    Property Cta As DTOPgcCta
    Property Cca As DTOCca
    Property CcaVto As DTOCca
    Property Csb As DTOCsb
    Property Status As StatusCod = StatusCod.NotSet

    Public Enum Codis
        NotSet
        Deutor
        Creditor
    End Enum

    Public Enum StatusCod
        NotSet = -1
        pendent = 0
        enCartera = 1
        enCirculacio = 2
        saldat = 10
        compensat = 11
    End Enum

    Public Enum FormasDePagament
        NotSet = 0
        Rebut = 1
        ReposicioFons = 2
        Comptat = 3
        Xerocopia = 4
        DomiciliacioBancaria = 5
        Transferencia = 6
        aNegociar = 9
        EfteAndorra = 10
        TransfPrevia = 11
        Diposit = 12
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory(oEmp As DTOEmp) As DTOPnd
        Dim retval As New DTOPnd
        With retval
            .Emp = oEmp
        End With
        Return retval
    End Function

    Shared Function Factory(oInvoice As DTOInvoice, oCta As DTOPgcCta) As DTOPnd
        Dim retval As DTOPnd = Nothing
        If oInvoice IsNot Nothing Then
            retval = DTOPnd.Factory(oInvoice.Emp)
            With retval
                .Fch = oInvoice.Fch
                .Cod = DTOPnd.Codis.Deutor
                .Cfp = oInvoice.Cfp
                .Amt = oInvoice.Total
                .Contact = oInvoice.Customer
                .Yef = oInvoice.Fch.Year
                .FraNum = oInvoice.Num
                .Invoice = oInvoice
                .Vto = oInvoice.Vto
                .Fpg = oInvoice.Fpg
                .Cca = oInvoice.Cca
                .Cta = oCta
                .Status = DTOPnd.StatusCod.pendent
            End With
        End If
        Return retval
    End Function

    Public Function Clon() As DTOPnd
        Dim retval As DTOPnd = Me
        MyBase.RenewGuid()
        Return retval
    End Function

    Shared Function Concepte(oPnds As List(Of DTOPnd)) As String
        Dim sb As New System.Text.StringBuilder
        If oPnds IsNot Nothing Then
            If oPnds.Count > 0 Then
                For Each oPnd As DTOPnd In oPnds
                    If oPnd.FraNum > "" Then
                        If oPnd.UnEquals(oPnds.First) Then sb.Append(",")
                        sb.Append(oPnd.FraNum)
                    End If
                Next
                Dim oLang As DTOLang = oPnds.First.Contact.Lang
                If oLang Is Nothing Then oLang = DTOLang.ESP
                Select Case oPnds.Count
                    Case 1
                        sb.Insert(0, oLang.tradueix("Factura ", "Factura ", "Invoice "))
                    Case Else
                        sb.Insert(0, oLang.tradueix("Facturas ", "Facturas ", "Invoices "))
                End Select
            End If
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function FacturaText(oPnd As DTOPnd, oLang As DTOLang) As String
        Dim retval As String = String.Format("{0} {1}", oLang.tradueix("Factura", "Factura", "Invoice"), oPnd.FraNum)
        Return retval
    End Function

    Shared Function GetFpg(oCfp As DTOPnd.FormasDePagament) As String
        Dim retval As String = ""
        Select Case oCfp
            Case DTOPnd.FormasDePagament.NotSet
                retval = "(sense forma de pago)"
            Case Else
                retval = oCfp.ToString
        End Select
        Return retval
    End Function

    Shared Function DueDays(oPnd As DTOPnd) As Integer
        Dim oSpan As TimeSpan = Today - oPnd.Vto
        Dim retval As Integer = oSpan.TotalDays
        Return retval
    End Function

    Shared Function EurDeutor(oPnd As DTOPnd) As Decimal
        Dim retval As Decimal
        If oPnd.Amt IsNot Nothing Then
            retval = IIf(oPnd.Cod = DTOPnd.Codis.Deutor, oPnd.Amt.Eur, -oPnd.Amt.Eur)
        End If
        Return retval
    End Function

    Shared Function Sum(oPnds As List(Of DTOPnd), oCod As DTOPnd.Codis) As DTOAmt
        Dim retval = DTOAmt.Empty
        For Each item As DTOPnd In oPnds
            If oCod = item.Cod Then
                retval.Add(item.Amt)
            Else
                retval.Substract(item.Amt)
            End If
        Next
        Return retval
    End Function

    Shared Function Excel(oPnds As List(Of DTOPnd), sTitle As String, oCod As DTOPnd.Codis, oLang As DTOLang) As MatHelperStd.ExcelHelper.Sheet

        Dim FirstCur As DTOCur = oPnds.First.Amt.cur
        Dim ShowCurAmt As Boolean = oPnds.Any(Function(x) x.Amt.cur.UnEquals(FirstCur))
        Dim ShowCurTag As Boolean = ShowCurAmt Or FirstCur.UnEquals(DTOCur.Eur)

        Dim retval As New MatHelperStd.ExcelHelper.Sheet(sTitle)
        With retval
            .AddColumn("Venciment", MatHelperStd.ExcelHelper.Sheet.NumberFormats.DDMMYY)
            .AddColumn("Deutor/Creditor", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("Eur", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Euro)
            If ShowCurTag Then .AddColumn("Divisa", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            If ShowCurAmt Then .AddColumn("Import", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Decimal2Digits)
            .AddColumn("Compte", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("Factura", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("Data", MatHelperStd.ExcelHelper.Sheet.NumberFormats.DDMMYY)
            .AddColumn("Status", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("Observacions", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
        End With

        If oPnds.Count > 0 Then

            For Each item As DTOPnd In oPnds
                Dim oRow = New MatHelperStd.ExcelHelper.Row(retval)
                retval.Rows.Add(oRow)
                With oRow
                    .AddCell(item.Vto)
                    .AddCell(item.Contact.nom)
                    .AddCell(item.Amt.eur * IIf(item.Cod = oCod, 1, -1))
                    If ShowCurTag Then .AddCell(item.Amt.cur.tag)
                    If ShowCurAmt Then .AddCell(item.Amt.val * IIf(item.Cod = oCod, 1, -1))
                    .AddCell(DTOPgcCta.FullNom(item.Cta, oLang))
                    .AddCell(item.FraNum)
                    .AddCell(item.Fch)
                    .AddCell(item.Status.ToString())
                    .AddCell(item.Fpg)
                End With
            Next
        End If

        Return retval
    End Function

End Class

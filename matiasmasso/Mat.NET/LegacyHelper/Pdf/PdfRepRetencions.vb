Public Class PdfRepRetencions
    Inherits PdfCorporate

    Private mRep As DTORep
    Private mYea As Integer
    Private mQuarter As Integer

    Private xBase = DTOAmt.Empty
    Private xIpf = DTOAmt.Empty
    Private xIva = DTOAmt.Empty
    Private xTot = DTOAmt.Empty

    Private mThBackground As Integer = Color.Yellow.ToArgb
    Private mTdBackground As Integer = Color.LightBlue.ToArgb

    Private Enum Cols
        Id
        Fch
        Bas
        IpfPct
        IpfAmt
        IvaPct
        IvaAmt
        Tot
    End Enum


    Public Sub New(oUser As DTOUser, oRepLiqs As List(Of DTORepLiq), oRep As DTORep, ByVal iYea As Integer, ByVal iQuarter As Integer)
        MyBase.New()
        mRep = oRep
        mYea = iYea
        mQuarter = iQuarter
        PrintHeader()
        PrintTit()

        Me.Table = GetTable(oRepLiqs)
        DrawTable()

    End Sub

    Private Sub PrintHeader()
        'DrawForm()
        Dim exs As New List(Of Exception)
        Dim oArray As New ArrayList
        oArray.Add(mRep.Nom)
        oArray.Add("NIF: " & mRep.PrimaryNifValue())
        oArray.Add(mRep.Address.Text)
        oArray.Add(DTOZip.FullNom(mRep.Address.Zip))
        DrawAdr(oArray)
    End Sub

    Private Sub PrintTit()
        Y = 250
        Dim sTrim As String = Choose(mQuarter, "PRIMER", "SEGUNDO", "TERCER", "CUARTO")
        Dim sText As String = "CERTIFICADO RETENCIONES " & sTrim & " TRIMESTRE " & mYea
        DrawText(sText, mLeft, mRight, StringAlignment.Center)
        Y = Y + 4 * mFont.GetHeight()
    End Sub

    Private Function GetTable(ByVal oRepLiqs As List(Of DTORepLiq)) As MatTable
        Dim oTable As New MatTable(mLeft, Y)
        With oTable.Columns
            .Add("liquidación", 55, MatColumn.Types.integer)
            .Add("fecha", 65, MatColumn.Types.date)
            .Add("base imponible", 75, MatColumn.Types.amt)
            .Add("", 50, MatColumn.Types.percent)
            .Add("IRPF", 60, MatColumn.Types.amt)
            .Add("", 50, MatColumn.Types.percent)
            .Add("IVA", 60, MatColumn.Types.amt)
            .Add("total", 75, MatColumn.Types.amt)
        End With

        With oTable.HeaderRow
            .Visible = True
        End With

        For Each oRepLiq In oRepLiqs
            oTable.Rows.Add(GetRowFromRepLiq(oTable, oRepLiq))
        Next

        With oTable.FooterRow
            .Visible = True
            .Cells(Cols.Fch).Value = "totales"
            .Cells(Cols.Bas).Value = oTable.Columns(Cols.Bas).SumAmt
            .Cells(Cols.IpfAmt).Value = oTable.Columns(Cols.IpfAmt).SumAmt
            .Cells(Cols.IvaAmt).Value = oTable.Columns(Cols.IvaAmt).SumAmt
            .Cells(Cols.Tot).Value = oTable.Columns(Cols.Tot).SumAmt
        End With

        Return oTable
    End Function

    Private Function GetRowFromRepLiq(ByVal oTable As MatTable, ByVal oRepLiq As DTORepLiq) As MatRow
        Dim oRow As MatRow = oTable.NewRow
        oRow.Cells(Cols.Id).Value = oRepLiq.Id
        oRow.Cells(Cols.Fch).Value = oRepLiq.Fch
        oRow.Cells(Cols.Bas).Value = oRepLiq.BaseImponible
        oRow.Cells(Cols.IpfPct).Value = oRepLiq.IRPFpct
        oRow.Cells(Cols.IpfAmt).Value = oRepLiq.IRPFamt
        oRow.Cells(Cols.IvaPct).Value = oRepLiq.IVApct
        oRow.Cells(Cols.IvaAmt).Value = oRepLiq.IVAamt
        oRow.Cells(Cols.Tot).Value = oRepLiq.Total
        Return oRow
    End Function


End Class

Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Data.SqlClient

Public Class Frm_Vivace_Stk

    Private Enum Cols
        Yea
        Q
        Id
        Art
        Dsc
        Qty
        Palet
        Fch
        Dias
        Entrada
        Procedencia
    End Enum

    Private Sub ButtonBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBrowse.Click
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "IMPORTAR FITXER INVENTARI"
            .Filter = "Excel (*.xls)|*.xls|(tots els fitxers)|*.*"
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            If .ShowDialog Then
                TextBoxPath.Text = .FileName
            End If
        End With
    End Sub

    Private Sub ImportaExcel(ByVal sFilename)
        Dim oApp As New Excel.Application
        Dim oWb As Excel.Workbook = oApp.Workbooks.Open(sFilename)
        Dim oSheet As Excel.Worksheet = oWb.ActiveSheet
        Dim iLastCol As Integer = 8
        Dim iFirstCol As Integer = 1
        Dim iFirstRow As Integer = 3
        Dim oFirstCell As Excel.Range = oSheet.Range("A3")
        Dim oCurrentRegion As Excel.Range = oFirstCell.CurrentRegion

        Dim iLastRow As Integer = oCurrentRegion.Rows.Count


        Dim oRow As DataRow
        Dim oTB As DataTable
        Dim oDS As New DataSet
        Dim SQL As String
        Dim iYea As Integer = NumericUpDownYea.Value
        Dim iQ As Integer = NumericUpDownQ.Value

        SQL = "DELETE AUDITSTK WHERE " _
        & "YEA=" & iYea & " AND " _
        & "Q=" & iQ
        maxisrvr.executenonquery(SQL, maxisrvr.Databases.Maxi)

        SQL = "SELECT * FROM AUDITSTK WHERE " _
        & "YEA=" & iYea & " AND " _
        & "Q=" & iQ

        Dim oConn As SqlConnection = maxisrvr.GetSQLConnection(maxisrvr.Databases.Maxi)
        Dim oDA As New SqlDataAdapter

        oDA.SelectCommand = New SqlCommand(SQL, oConn)
        Dim oCB As New SqlCommandBuilder(oDA)
        oConn.open

        oDa.Fill(oDs)
        oTB = oDS.Tables(0)


        Dim i As Integer
        Dim j As Integer
        For i = iFirstRow To iLastRow
            Try
                oRow = oTB.NewRow
                oRow(Cols.Yea) = iYea
                oRow(Cols.Q) = iQ
                For j = iFirstCol To iLastCol
                    oRow(Cols.Id + j) = oSheet.Cells(i, j).text
                Next
                oTB.Rows.Add(oRow)
            Catch e As Exception
            End Try
        Next
        oWb.Close()
        oApp = Nothing


        oDA.Update(oDS)
        oConn.close
        MsgBox(iLastRow & " linies importades")
    End Sub

    Private Sub ButtonImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonImport.Click
        ImportaExcel(TextBoxPath.Text)
    End Sub

    Private Sub Frm_Vivace_Stk_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        NumericUpDownYea.Value = Today.Year
    End Sub
End Class
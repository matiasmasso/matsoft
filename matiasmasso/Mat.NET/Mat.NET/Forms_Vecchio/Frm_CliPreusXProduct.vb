
Imports System.Data.SqlClient

Public Class Frm_CliPreusXProduct
    Private mProduct As Product = Nothing
    Private mDirty As Boolean = False
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Cli
        Clx
        Preu
    End Enum

    Public Sub New(ByVal oProduct As Product)
        MyBase.New()
        InitializeComponent()
        mProduct = oProduct
        refresca()
        mAllowEvents = True
    End Sub

    Private Sub refresca()
        TextBoxProductNom.Text = mProduct.Text
        TextBoxTarifa.Text = mProduct.tarifaA.CurFormat
        Dim SQL As String = "SELECT CLX.CLI, CLX.CLX, PREUS.TARIFA FROM CLIPREUS PREUS INNER JOIN " _
                            & "CLX ON PREUS.EMP=CLX.EMP AND PREUS.CLIENT=CLX.CLI " _
                            & "WHERE CLX.EMP=@EMP AND PREUS.PRODUCT=@PRODUCT " _
                            & "ORDER BY CLX.CLX"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", App.Current.Emp.Id, "@PRODUCT", mProduct.Guid.ToString)
        Dim oTb As DataTable = oDs.Tables(0)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .ColumnHeadersVisible = True
            .RowHeadersVisible = True
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Cli)
                .Visible = False
            End With

            With .Columns(Cols.Clx)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            With .Columns(Cols.Preu)
                .HeaderText = "preu"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

        End With

        DataGridView1.ClearSelection()
    End Sub

    Private Sub DataGridView1_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValidated
        If mAllowEvents Then
            Select Case e.ColumnIndex
                Case Cols.Clx
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    Dim oCell As DataGridViewCell = oRow.Cells(e.ColumnIndex)

                    Dim ClearValue As Boolean = False
                    If IsDBNull(oRow.Cells(Cols.Cli).Value) Then
                        If Not IsDBNull(oRow.Cells(Cols.Clx).Value) Then ClearValue = True
                    Else
                        Dim iCli As Integer = oRow.Cells(Cols.Cli).Value
                        If iCli = 0 Then
                            ClearValue = True
                            mDirty = False
                        Else
                            oCell.Value = MaxiSrvr.Contact.FromNum(BLL.BLLApp.Emp, iCli).Clx
                        End If
                    End If

                    If ClearValue Then oCell.Value = ""
                Case Cols.Preu
            End Select
        End If
    End Sub

    Private Sub DataGridView1_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles DataGridView1.CellValidating
        If mAllowEvents Then
            Dim oArrayErrs As New ArrayList
            Dim sWarn As String = ""

            Select Case e.ColumnIndex
                Case Cols.Clx
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    Dim oCell As DataGridViewCell = oRow.Cells(e.ColumnIndex)
                    Dim BlProcede As Boolean = False
                    If IsDBNull(oCell.Value) Then
                        BlProcede = (e.FormattedValue > "")
                    Else
                        BlProcede = (e.FormattedValue <> oCell.Value)
                    End If

                    If BlProcede Then
                        Dim sKey As String = e.FormattedValue
                        Dim oContact As Contact = Finder.FindContact(BLL.BLLApp.Emp, sKey)
                        If oContact Is Nothing Then
                            e.Cancel = True
                        Else
                            oRow.Cells(Cols.Cli).Value = new Client(oContact.Guid).CcxOrMe.Id
                        End If
                    End If
            End Select
        End If
    End Sub

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If mAllowEvents Then mDirty = True
    End Sub

    Private Sub DataGridView1_RowValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.RowValidated
        If mAllowEvents Then
            If DataGridView1.Rows(e.RowIndex).Cells(Cols.Cli).Value IsNot Nothing Then
                If mDirty Then
                    UpdateRows()
                End If
            End If
        End If
    End Sub

    Private Sub UpdateRows()
        Dim SQL As String = "DELETE CLIPREUS WHERE EMP=@EMP AND PRODUCT=@PRODUCT"
        maxisrvr.executenonquery(SQL, maxisrvr.Databases.Maxi, "@EMP", App.Current.Emp.Id, "@PRODUCT", mProduct.Guid.ToString)

        SQL = "SELECT * FROM CLIPREUS WHERE EMP=@EMP AND PRODUCT=@PRODUCT "
        Dim oConn As SqlConnection = maxisrvr.GetSQLConnection(maxisrvr.Databases.Maxi)
        Dim oDa As SqlDataAdapter = maxisrvr.GetSQLDataAdapter(SQL, oConn, "@EMP", App.Current.Emp.Id, "@PRODUCT", mProduct.Guid.ToString)
        Dim oDs As New DataSet
        oDa.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each oGridRow As DataGridViewRow In DataGridView1.Rows
            If Not oGridRow.IsNewRow Then
                If Not IsDBNull(oGridRow.Cells(Cols.Cli).Value) And Not IsDBNull(oGridRow.Cells(Cols.Preu).Value) Then
                    Dim oDataRow As DataRow = oTb.NewRow
                    oDataRow("Product") = mProduct.Guid
                    oDataRow("Emp") =BLL.BLLApp.Emp.Id
                    oDataRow("Client") = CInt(oGridRow.Cells(Cols.Cli).Value)
                    oDataRow("Tarifa") = CDec(oGridRow.Cells(Cols.Preu).Value)
                    oTb.Rows.Add(oDataRow)
                End If
            End If
        Next
        oDa.Update(oTb)
        mDirty = False
        RaiseEvent AfterUpdate(Nothing, EventArgs.Empty)

    End Sub

    Private Sub DataGridView1_UserDeletedRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles DataGridView1.UserDeletedRow
        If mAllowEvents Then
            UpdateRows()
        End If
    End Sub
End Class
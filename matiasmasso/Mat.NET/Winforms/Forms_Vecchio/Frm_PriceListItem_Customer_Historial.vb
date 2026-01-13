Public Class Frm_PricelistItemCustomer_Historial
    Private _Sku As DTOProductSku
    Private _AllowEvents As Boolean

    Private Enum Cols
        Guid
        Fch
        Concepte
        Retail
    End Enum

    Public Sub New(oSku As DTOProductSku)
        MyBase.New()
        Me.InitializeComponent()
        _Sku = oSku
    End Sub

    Private Sub Frm_PricelistItemCustomer_Historial_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LoadGrid()
        _AllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT P1.Guid,P1.Fch,P1.Concepte,P2.Art,P2.Retail " _
        & "FROM            DTOPricelistItemCustomer P2 INNER JOIN " _
        & "PriceList_Customer P1 ON P2.PriceList=P1.Guid " _
        & "WHERE P2.Art=@Guid " _
        & "ORDER BY P1.Fch DESC"

        Dim oDs As DataSet = DAL.SQLHelper.GetDataset(SQL, New List(Of Exception), "@Guid", _Sku.Guid.ToString)

        If oDs.Tables.Count > 0 Then
            Dim oTb As DataTable = oDs.Tables(0)

            With DataGridView1
                With .RowTemplate
                    .Height = DataGridView1.Font.Height * 1.3
                End With
                .DataSource = oTb
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .ColumnHeadersVisible = True
                .RowHeadersVisible = False
                .MultiSelect = False
                .AllowUserToResizeRows = False
                .AllowDrop = False

                With .Columns(Cols.Guid)
                    .Visible = False
                End With
                With .Columns(Cols.Fch)
                    .HeaderText = "data"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .Width = 70
                    .DefaultCellStyle.Format = "dd/MM/yy"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                End With
                With .Columns(Cols.Concepte)
                    .HeaderText = "tarifa"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                End With
                With .Columns(Cols.Retail)
                    .HeaderText = "PVP recomenat"
                    .DataPropertyName = "Retail"
                    .Width = 70
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
            End With
        End If
        'SetContextMenu()
        _AllowEvents = True
    End Sub
End Class
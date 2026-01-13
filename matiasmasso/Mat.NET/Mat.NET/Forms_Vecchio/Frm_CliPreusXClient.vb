
Imports System.Data.SqlClient

Public Class Frm_CliPreusXClient
    Private mClient As Client = Nothing
    Private mDirty As Boolean = False
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Guid
        Nom
        Preu
    End Enum

    Public Sub New(ByVal oClient As Client)
        MyBase.New()
        InitializeComponent()
        mClient = oClient.CcxOrMe
        Me.Text = mClient.Clx
        refresca()
        mAllowEvents = True
    End Sub

    Private Sub refresca()
        Dim SQL As String = "SELECT PROD.GUID, PROD.NOM, PREUS.TARIFA FROM CLIPREUS PREUS INNER JOIN " _
                            & "PRODUCT PROD ON PREUS.EMP=PROD.EMP AND PREUS.PRODUCT=PROD.GUID " _
                            & "WHERE PREUS.EMP=@EMP AND PREUS.CLIENT=@CLIENT " _
                            & "ORDER BY NOM"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", App.Current.Emp.Id, "@CLIENT", mClient.Id.ToString)
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
            .ReadOnly = True

            With .Columns(Cols.Guid)
                .Visible = False
            End With

            With .Columns(Cols.Nom)
                .HeaderText = "producte"
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

 
End Class